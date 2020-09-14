using System;
using System.Collections.Generic;

using Abp.Domain.Entities;

using Sales.Domain.ValueObjects.Subscriptions;

namespace Sales.Domain.Entities.Subscriptions
{
    public class SubscriptionCycle : Entity<Guid>
    {
        public SubscriptionCycle()
        {
            SubscriptionCycleOrders = new HashSet<SubscriptionCycleOrder>();
        }

        public Guid SubscriptionId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ActivationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public SubscriptionCycleStatus Status { get; set; }
        public virtual Subscription Subscription { get; set; }
        public virtual ICollection<SubscriptionCycleOrder> SubscriptionCycleOrders { get; set; }
    }
}
