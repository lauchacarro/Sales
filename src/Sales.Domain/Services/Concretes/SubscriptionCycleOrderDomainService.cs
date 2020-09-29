
using Abp.Domain.Services;

using Sales.Domain.Entities.Orders;
using Sales.Domain.Entities.Subscriptions;
using Sales.Domain.Services.Abstracts;

namespace Sales.Domain.Services.Concretes
{
    public class SubscriptionCycleOrderDomainService : DomainService, ISubscriptionCycleOrderDomainService
    {
        public SubscriptionCycleOrder CreateSubscriptionCycleOrder(SubscriptionCycle subscriptionCycle, Order order)
        {
            return new SubscriptionCycleOrder
            {
                SubscriptionCycleId = subscriptionCycle.Id,
                OrderId = order.Id
            };
        }
    }
}
