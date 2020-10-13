using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.ObjectMapping;
using Abp.UI;

using Microsoft.EntityFrameworkCore;

using Sales.Application.Dtos.Invoices;
using Sales.Application.Dtos.Orders;
using Sales.Application.Events.Orders.OrderExtraPayedEvent;
using Sales.Application.Events.Orders.OrderRenewSubscriptionPayedEvent;
using Sales.Application.Events.Orders.OrderSubscriptionPayedEvent;
using Sales.Application.Services.Abstracts;
using Sales.Domain.Entities.Invoices;
using Sales.Domain.Entities.Orders;
using Sales.Domain.PaymentProviders;
using Sales.Domain.Services.Abstracts;
using Sales.Domain.ValueObjects.Orders;

namespace Sales.Application.Services.Concretes
{
    public class OrderAppService : ApplicationService, IOrderAppService
    {
        private readonly IRepository<Order, Guid> _repository;
        private readonly IRepository<Invoice, Guid> _invoiceRepository;
        private readonly IRepository<InvoicePaymentProvider, Guid> _invoicePaymentProviderRepository;
        private readonly IInvoiceDomainService _invoiceDomainService;
        private readonly IObjectMapper _mapper;
        private readonly IPaypalService _paypalService;
        private readonly IMobbexService _mobbexService;
        private readonly IEventBus _eventBus;

        public OrderAppService(IRepository<Order, Guid> repository, IRepository<Invoice, Guid> invoiceRepository, IRepository<InvoicePaymentProvider, Guid> invoicePaymentProviderRepository, IInvoiceDomainService invoiceDomainService, IObjectMapper mapper, IPaypalService paypalService, IMobbexService mobbexService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _invoicePaymentProviderRepository = invoicePaymentProviderRepository ?? throw new ArgumentNullException(nameof(invoicePaymentProviderRepository));
            _invoiceDomainService = invoiceDomainService ?? throw new ArgumentNullException(nameof(invoiceDomainService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _paypalService = paypalService ?? throw new ArgumentNullException(nameof(paypalService));
            _mobbexService = mobbexService ?? throw new ArgumentNullException(nameof(mobbexService));
            _eventBus = EventBus.Default;
        }

        public async Task<OrderDto> UpdateOrder(UpdateOrderInput input)
        {
            Order order = _repository.GetAll().Include(x => x.Invoices).ThenInclude(x => x.InvocePaymentProviders).Single(x => x.Id == input.Id);

            if (order.Status.Status == OrderStatus.OrderStatusValue.Payed)
            {
                throw new UserFriendlyException("No se puede editar una orden que ya fue Pagada.");
            }

            if (order.Status.Status == OrderStatus.OrderStatusValue.Canceled)
            {
                throw new UserFriendlyException("No se puede editar una orden que ya fue Camcelada.");
            }

            order = _mapper.Map<UpdateOrderInput, Order>(input, order);

            _repository.Update(order);

            var invocePaymentProvider = order.Invoices.Single().InvocePaymentProviders.Single();

            if (order.Status.Status == OrderStatus.OrderStatusValue.Payed)
            {
                switch (order.Type.Type)
                {
                    case OrderType.OrderTypeValue.Subscription:
                        _eventBus.Trigger(new OrderSubscriptionPayedEventData(order));
                        break;
                    case OrderType.OrderTypeValue.RenewSubscription:
                        _eventBus.Trigger(new OrderRenewSubscriptionPayedEventData(order));
                        break;
                    case OrderType.OrderTypeValue.Extra:
                        _eventBus.Trigger(new OrderExtraPayedEventData(order));
                        break;
                    default:
                        throw new NotImplementedException();
                }


                switch (order.Currency.Code)
                {
                    case Domain.ValueObjects.Currency.CurrencyValue.ARS:
                        await _mobbexService.CancelInvoice(invocePaymentProvider);
                        break;
                    case Domain.ValueObjects.Currency.CurrencyValue.USD:
                        await _paypalService.CancelInvoice(invocePaymentProvider);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            if (order.Status.Status == OrderStatus.OrderStatusValue.PaymentPending)
            {
                switch (order.Currency.Code)
                {
                    case Domain.ValueObjects.Currency.CurrencyValue.ARS:
                        await _mobbexService.CancelInvoice(invocePaymentProvider);
                        var newinvoicePaymentProvider = await _mobbexService.CreateUriForPayment(order.Invoices.Single(), order, string.Empty);
                        invocePaymentProvider.Link = newinvoicePaymentProvider.Link;
                        invocePaymentProvider.Transaction = newinvoicePaymentProvider.Transaction;
                        break;
                    case Domain.ValueObjects.Currency.CurrencyValue.USD:
                        invocePaymentProvider = await _paypalService.UpdateAmountInvoice(invocePaymentProvider, order);
                        break;
                    default:
                        throw new NotImplementedException();
                }

                _invoicePaymentProviderRepository.Update(invocePaymentProvider);
            }

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateOrder(CreateOrderInput input)
        {
            Order order = _mapper.Map<Order>(input);

            _repository.Insert(order);

            Invoice invoice = _invoiceDomainService.CreateInvoice(order, DateTime.Now);
            _invoiceDomainService.ActiveInvoice(invoice);
            _invoiceRepository.Insert(invoice);

            InvoicePaymentProvider invoicePaymentProvider = order.Currency.Code switch
            {
                Domain.ValueObjects.Currency.CurrencyValue.USD => await _paypalService.CreateUriForPayment(invoice, order, input.Description),
                Domain.ValueObjects.Currency.CurrencyValue.ARS => await _mobbexService.CreateUriForPayment(invoice, order, input.Description),
                _ => throw new NotImplementedException()
            };

            invoicePaymentProvider = _invoicePaymentProviderRepository.Insert(invoicePaymentProvider);

            return _mapper.Map<OrderDto>(order);
        }

        public OrderDto GetOrder(Guid id)
        {
            return _mapper.Map<OrderDto>(_repository.Single(x => x.Id == id));
        }

        public IEnumerable<OrderDto> GetOrders()
        {
            return _repository.GetAll().Select(x => _mapper.Map<OrderDto>(x)).ToList();
        }

        public IEnumerable<OrderInvoicePaymentProviderDto> GetOrdersByUser(Guid userId)
        {
            IEnumerable<Order> orders = _repository.GetAll().Include(x => x.Invoices).ThenInclude(x => x.InvocePaymentProviders).Where(x => x.UserId == userId).ToList();

            List<OrderInvoicePaymentProviderDto> orderInvoicePaymentProviderDtos = new List<OrderInvoicePaymentProviderDto>();
            foreach (Order order in orders)
            {
                orderInvoicePaymentProviderDtos.Add(new OrderInvoicePaymentProviderDto
                {
                    Order = _mapper.Map<OrderDto>(order),
                    InvoicePaymentProvider = _mapper.Map<InvoicePaymentProviderDto>(order.Invoices.Single().InvocePaymentProviders.Single())
                });

            }
            return orderInvoicePaymentProviderDtos;
        }
    }
}
