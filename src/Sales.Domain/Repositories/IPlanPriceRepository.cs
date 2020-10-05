using System;
using System.Collections.Generic;

using Abp.Domain.Repositories;

using Sales.Domain.Entities.Orders;
using Sales.Domain.Entities.Plans;
using Sales.Domain.ValueObjects;

namespace Sales.Domain.Repositories
{
    public interface IPlanPriceRepository : IRepository<PlanPrice, Guid>
    {
        PlanPrice GetByPlan(Guid planId, Currency currency);
        IEnumerable<PlanPrice> GetByPlan(Guid planId);
        PlanPrice GetByOrder(Order order);
    }
}
