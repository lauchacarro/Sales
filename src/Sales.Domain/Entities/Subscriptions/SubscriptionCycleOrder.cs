using System;

using Abp.Domain.Entities;

using Sales.Domain.Entities.Orders;

namespace Sales.Domain.Entities.Subscriptions
{
    public class SubscriptionCycleOrder : Entity<Guid>
    {
        public Guid SubscriptionCycleId { get; set; }
        public Guid OrderId { get; set; }

        public virtual Order Order { get; set; }
        public virtual SubscriptionCycle SubscriptionCycle { get; set; }
    }
}
