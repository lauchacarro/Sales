using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Sales.Application.Dtos.Notifications;
using Sales.Domain.Entities.Invoices;
using Sales.Domain.Entities.Notifications;
using Sales.Domain.Entities.Orders;
using Sales.Domain.Entities.Plans;
using Sales.Domain.Entities.Subscriptions;
using Sales.Domain.Options;
using Sales.Domain.PaymentProviders;
using Sales.Domain.Repositories;
using Sales.Domain.Services.Abstracts;
using Sales.Domain.ValueObjects.Orders;

namespace Sales.Application.Services.Concretes
{
    public class JobAppService : ApplicationService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ISubscriptionCycleRepository _subscriptionCycleRepository;
        private readonly IRepository<SubscriptionCycleOrder, Guid> _subscriptionCycleOrderRepository;
        private readonly IRepository<Notification, Guid> _noticationRepository;
        private readonly INotificationDomainService _notificationDomainService;
        private readonly IPlanPriceRepository _planPriceRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IRepository<Invoice, Guid> _invoiceRepository;
        private readonly IRepository<InvoicePaymentProvider, Guid> _invoicePaymentProviderRepository;
        private readonly IOrderDomainService _orderDomainService;
        private readonly ISubscriptionDomainService _subscriptionDomainService;
        private readonly ISubscriptionCycleDomainService _subscriptionCycleDomainService;
        private readonly ISubscriptionCycleOrderDomainService _subscriptionCycleOrderDomainService;
        private readonly IInvoiceDomainService _invoiceDomainService;
        private readonly IObjectMapper _mapper;
        private readonly IPaypalService _paypalService;
        private readonly IMobbexService _mobbexService;
        private readonly IClientOptions _clientOptions;
        private readonly HttpClient _httpClient;

