
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Events.Bus.Handlers;

using AutoMapper;

using Sales.Application.Dtos.Notifications;
using Sales.Application.Events.Orders.OrderSubscriptionPayedEvent;
using Sales.Domain.Entities.Invoices;
using Sales.Domain.Entities.Notifications;
using Sales.Domain.Entities.Orders;
using Sales.Domain.Entities.Plans;
using Sales.Domain.Entities.Subscriptions;
using Sales.Domain.Options;
using Sales.Domain.PaymentProviders;
using Sales.Domain.Repositories;
using Sales.Domain.Services.Abstracts;
using Sales.Domain.ValueObjects;
using Sales.Domain.ValueObjects.Orders;
using Sales.Domain.ValueObjects.Subscriptions;

namespace Sales.Application.Events.Orders.OrderSubscriptionPayedEvent
{
    public class OrderSubscriptionPayedEventHandler : IAsyncEventHandler<OrderSubscriptionPayedEventData>, ITransientDependency
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IRepository<SubscriptionCycle, Guid> _subscriptionCycleRepository;
        private readonly IRepository<SubscriptionCycleOrder, Guid> _subscriptionCycleOrderRepository;
        private readonly IRepository<Invoice, Guid> _invoiceRepository;
        private readonly IPlanPriceRepository _planPriceRepository;
        private readonly IRepository<Notification, Guid> _noticationRepository;
        private readonly ISubscriptionDomainService _subscriptionDomainService;
        private readonly ISubscriptionCycleDomainService _subscriptionCycleDomainService;
        private readonly IOrderDomainService _orderDomainService;
        private readonly INotificationDomainService _notificationDomainService;
        private readonly IInvoiceDomainService _invoiceDomainService;
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;
        private readonly IClientOptions _clientOptions;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly HttpClient _httpClient;
        private readonly IPaypalService _paypalService;
        private readonly IMobbexService _mobbexService;

        public OrderSubscriptionPayedEventHandler(IOrderRepository orderRepository,
                                      ISubscriptionRepository subscriptionRepository,
                                      IRepository<SubscriptionCycle, Guid> subscriptionCycleRepository,
                                      IRepository<SubscriptionCycleOrder, Guid> subscriptionCycleOrderRepository,
                                      IRepository<Invoice, Guid> invoiceRepository,
                                      IPlanPriceRepository planPriceRepository,
                                      ISubscriptionDomainService subscriptionDomainService,
                                      ISubscriptionCycleDomainService subscriptionCycleDomainService,
                                      IOrderDomainService orderDomainService,
                                      IRepository<Notification, Guid> noticationRepository,
                                      IMapper mapper,
                                      IClientOptions clientOptions,
                                      IHttpClientFactory httpClientFactory,
                                      IBackgroundJobManager backgroundJobManager,
                                      INotificationDomainService notificationDomainService,
                                      IInvoiceDomainService invoiceDomainService,
                                      IPaypalService paypalService,
                                      IMobbexService mobbexService)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
            _subscriptionCycleRepository = subscriptionCycleRepository ?? throw new ArgumentNullException(nameof(subscriptionCycleRepository));
            _subscriptionCycleOrderRepository = subscriptionCycleOrderRepository ?? throw new ArgumentNullException(nameof(subscriptionCycleOrderRepository));
            _planPriceRepository = planPriceRepository ?? throw new ArgumentNullException(nameof(planPriceRepository));
            _subscriptionDomainService = subscriptionDomainService ?? throw new ArgumentNullException(nameof(subscriptionDomainService));
            _subscriptionCycleDomainService = subscriptionCycleDomainService ?? throw new ArgumentNullException(nameof(subscriptionCycleDomainService));
            _orderDomainService = orderDomainService ?? throw new ArgumentNullException(nameof(orderDomainService));
            _notificationDomainService = notificationDomainService ?? throw new ArgumentNullException(nameof(notificationDomainService));
            _invoiceDomainService = invoiceDomainService ?? throw new ArgumentNullException(nameof(invoiceDomainService));
            _noticationRepository = noticationRepository ?? throw new ArgumentNullException(nameof(noticationRepository));
            _clientOptions = clientOptions ?? throw new ArgumentNullException(nameof(clientOptions));
            _httpClient = httpClientFactory?.CreateClient() ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _backgroundJobManager = backgroundJobManager ?? throw new ArgumentNullException(nameof(backgroundJobManager));
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _mobbexService = mobbexService ?? throw new ArgumentNullException(nameof(mobbexService));
            _paypalService = paypalService ?? throw new ArgumentNullException(nameof(paypalService));

