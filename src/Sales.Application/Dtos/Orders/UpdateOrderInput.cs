using System;

using Abp.AutoMapper;

using Sales.Domain.Entities.Orders;

namespace Sales.Application.Dtos.Orders
{
    [AutoMap(typeof(Order))]
    public class UpdateOrderInput
    {
        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
}
