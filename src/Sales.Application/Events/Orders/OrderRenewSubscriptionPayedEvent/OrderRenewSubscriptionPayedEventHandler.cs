using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.Events.Bus.Handlers;

using AutoMapper;

using Sales.Application.Dtos.Notifications;
using Sales.Domain.Entities.Invoices;
using Sales.Domain.Entities.Notifications;
using Sales.Domain.Entities.Orders;
using Sales.Domain.Entities.Plans;
using Sales.Domain.Entities.Subscriptions;
using Sales.Domain.Options;
using Sales.Domain.Repositories;
using Sales.Domain.Services.Abstracts;

namespace Sales.Application.Events.Orders.OrderRenewSubscriptionPayedEvent
{
    public class OrderRenewSubscriptionPayedEventHandler : IAsyncEventHandler<OrderRenewSubscriptionPayedEventData>, ITransientDependency
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IRepository<SubscriptionCycle, Guid> _subscriptionCycleRepository;
        private readonly IRepository<SubscriptionCycleOrder, Guid> _subscriptionCycleOrderRepository;
        private readonly IPlanPriceRepository _planPriceRepository;
        private readonly IRepository<Notification, Guid> _noticationRepository;
        private readonly ISubscriptionDomainService _subscriptionDomainService;
        private readonly ISubscriptionCycleDomainService _subscriptionCycleDomainService;
        private readonly IOrderDomainService _orderDomainService;
        private readonly INotificationDomainService _notificationDomainService;
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;
        private readonly IClientOptions _clientOptions;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IInvoiceDomainService _invoiceDomainService;
        private readonly IRepository<Invoice, Guid> _invoiceRepository;
        private readonly HttpClient _httpClient;

        public OrderRenewSubscriptionPayedEventHandler(IOrderRepository orderRepository,
                                      ISubscriptionRepository subscriptionRepository,
                                      IRepository<SubscriptionCycle, Guid> subscriptionCycleRepository,
                                      IRepository<SubscriptionCycleOrder, Guid> subscriptionCycleOrderRepository,
                                      IPlanPriceRepository planPriceRepository,
                                      ISubscriptionDomainService subscriptionDomainService,
                                      ISubscriptionCycleDomainService subscriptionCycleDomainService,
                                      IOrderDomainService orderDomainService,
                                      IRepository<Notification, Guid> noticationRepository,
                                      IMapper mapper,
                                      IClientOptions clientOptions,
                                      IHttpClientFactory httpClientFactory,
                                      IBackgroundJobManager backgroundJobManager,
                                      IRepository<Invoice, Guid> invoiceRepository,
                                      IInvoiceDomainService invoiceDomainService,
                                      INotificationDomainService notificationDomainService)
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
            _noticationRepository = noticationRepository ?? throw new ArgumentNullException(nameof(noticationRepository));
            _clientOptions = clientOptions ?? throw new ArgumentNullException(nameof(clientOptions));
            _httpClient = httpClientFactory?.CreateClient() ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _backgroundJobManager = backgroundJobManager ?? throw new ArgumentNullException(nameof(backgroundJobManager));
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _invoiceDomainService = invoiceDomainService ?? throw new ArgumentNullException(nameof(invoiceDomainService));
            _eventBus = EventBus.Default;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));


        }
        public async Task HandleEventAsync(OrderRenewSubscriptionPayedEventData eventData)
        {
            Order orderToUpdate = _orderRepository.Get(eventData.Entity.Id);

            PlanPrice planPrice = _planPriceRepository.GetByOrder(eventData.Entity);

            SubscriptionCycleOrder subscriptionCycleOrder = null;
            SubscriptionCycle subscriptionCycle = null;

            subscriptionCycleOrder = _subscriptionCycleOrderRepository.GetAll().Single(x => x.OrderId == orderToUpdate.Id);
            subscriptionCycle = _subscriptionCycleRepository.Get(subscriptionCycleOrder.SubscriptionCycleId);

            _subscriptionCycleDomainService.ActiveSubscriptionCycle(subscriptionCycle, DateTime.Now, planPrice.Plan.Duration);
            _subscriptionCycleRepository.Update(subscriptionCycle);

            Invoice invoice = _invoiceRepository.GetAllIncluding(x => x.InvocePaymentProviders).Single(x => x.OrderId == eventData.Entity.Id);
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
    }
}
