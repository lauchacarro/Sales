using System;
using System.Linq;
using System.Threading.Tasks;

using Abp.Application.Services;
using Abp.BackgroundJobs;
using Abp.Domain.Repositories;

using Microsoft.AspNetCore.Mvc;

using Sales.Application.Services.Abstracts;
using Sales.Domain.Entities.Invoices;
using Sales.Domain.Entities.Notifications;
using Sales.Domain.Entities.Orders;
using Sales.Domain.Entities.Plans;
using Sales.Domain.Entities.Subscriptions;
using Sales.Domain.PaymentProviders;
using Sales.Domain.Services.Abstracts;
using Sales.Domain.ValueObjects.Orders;

namespace Sales.Application.Services.Concretes
{
    public class InvoiceWebhookAppService : ApplicationService, IInvoiceWebhookAppService
    {
        private readonly IRepository<Invoice, Guid> _invoiceRepository;
        private readonly IRepository<InvoiceWebhook, Guid> _invoiceWebhookRepository;
        private readonly IRepository<InvoicePaymentProvider, Guid> _invoicePaymentProviderRepository;
        private readonly IRepository<Order, Guid> _orderRepository;
        private readonly IRepository<SubscriptionCycleOrder, Guid> _subscriptionCycleOrderRepository;
        private readonly IRepository<SubscriptionCycle, Guid> _subscriptionCycleRepository;
        private readonly IRepository<Subscription, Guid> _subscriptionRepository;
        private readonly IRepository<Plan, Guid> _planRepository;
        private readonly IRepository<Notification, Guid> _noticationRepository;
        private readonly IInvoiceDomainService _invoiceDomainService;
        private readonly IOrderDomainService _orderDomainService;
        private readonly ISubscriptionCycleDomainService _subscriptionCycleDomainService;
        private readonly IInvoiceWebhookDomainService _invoiceWebhookDomainService;
        private readonly IPaypalService _paypalService;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly INotificationDomainService _notificationDomainService;

        public InvoiceWebhookAppService(IRepository<Invoice, Guid> invoiceRepository,
                                        IRepository<InvoiceWebhook, Guid> invoiceWebhookRepository,
                                        IRepository<InvoicePaymentProvider, Guid> invoicePaymentProviderRepository,
                                        IRepository<Order, Guid> orderRepository,
                                        IRepository<SubscriptionCycleOrder, Guid> subscriptionCycleOrderRepository,
                                        IRepository<SubscriptionCycle, Guid> subscriptionCycleRepository,
                                        IRepository<Subscription, Guid> subscriptionRepository,
                                        IRepository<Plan, Guid> planRepository,
                                        IRepository<Notification, Guid> noticationRepository,
                                        IInvoiceDomainService invoiceDomainService,
                                        IOrderDomainService orderDomainService,
                                        ISubscriptionCycleDomainService subscriptionCycleDomainService,
                                        IInvoiceWebhookDomainService invoiceWebhookDomainService,
                                        IPaypalService paypalService,
                                        IBackgroundJobManager backgroundJobManager,
                                        INotificationDomainService notificationDomainService)
        {
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _invoiceWebhookRepository = invoiceWebhookRepository ?? throw new ArgumentNullException(nameof(invoiceWebhookRepository));
            _invoicePaymentProviderRepository = invoicePaymentProviderRepository ?? throw new ArgumentNullException(nameof(invoicePaymentProviderRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _subscriptionCycleOrderRepository = subscriptionCycleOrderRepository ?? throw new ArgumentNullException(nameof(subscriptionCycleOrderRepository));
            _subscriptionCycleRepository = subscriptionCycleRepository ?? throw new ArgumentNullException(nameof(subscriptionCycleRepository));
            _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
            _planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
            _noticationRepository = noticationRepository ?? throw new ArgumentNullException(nameof(noticationRepository));
            _invoiceDomainService = invoiceDomainService ?? throw new ArgumentNullException(nameof(invoiceDomainService));
            _orderDomainService = orderDomainService ?? throw new ArgumentNullException(nameof(orderDomainService));
            _subscriptionCycleDomainService = subscriptionCycleDomainService ?? throw new ArgumentNullException(nameof(subscriptionCycleDomainService));
            _invoiceWebhookDomainService = invoiceWebhookDomainService ?? throw new ArgumentNullException(nameof(invoiceWebhookDomainService));
            _paypalService = paypalService ?? throw new ArgumentNullException(nameof(paypalService));
            _backgroundJobManager = backgroundJobManager ?? throw new ArgumentNullException(nameof(backgroundJobManager));
            _notificationDomainService = notificationDomainService ?? throw new ArgumentNullException(nameof(notificationDomainService));
        }

        [RemoteService(false)]

        public async Task WebhookPaypal([FromQuery] string token)
        {
            Guid invoiceId = await _paypalService.ConfirmOrder(token);
            InvoicePaymentProvider invoicePaymentProvider = _invoicePaymentProviderRepository.Single(x => x.InvoceId == invoiceId && x.Transaction == token);
            PayOrder(invoicePaymentProvider);
        }

        [RemoteService(false)]

        public void WebhookMobbex([FromQuery] Guid invoiceId, [FromQuery] int status, [FromQuery] string transactionId)
        {
            if (status == 200)
            {
                InvoicePaymentProvider invoicePaymentProvider = _invoicePaymentProviderRepository.Single(x => x.InvoceId == invoiceId && x.PaymentProvider.Provider == PaymentProvider.PaymentProviderValue.Mobbex);
                PayOrder(invoicePaymentProvider);
            }
        }

        private void PayOrder(InvoicePaymentProvider invoicePaymentProvider)
        {
            InvoiceWebhook invoiceWebhook = _invoiceWebhookDomainService.CreateInvoiceWebhook(invoicePaymentProvider, DateTime.Now);

            try
            {
                Invoice invoice = _invoiceRepository.GetAllIncluding(x => x.Order).Single(x => x.Id == invoicePaymentProvider.InvoceId);

                if (invoice.Order.Status.Status == OrderStatus.OrderStatusValue.PaymentPending)
                {

                    _invoiceDomainService.PayInvoice(invoice);
                    _orderDomainService.PayOrder(invoice.Order);

                    _invoiceRepository.Update(invoice);

                    if (invoice.Order.Type.Type == OrderType.OrderTypeValue.Subscription)
                    {
                        SubscriptionCycleOrder subsubscriptionCycleOrder = _subscriptionCycleOrderRepository.GetAll().Single(x => x.OrderId == invoice.Order.Id);

                        SubscriptionCycle subsubscriptionCycle = _subscriptionCycleRepository.Get(subsubscriptionCycleOrder.SubscriptionCycleId);

                        Subscription subsubscription = _subscriptionRepository.Get(subsubscriptionCycle.SubscriptionId);

                        Plan plan = _planRepository.Get(subsubscription.PlanId);

                        _subscriptionCycleDomainService.ActiveSubscriptionCycle(subsubscriptionCycle, DateTime.Now, plan.Duration);
                        _subscriptionCycleRepository.Update(subsubscriptionCycle);

                        Notification notification = _notificationDomainService.CreateNotification(invoice.Order);
                        _notificationDomainService.SetOrderPayed(notification);
                        _noticationRepository.Insert(notification);
                    }
                }
            }
            catch (Exception)
            {
                _invoiceWebhookDomainService.ChangeToError(invoiceWebhook);
            }
            finally
            {
                _invoiceWebhookRepository.Insert(invoiceWebhook);
            }
        }
    }
}
