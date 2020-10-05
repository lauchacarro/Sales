using System;
using System.Collections.Generic;

using Abp.Domain.Repositories;

using Sales.Domain.Entities.Orders;
using Sales.Domain.Entities.Plans;

namespace Sales.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
        IEnumerable<Order> GetPendingPayments(Plan plan, Guid userId);
    }
}
