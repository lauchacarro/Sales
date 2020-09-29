using System;
using System.Collections.Generic;

using Abp.Domain.Repositories;

using Sales.Domain.Entities.Subscriptions;

namespace Sales.Domain.Repositories
{
    public interface ISubscriptionRepository : IRepository<Subscription, Guid>
    {
        IEnumerable<Subscription> GetActives(Guid userId);
        Subscription GetSubscriptionPlanActive(Guid userId, Guid productId);
    }
}
