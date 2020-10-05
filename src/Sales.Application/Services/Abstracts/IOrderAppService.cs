using System;
using System.Collections.Generic;

using Abp.Application.Services;

using Sales.Application.Dtos.Orders;

namespace Sales.Application.Services.Abstracts
{
    public interface IOrderAppService : IApplicationService
    {
        OrderDto GetOrder(Guid id);
        IEnumerable<OrderDto> GetOrders();
    }
}
