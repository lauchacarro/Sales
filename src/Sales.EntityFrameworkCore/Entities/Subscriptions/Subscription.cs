using System;
using System.Collections.Generic;

using Abp.Domain.Entities;

using Sales.Domain.ValueObjects.Subscriptions;

namespace Sales.Entities
{
    public class Subscription : Entity<Guid>
    {
        public Subscription()
        {
            SubscriptionCycles = new HashSet<SubscriptionCycle>();
        }

        public Guid PlanId { get; set; }

        public virtual Plan Plan { get; set; }
        public SubscriptionStatus Status { get; set; }
        public SubscriptionType Type { get; set; }
        public virtual ICollection<SubscriptionCycle> SubscriptionCycles { get; set; }
    }
}