        public JobAppService(ISubscriptionRepository subscriptionRepository,
                             ISubscriptionCycleRepository subscriptionCycleRepository,
                             IRepository<SubscriptionCycleOrder, Guid> subscriptionCycleOrderRepository,
                             IRepository<Notification, Guid> notificationRepository,
                             INotificationDomainService notificationDomainService,
                             IPlanPriceRepository planPriceRepository,
                             IOrderRepository orderRepository,
                             IRepository<Invoice, Guid> invoiceRepository,
                             IRepository<InvoicePaymentProvider, Guid> invoicePaymentProviderRepository,
                             IOrderDomainService orderDomainService,
                             ISubscriptionDomainService subscriptionDomainService,
                             ISubscriptionCycleDomainService subscriptionCycleDomainService,
                             ISubscriptionCycleOrderDomainService subscriptionCycleOrderDomainService,
                             IInvoiceDomainService invoiceDomainService,
                             IObjectMapper mapper,
                             IPaypalService paypalService,
                             IMobbexService mobbexService,
                             IClientOptions clientOptions,
                             IHttpClientFactory httpClientFactory)
        {
            _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
            _subscriptionCycleRepository = subscriptionCycleRepository ?? throw new ArgumentNullException(nameof(subscriptionCycleRepository));
            _subscriptionCycleOrderRepository = subscriptionCycleOrderRepository ?? throw new ArgumentNullException(nameof(subscriptionCycleOrderRepository));
            _noticationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
            _notificationDomainService = notificationDomainService ?? throw new ArgumentNullException(nameof(notificationDomainService));
            _planPriceRepository = planPriceRepository ?? throw new ArgumentNullException(nameof(planPriceRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _invoicePaymentProviderRepository = invoicePaymentProviderRepository ?? throw new ArgumentNullException(nameof(invoicePaymentProviderRepository));
            _orderDomainService = orderDomainService ?? throw new ArgumentNullException(nameof(orderDomainService));
            _subscriptionDomainService = subscriptionDomainService ?? throw new ArgumentNullException(nameof(subscriptionDomainService));
            _subscriptionCycleDomainService = subscriptionCycleDomainService ?? throw new ArgumentNullException(nameof(subscriptionCycleDomainService));
            _subscriptionCycleOrderDomainService = subscriptionCycleOrderDomainService ?? throw new ArgumentNullException(nameof(subscriptionCycleOrderDomainService));
            _invoiceDomainService = invoiceDomainService ?? throw new ArgumentNullException(nameof(invoiceDomainService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _paypalService = paypalService ?? throw new ArgumentNullException(nameof(paypalService));
            _mobbexService = mobbexService ?? throw new ArgumentNullException(nameof(mobbexService));
            _clientOptions = clientOptions ?? throw new ArgumentNullException(nameof(clientOptions));
            _httpClient = httpClientFactory?.CreateClient() ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        [HttpGet]
        public async Task CreateNewSubscriptionCycles()
        {
            var subscriptionCycles = _subscriptionCycleRepository.GetSoonExpires(DateTime.Now, _clientOptions.OrderDaysToExpire);

            foreach (var cycle in subscriptionCycles)
            {
                using var unitOfWork = UnitOfWorkManager.Begin();

                SubscriptionCycle newCycle = _subscriptionCycleDomainService.CreateSubscriptionCycle(cycle.Subscription, DateTime.Now);
                _subscriptionCycleDomainService.PaymentPendingSubscriptionCycle(newCycle);

                _subscriptionCycleRepository.Insert(newCycle);


                Order oldOrder = _orderRepository.GetBySubscriptionCycle(cycle);

                PlanPrice planPrice = cycle.Subscription.Plan.PlanPrices.Single(x => x.Currency.Code == oldOrder.Currency.Code);

                Order order = _orderDomainService.CreateOrderForRenewSubscription(planPrice, oldOrder.UserId, DateTime.Now);
                _orderDomainService.PaymentPendingOrder(order);
                order = _orderRepository.Insert(order);



                SubscriptionCycleOrder subscriptionCycleOrder = _subscriptionCycleOrderDomainService.CreateSubscriptionCycleOrder(newCycle, order);

                _subscriptionCycleOrderRepository.Insert(subscriptionCycleOrder);

                Invoice invoice = _invoiceDomainService.CreateInvoice(order, DateTime.Now);
                _invoiceDomainService.ActiveInvoice(invoice);
                _invoiceRepository.Insert(invoice);



                InvoicePaymentProvider invoicePaymentProvider = planPrice.Currency.Code switch
                {
                    Domain.ValueObjects.Currency.CurrencyValue.USD => await _paypalService.CreateUriForPayment(invoice, order, cycle.Subscription.Plan),
                    Domain.ValueObjects.Currency.CurrencyValue.ARS => await _mobbexService.CreateUriForPayment(invoice, order, cycle.Subscription.Plan),
                    _ => throw new NotImplementedException()
                };

                _invoicePaymentProviderRepository.Insert(invoicePaymentProvider);

                Notification notification = _notificationDomainService.CreateNotification(order);
                _notificationDomainService.SetNewSubscribeCycle(notification);
                _noticationRepository.Insert(notification);

                NotificationDto notificationDto = _mapper.Map<NotificationDto>(notification);

                HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync(_clientOptions.NotificactionUrl, notificationDto);

                if (httpResponse.IsSuccessStatusCode)
                {
                    _noticationRepository.Delete(notification);
                }
                else
                {
                    _notificationDomainService.AddAttempt(notification);

                    _noticationRepository.Update(notification);
                }
                unitOfWork.Complete();
            }
        }

        [HttpGet]
        public void VerifyOrderExpiration()
        {
            IEnumerable<Order> orders = _orderRepository.GetAll()
                .Where(x => (x.Status.Status == OrderStatus.OrderStatusValue.PaymentPending || x.Status.Status == OrderStatus.OrderStatusValue.Created) && DateTime.Today > x.CreationTime.AddDays(_clientOptions.OrderDaysToExpire)).ToList();

            foreach (Order order in orders)
            {
                using var unitOfWork = UnitOfWorkManager.Begin();

                _orderDomainService.ExpireOrder(order);

                if (order.Type.Type == OrderType.OrderTypeValue.Subscription || order.Type.Type == OrderType.OrderTypeValue.RenewSubscription)
                {
                    SubscriptionCycleOrder cycleOrder = _subscriptionCycleOrderRepository.GetAll().Include(x => x.SubscriptionCycle).ThenInclude(x => x.Subscription).Single(x => x.OrderId == order.Id);

                    _subscriptionCycleDomainService.CancelSubscriptionCycle(cycleOrder.SubscriptionCycle);

                    if (order.Type.Type == OrderType.OrderTypeValue.RenewSubscription)
                    {
                        _subscriptionDomainService.SuspendSubscription(cycleOrder.SubscriptionCycle.Subscription);

                    }
                    else if (order.Type.Type == OrderType.OrderTypeValue.Subscription)
                    {
                        _subscriptionDomainService.CancelSubscription(cycleOrder.SubscriptionCycle.Subscription);
                    }

                    _subscriptionCycleOrderRepository.Update(cycleOrder);
                }

                unitOfWork.Complete();
            }
        }
    }
}