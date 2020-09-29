
using Sales.Application.Dtos.Invoices;
using Sales.Application.Dtos.Orders;

namespace Sales.Application.Dtos.Subscriptions
{
    public class SubscriptionOrderDto
    {
        public SubscriptionDto Subscription { get; set; }

        public SubscriptionCycleDto SubscriptionCycle { get; set; }

        public SubscriptionCycleOrderDto SubscriptionCycleOrder { get; set; }

        public OrderDto Order { get; set; }
        public InvoicePaymentProviderDto InvoicePaymentProvider { get; set; }
    }
}
