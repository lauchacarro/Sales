using System;

using Abp.Domain.Entities;

namespace Sales.Entities
{
    public class SubscriptionCycleOrder : Entity<Guid>
    {
        public Guid SubscriptionCycleId { get; set; }
        public Guid OrderId { get; set; }

        public virtual Order Order { get; set; }
        public virtual SubscriptionCycle SubscriptionCycle { get; set; }
    }
}
