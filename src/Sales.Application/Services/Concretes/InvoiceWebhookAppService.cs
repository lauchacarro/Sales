using System;
using System.Linq;
using System.Threading.Tasks;

using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Events.Bus;

using Microsoft.AspNetCore.Mvc;

using Sales.Application.Events.Orders.OrderRenewSubscriptionPayedEvent;
using Sales.Application.Events.Orders.OrderSubscriptionPayedEvent;
using Sales.Application.Services.Abstracts;
using Sales.Domain.Entities.Invoices;
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
        private readonly IInvoiceDomainService _invoiceDomainService;
        private readonly IOrderDomainService _orderDomainService;
        private readonly IInvoiceWebhookDomainService _invoiceWebhookDomainService;
        private readonly IPaypalService _paypalService;
        private readonly IEventBus _eventBus;

        public InvoiceWebhookAppService(IRepository<Invoice, Guid> invoiceRepository,
                                        IRepository<InvoiceWebhook, Guid> invoiceWebhookRepository,
                                        IRepository<InvoicePaymentProvider, Guid> invoicePaymentProviderRepository,
                                        IOrderDomainService orderDomainService,
                                        IInvoiceDomainService invoiceDomainService,
                                        IInvoiceWebhookDomainService invoiceWebhookDomainService,
                                        IPaypalService paypalService)
        {
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _invoiceWebhookRepository = invoiceWebhookRepository ?? throw new ArgumentNullException(nameof(invoiceWebhookRepository));
            _invoiceDomainService = invoiceDomainService ?? throw new ArgumentNullException(nameof(invoiceDomainService));
            _invoicePaymentProviderRepository = invoicePaymentProviderRepository ?? throw new ArgumentNullException(nameof(invoicePaymentProviderRepository));
            _orderDomainService = orderDomainService ?? throw new ArgumentNullException(nameof(orderDomainService));
            _invoiceWebhookDomainService = invoiceWebhookDomainService ?? throw new ArgumentNullException(nameof(invoiceWebhookDomainService));
            _paypalService = paypalService ?? throw new ArgumentNullException(nameof(paypalService));
            _eventBus = EventBus.Default;
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

                    _orderDomainService.PayOrder(invoice.Order);

                    switch (invoice.Order.Type.Type)
                    {
                        case OrderType.OrderTypeValue.Subscription:
                            _eventBus.Trigger(new OrderSubscriptionPayedEventData(invoice.Order));
                            break;
                        case OrderType.OrderTypeValue.RenewSubscription:
                            _eventBus.Trigger(new OrderRenewSubscriptionPayedEventData(invoice.Order));
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Error("Ocurrio un error al Pagar una Orden.", ex);
                _invoiceWebhookDomainService.ChangeToError(invoiceWebhook);
            }
            finally
            {
                _invoiceWebhookRepository.Insert(invoiceWebhook);
            }
        }
    }
}
