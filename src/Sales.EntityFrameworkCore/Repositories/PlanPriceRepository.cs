using System;
using System.Collections.Generic;
using System.Linq;

using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;

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
    }
}
