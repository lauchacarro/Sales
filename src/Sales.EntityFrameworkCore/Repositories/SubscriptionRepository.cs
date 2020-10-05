using System;
using System.Collections.Generic;
using System.Linq;

using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;

using Microsoft.EntityFrameworkCore;

using Sales.Domain.Entities.Subscriptions;
using Sales.Domain.Repositories;
using Sales.Domain.ValueObjects.Orders;
using Sales.Domain.ValueObjects.Subscriptions;

namespace Sales.EntityFrameworkCore.Repositories
{
    public class SubscriptionRepository : EfCoreRepositoryBase<SalesDbContext, Subscription, Guid>, ISubscriptionRepository
    {
        public SubscriptionRepository(IDbContextProvider<SalesDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public IEnumerable<Subscription> GetActives(Guid userId)
        {
            var result = (from s in GetAll().AsNoTracking()
                          join sc in Context.SubscriptionCycles.AsNoTracking() on s.Id equals sc.SubscriptionId
                          join sco in Context.SubscriptionCycleOrders.AsNoTracking() on sc.Id equals sco.SubscriptionCycleId
                          join o in Context.Orders.AsNoTracking() on sco.OrderId equals o.Id
                          where s.Status.Status == SubscriptionStatus.SubscriptionStatusValue.Active &&
                          sc.Status.Status == SubscriptionCycleStatus.SubscriptionCycleStatusValue.Active &&
                          o.Status.Status == OrderStatus.OrderStatusValue.Payed &&
                          o.UserId == userId

                          select s).ToList();

            return result;
        }

        public Subscription GetSubscriptionPlanActive(Guid userId, Guid productId)
        {
            var result = (from s in GetAll().AsNoTracking()
                          join sc in Context.SubscriptionCycles.AsNoTracking() on s.Id equals sc.SubscriptionId
                          join sco in Context.SubscriptionCycleOrders.AsNoTracking() on sc.Id equals sco.SubscriptionCycleId
                          join o in Context.Orders.AsNoTracking() on sco.OrderId equals o.Id
                          join pl in Context.Plans.AsNoTracking() on s.PlanId equals pl.Id
                          where s.Status.Status == SubscriptionStatus.SubscriptionStatusValue.Active &&
                          sc.Status.Status == SubscriptionCycleStatus.SubscriptionCycleStatusValue.Active &&
                          o.Status.Status == OrderStatus.OrderStatusValue.Payed &&
                          o.UserId == userId &&
                          pl.ProductId == productId

                          select s).SingleOrDefault();

            return result;
        }

        public IEnumerable<Subscription> GetPaymentPendings(Guid userId, Guid productId)
        {
            var result = (from s in GetAll().AsNoTracking()
                          join sc in Context.SubscriptionCycles.AsNoTracking() on s.Id equals sc.SubscriptionId
                          join sco in Context.SubscriptionCycleOrders.AsNoTracking() on sc.Id equals sco.SubscriptionCycleId
                          join o in Context.Orders.AsNoTracking() on sco.OrderId equals o.Id
                          join pl in Context.Plans.AsNoTracking() on s.PlanId equals pl.Id
                          where s.Status.Status == SubscriptionStatus.SubscriptionStatusValue.Active &&
                          sc.Status.Status == SubscriptionCycleStatus.SubscriptionCycleStatusValue.PaymentPending &&
                          o.Status.Status == OrderStatus.OrderStatusValue.PaymentPending &&
                          o.UserId == userId &&
                          pl.ProductId == productId

                          select s).ToList();

            return result;
        }
    }
}
