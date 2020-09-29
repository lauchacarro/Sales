
using System;
using System.Threading.Tasks;

using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;

using Sales.Application.Dtos.Invoices;
using Sales.Application.Dtos.Orders;
using Sales.Application.Dtos.Subscriptions;
using Sales.Application.Extensions;
using Sales.Application.Services.Abstracts;
using Sales.Domain.Entities.Invoices;
using Sales.Domain.Entities.Orders;
using Sales.Domain.Entities.Plans;
using Sales.Domain.Entities.Subscriptions;
using Sales.Domain.PaymentProviders;
using Sales.Domain.Repositories;
using Sales.Domain.Services.Abstracts;

namespace Sales.Application.Services.Concretes
{
    public class SubscriptionAppService : ApplicationService, ISubscriptionAppService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IRepository<SubscriptionCycle, Guid> _subscriptionCycleRepository;
        private readonly IRepository<SubscriptionCycleOrder, Guid> _subscriptionCycleOrderRepository;
        private readonly IRepository<Plan, Guid> _planRepository;
        private readonly IPlanPriceRepository _planPriceRepository;
        private readonly IRepository<Order, Guid> _orderRepository;
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

        public SubscriptionAppService(ISubscriptionRepository subscriptionRepository,
                                      IRepository<SubscriptionCycle, Guid> subscriptionCycleRepository,
                                      IRepository<SubscriptionCycleOrder, Guid> subscriptionCycleOrderRepository,
                                      IRepository<Plan, Guid> planRepository,
                                      IPlanPriceRepository planPriceRepository,
                                      IRepository<Order, Guid> orderRepository,
                                      IRepository<Invoice, Guid> invoiceRepository,
                                      IRepository<InvoicePaymentProvider, Guid> invoicePaymentProviderRepository,
                                      IOrderDomainService orderDomainService,
                                      ISubscriptionDomainService subscriptionDomainService,
                                      ISubscriptionCycleDomainService subscriptionCycleDomainService,
                                      ISubscriptionCycleOrderDomainService subscriptionCycleOrderDomainService,
                                      IInvoiceDomainService invoiceDomainService,
                                      IObjectMapper mapper,
                                      IPaypalService paypalService,
                                      IMobbexService mobbexService)
        {
            _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
            _subscriptionCycleRepository = subscriptionCycleRepository ?? throw new ArgumentNullException(nameof(subscriptionCycleRepository));
            _subscriptionCycleOrderRepository = subscriptionCycleOrderRepository ?? throw new ArgumentNullException(nameof(subscriptionCycleOrderRepository));
            _planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
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
        }

        public async Task<SubscriptionOrderDto> CreateSubscription(CreateSubscriptionInput input)
        {
            PlanPrice planPrice = _planPriceRepository.Get(input.PlanPriceId);

            if (planPrice.IsNull()) throw new UserFriendlyException("No existe un PlanPrice con ese Id");

            Plan plan = _planRepository.Get(planPrice.PlanId);

            Subscription actualSubscriptions = _subscriptionRepository.GetSubscriptionPlanActive(input.UserId, plan.ProductId);

            if (actualSubscriptions.IsNotNull()) throw new UserFriendlyException("El usuario ya esta suscripto a un Plan del mismo producto.");



            Subscription subscription = _subscriptionDomainService.CreateSubscription(plan);
            _subscriptionDomainService.ActiveSubscription(subscription);
            subscription = _subscriptionRepository.Insert(subscription);



            SubscriptionCycle subscriptionCycle = _subscriptionCycleDomainService.CreateSubscriptionCycle(subscription, DateTime.Now);
            _subscriptionCycleDomainService.PaymentPendingSubscriptionCycle(subscriptionCycle);
            subscriptionCycle = _subscriptionCycleRepository.Insert(subscriptionCycle);


            Order order = _orderDomainService.CreateOrderForSubscription(planPrice, input.UserId, DateTime.Now);
            _orderDomainService.PaymentPendingOrder(order);
            order = _orderRepository.Insert(order);



            SubscriptionCycleOrder subscriptionCycleOrder = _subscriptionCycleOrderDomainService.CreateSubscriptionCycleOrder(subscriptionCycle, order);

            _subscriptionCycleOrderRepository.Insert(subscriptionCycleOrder);

            Invoice invoice = _invoiceDomainService.CreateInvoice(order, DateTime.Now);
            _invoiceDomainService.ActiveInvoice(invoice);
            _invoiceRepository.Insert(invoice);



            InvoicePaymentProvider invoicePaymentProvider = planPrice.Currency.Code switch
            {
                Domain.ValueObjects.Currency.CurrencyValue.USD => await _paypalService.CreateUriForPayment(invoice, order, plan),
                Domain.ValueObjects.Currency.CurrencyValue.ARS => await _mobbexService.CreateUriForPayment(invoice, order, plan),
                _ => throw new NotImplementedException()
            };

            invoicePaymentProvider = _invoicePaymentProviderRepository.Insert(invoicePaymentProvider);


            return new SubscriptionOrderDto
            {
                Subscription = _mapper.Map<SubscriptionDto>(subscription),
                SubscriptionCycle = _mapper.Map<SubscriptionCycleDto>(subscriptionCycle),
                SubscriptionCycleOrder = _mapper.Map<SubscriptionCycleOrderDto>(subscriptionCycleOrder),
                Order = _mapper.Map<OrderDto>(order),
                InvoicePaymentProvider = _mapper.Map<InvoicePaymentProviderDto>(invoicePaymentProvider),
            };
        }
    }
}
