using System;
using System.Collections.Generic;

using Abp.Domain.Entities;

using Sales.Domain.Entities.Products;
using Sales.Domain.Entities.Subscriptions;
using Sales.Domain.ValueObjects.Plans;

namespace Sales.Domain.Entities.Plans
{
    public class Plan : Entity<Guid>
    {
        public Plan()
        {
            PlanPrices = new HashSet<PlanPrice>();
            Subscriptions = new HashSet<Subscription>();
        }

        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Product Product { get; set; }
        public PlanStatus Status { get; set; }
        public PlanType Type { get; set; }
        public PlanCycleDuration Duration { get; set; }
        public virtual ICollection<PlanPrice> PlanPrices { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
