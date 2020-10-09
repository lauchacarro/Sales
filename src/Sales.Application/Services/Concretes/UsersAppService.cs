using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;

using Sales.Domain.Entities.Invoices;
using Sales.Domain.Entities.Notifications;
using Sales.Domain.Entities.Orders;
using Sales.Domain.Entities.Plans;
using Sales.Domain.Entities.Products;
using Sales.Domain.Entities.Subscriptions;
using Sales.Domain.Repositories;

namespace Sales.Application.Services.Concretes
{
    public class UsersAppService : ApplicationService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ISubscriptionCycleRepository _subscriptionCycleRepository;
        private readonly IRepository<SubscriptionCycleOrder, Guid> _subscriptionCycleOrderRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IRepository<Invoice, Guid> _invoiceRepository;
        private readonly IRepository<InvoicePaymentProvider, Guid> _invoicePaymentProviderRepository;
        private readonly IRepository<InvoiceWebhook, Guid> _invoiceWebhookRepository;
        private readonly IRepository<Notification, Guid> _notificationRepository;
        private readonly IRepository<ProductSaleOrder, Guid> _productSaleOrderRepository;

        public UsersAppService(ISubscriptionRepository subscriptionRepository,
                               ISubscriptionCycleRepository subscriptionCycleRepository,
                               IRepository<SubscriptionCycleOrder, Guid> subscriptionCycleOrderRepository,
                               IOrderRepository orderRepository,
                               IRepository<Invoice, Guid> invoiceRepository,
                               IRepository<ProductSaleOrder, Guid> productSaleOrderRepository,
                               IRepository<InvoicePaymentProvider, Guid> invoicePaymentProviderRepository,
                               IRepository<InvoiceWebhook, Guid> invoiceWebhookRepository,
                               IRepository<Notification, Guid> notificationRepository)
        {
            _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
            _subscriptionCycleRepository = subscriptionCycleRepository ?? throw new ArgumentNullException(nameof(subscriptionCycleRepository));
            _subscriptionCycleOrderRepository = subscriptionCycleOrderRepository ?? throw new ArgumentNullException(nameof(subscriptionCycleOrderRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _invoicePaymentProviderRepository = invoicePaymentProviderRepository ?? throw new ArgumentNullException(nameof(invoicePaymentProviderRepository));
            _invoiceWebhookRepository = invoiceWebhookRepository ?? throw new ArgumentNullException(nameof(invoiceWebhookRepository));
            _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
            _productSaleOrderRepository = productSaleOrderRepository ?? throw new ArgumentNullException(nameof(productSaleOrderRepository));
        }


        public void Delete(Guid userId)
        {
            IEnumerable<Order> orders = _orderRepository.GetByUser(userId);

            foreach (Order order in orders)
            {
                foreach (Notification notification in order.Notifications)
                {

                    _notificationRepository.Delete(x => x.Id == notification.Id);

                }

                foreach (Invoice invoice in order.Invoices)
                {
                    foreach (InvoicePaymentProvider invocePaymentProvider in invoice.InvocePaymentProviders)
                    {

                        _invoiceWebhookRepository.Delete(x => x.InvocePaymentProviderId == invocePaymentProvider.Id);


                        _invoicePaymentProviderRepository.Delete(x => x.Id == invocePaymentProvider.Id);

                    }


                    _invoiceRepository.Delete(x => x.Id == invoice.Id);

                }

                foreach (ProductSaleOrder productSaleOrder in order.ProductSaleOrders)
                {

                    _productSaleOrderRepository.Delete(x => x.Id == productSaleOrder.Id);

                }

                _subscriptionCycleOrderRepository.Delete(x => x.OrderId == order.Id);


                _subscriptionCycleRepository.Delete(x => x.SubscriptionId == order.SubscriptionCycleOrders.First().SubscriptionCycle.SubscriptionId);


                _subscriptionRepository.Delete(x => x.Id == order.SubscriptionCycleOrders.First().SubscriptionCycle.SubscriptionId);


                _orderRepository.Delete(x => x.Id == order.Id);

            }
        }
    }
}
