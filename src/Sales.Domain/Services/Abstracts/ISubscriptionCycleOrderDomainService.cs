
using Abp.Domain.Services;

using Sales.Domain.Entities.Orders;
using Sales.Domain.Entities.Subscriptions;

namespace Sales.Domain.Services.Abstracts
{
    public interface ISubscriptionCycleOrderDomainService : IDomainService
    {
        SubscriptionCycleOrder CreateSubscriptionCycleOrder(SubscriptionCycle subscriptionCycle, Order order);
    }
}
