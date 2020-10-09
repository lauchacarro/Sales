using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;

using Sales.Domain.Entities.Subscriptions;
using Sales.Domain.Repositories;
using Sales.Domain.ValueObjects.Subscriptions;

namespace Sales.EntityFrameworkCore.Repositories
{
    public class SubscriptionCycleRepository : EfCoreRepositoryBase<SalesDbContext, SubscriptionCycle, Guid>, ISubscriptionCycleRepository
    {
        public SubscriptionCycleRepository(IDbContextProvider<SalesDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public IEnumerable<SubscriptionCycle> GetSoonExpires(DateTime today, int days)
        {
            return GetAllIncluding(x => x.Subscription, x => x.Subscription.Plan, x => x.Subscription.Plan.PlanPrices)
                .Where(x => x.ExpirationDate > today && x.ExpirationDate <= today.AddDays(days) && x.Status.Status == SubscriptionCycleStatus.SubscriptionCycleStatusValue.Active)
                .ToList();
        }
    }
}
