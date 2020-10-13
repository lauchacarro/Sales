using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.Events.Bus.Handlers;

using AutoMapper;

using Sales.Domain.Entities.Invoices;
using Sales.Domain.Entities.Notifications;
using Sales.Domain.Entities.Orders;
using Sales.Domain.Entities.Subscriptions;
using Sales.Domain.Options;
using Sales.Domain.Repositories;
using Sales.Domain.Services.Abstracts;

namespace Sales.Application.Events.Orders.OrderExtraPayedEvent
{
    public class OrderExtraPayedEventHandler : IAsyncEventHandler<OrderExtraPayedEventData>, ITransientDependency
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
        private readonly IRepository<Invoice, Guid> _invoiceRepository;
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;
        private readonly IInvoiceDomainService _invoiceDomainService;
        private readonly IClientOptions _clientOptions;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly HttpClient _httpClient;

        public OrderExtraPayedEventHandler(IOrderRepository orderRepository,
                                      ISubscriptionRepository subscriptionRepository,
                                      IRepository<SubscriptionCycle, Guid> subscriptionCycleRepository,
                                      IRepository<SubscriptionCycleOrder, Guid> subscriptionCycleOrderRepository,
                                      IPlanPriceRepository planPriceRepository,
                                      IRepository<Invoice, Guid> invoiceRepository,
                                      ISubscriptionDomainService subscriptionDomainService,
                                      ISubscriptionCycleDomainService subscriptionCycleDomainService,
                                      IOrderDomainService orderDomainService,
                                      IRepository<Notification, Guid> noticationRepository,
                                      IMapper mapper,
                                      IClientOptions clientOptions,
                                      IHttpClientFactory httpClientFactory,
                                      IInvoiceDomainService invoiceDomainService,
                                      IBackgroundJobManager backgroundJobManager,
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
        public async Task HandleEventAsync(OrderExtraPayedEventData eventData)
        {
            Invoice invoice = _invoiceRepository.GetAllIncluding(x => x.InvocePaymentProviders).Single(x => x.OrderId == eventData.Entity.Id);
            _invoiceDomainService.PayInvoice(invoice);
            _invoiceRepository.Update(invoice);
        }
    }
}
