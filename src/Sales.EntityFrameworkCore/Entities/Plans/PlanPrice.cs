using System;

using Abp.Domain.Entities;

namespace Sales.Entities
{
    public class PlanPrice : Entity<Guid>
    {
        public Guid PlanId { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }

        public virtual Plan Plan { get; set; }
    }
}
