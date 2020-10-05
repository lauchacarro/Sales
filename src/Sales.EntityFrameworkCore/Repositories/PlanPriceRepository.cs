using System;
using System.Collections.Generic;
using System.Linq;

using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;

using Microsoft.EntityFrameworkCore;

using Sales.Domain.Entities.Orders;
using Sales.Domain.Entities.Plans;
using Sales.Domain.Repositories;
using Sales.Domain.ValueObjects;

namespace Sales.EntityFrameworkCore.Repositories
{
    public class PlanPriceRepository : EfCoreRepositoryBase<SalesDbContext, PlanPrice, Guid>, IPlanPriceRepository
    {
        public PlanPriceRepository(IDbContextProvider<SalesDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public PlanPrice GetByPlan(Guid planId, Currency currency)
        {
            return GetAll().SingleOrDefault(x => x.PlanId == planId && x.Currency.Code == currency.Code);
        }

        public IEnumerable<PlanPrice> GetByPlan(Guid planId)
        {
            return GetAll().Where(x => x.PlanId == planId).ToList();
        }

        public PlanPrice GetByOrder(Order order)
        {
            return (from pp in GetAll().AsNoTracking()
                    join p in Context.Plans.AsNoTracking() on pp.PlanId equals p.Id
                    join s in Context.Subscriptions.AsNoTracking() on p.Id equals s.PlanId
                    join sc in Context.SubscriptionCycles.AsNoTracking() on s.Id equals sc.SubscriptionId
                    join sco in Context.SubscriptionCycleOrders.AsNoTracking() on sc.Id equals sco.SubscriptionCycleId
                    join o in Context.Orders.AsNoTracking() on sco.OrderId equals o.Id
                    where o.Id == order.Id && o.Currency.Code == pp.Currency.Code

                    select pp).Include(x => x.Plan).SingleOrDefault();
        }


    }
}
