using System;
using System.Collections.Generic;
using System.Text;

using Abp.Domain.Repositories;

using Sales.Domain.Entities.Subscriptions;

namespace Sales.Domain.Repositories
{
    public interface ISubscriptionCycleRepository : IRepository<SubscriptionCycle, Guid>
    {
        IEnumerable<SubscriptionCycle> GetSoonExpires(DateTime today, int days);

    }
}
