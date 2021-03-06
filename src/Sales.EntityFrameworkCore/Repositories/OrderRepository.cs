﻿using System;
using System.Collections.Generic;
using System.Linq;

using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;

using Microsoft.EntityFrameworkCore;

using Sales.Domain.Entities.Orders;
using Sales.Domain.Entities.Plans;
using Sales.Domain.Entities.Subscriptions;
using Sales.Domain.Repositories;
using Sales.Domain.ValueObjects.Orders;
using Sales.Domain.ValueObjects.Subscriptions;

namespace Sales.EntityFrameworkCore.Repositories
{
    public class OrderRepository : EfCoreRepositoryBase<SalesDbContext, Order, Guid>, IOrderRepository
    {
        public OrderRepository(IDbContextProvider<SalesDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Order GetBySubscriptionCycle(SubscriptionCycle subscriptionCycle)
        {
            var result = (from sc in Context.SubscriptionCycles.AsNoTracking()
                          join sco in Context.SubscriptionCycleOrders.AsNoTracking() on sc.Id equals sco.SubscriptionCycleId
                          join o in Context.Orders.AsNoTracking() on sco.OrderId equals o.Id
                          where sc.Id == subscriptionCycle.Id

                          select o).Single();

            return result;
        }

        public IEnumerable<Order> GetByUser(Guid userId)
        {
            return GetAll().Include(x => x.SubscriptionCycleOrders)
                .ThenInclude(x => x.SubscriptionCycle)
                .ThenInclude(x => x.Subscription)
                .Include(x => x.Invoices)
                .ThenInclude(x => x.InvocePaymentProviders)
                .ThenInclude(x => x.InvoceWebhooks)
                .Include(x => x.ProductSaleOrders)
                .Include(x => x.Notifications)
                .Where(x => x.UserId == userId).ToList();
        }

        public IEnumerable<Order> GetPendingPayments(Plan plan, Guid userId)
        {
            var result = (from s in Context.Subscriptions.AsNoTracking()
                          join sc in Context.SubscriptionCycles.AsNoTracking() on s.Id equals sc.SubscriptionId
                          join sco in Context.SubscriptionCycleOrders.AsNoTracking() on sc.Id equals sco.SubscriptionCycleId
                          join o in Context.Orders.AsNoTracking() on sco.OrderId equals o.Id
                          join pl in Context.Plans.AsNoTracking() on s.PlanId equals pl.Id
                          where s.Status.Status == SubscriptionStatus.SubscriptionStatusValue.Created &&
                          sc.Status.Status == SubscriptionCycleStatus.SubscriptionCycleStatusValue.PaymentPending &&
                          o.Status.Status == OrderStatus.OrderStatusValue.PaymentPending &&
                          o.UserId == userId &&
                          pl.ProductId == plan.ProductId

                          select o).ToList();

            return result;
        }
    }
}