            _eventBus = EventBus.Default;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));


        }

        [UnitOfWork]
        public async Task HandleEventAsync(OrderSubscriptionPayedEventData eventData)
        {


            PlanPrice planPrice = _planPriceRepository.GetByOrder(eventData.Entity);

            IEnumerable<Order> paymentPendingOrders = _orderRepository.GetPendingPayments(planPrice.Plan, eventData.Entity.UserId);

            foreach (Order order in paymentPendingOrders)
            {

                Order orderToUpdate = null;
                SubscriptionCycleOrder subscriptionCycleOrder = null;
                SubscriptionCycle subscriptionCycle = null;
                Subscription subscription = null;


                if (order.Id == eventData.Entity.Id)
                {
                    orderToUpdate = _orderRepository.Get(eventData.Entity.Id);

                    subscriptionCycleOrder = _subscriptionCycleOrderRepository.GetAll().Single(x => x.OrderId == orderToUpdate.Id);
                    subscriptionCycle = _subscriptionCycleRepository.Get(subscriptionCycleOrder.SubscriptionCycleId);
                    subscription = _subscriptionRepository.Get(subscriptionCycle.SubscriptionId);

                    _orderDomainService.PayOrder(orderToUpdate);
                    _subscriptionDomainService.ActiveSubscription(subscription);
                    _subscriptionCycleDomainService.ActiveSubscriptionCycle(subscriptionCycle, DateTime.Now, planPrice.Plan.Duration);

                    Invoice invoice = _invoiceRepository.Single(x => x.OrderId == order.Id);
                    _invoiceDomainService.PayInvoice(invoice);
                    _invoiceRepository.Update(invoice);

                    Notification notification = _notificationDomainService.CreateNotification(orderToUpdate);
                    _notificationDomainService.SetOrderPayed(notification);
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
                }
                else
                {
                    orderToUpdate = _orderRepository.Get(order.Id);

                    subscriptionCycleOrder = _subscriptionCycleOrderRepository.GetAll().Single(x => x.OrderId == orderToUpdate.Id);
                    subscriptionCycle = _subscriptionCycleRepository.Get(subscriptionCycleOrder.SubscriptionCycleId);
                    subscription = _subscriptionRepository.Get(subscriptionCycle.SubscriptionId);

                    _subscriptionDomainService.CancelSubscription(subscription);
                    _subscriptionCycleDomainService.CancelSubscriptionCycle(subscriptionCycle);
                    _orderDomainService.CancelOrder(orderToUpdate);

                    Invoice invoice = _invoiceRepository.GetAllIncluding(x => x.InvocePaymentProviders).Single(x => x.OrderId == order.Id);
                    _invoiceDomainService.CancelInvoice(invoice);
                    _invoiceRepository.Update(invoice);

                    PaymentProvider.PaymentProviderValue paymentProviderValue = order.Currency.Code == Currency.CurrencyValue.USD ? PaymentProvider.PaymentProviderValue.Paypal : PaymentProvider.PaymentProviderValue.Mobbex;
                    InvoicePaymentProvider invoicePaymentProvider = invoice.InvocePaymentProviders.Single(x => x.PaymentProvider.Provider == paymentProviderValue);

                    switch (order.Currency.Code)
                    {
                        case Currency.CurrencyValue.ARS:
                            await _mobbexService.CancelInvoice(invoicePaymentProvider);
                            break;
                        case Currency.CurrencyValue.USD:
                            await _paypalService.CancelInvoice(invoicePaymentProvider);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }

                _subscriptionRepository.Update(subscription);
                _subscriptionCycleRepository.Update(subscriptionCycle);
                _orderRepository.Update(orderToUpdate);

            }
        }
    }
}
