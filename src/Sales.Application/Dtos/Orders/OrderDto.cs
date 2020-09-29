using System;

using Abp.Application.Services.Dto;
using Abp.AutoMapper;

using Sales.Domain.Entities.Orders;

namespace Sales.Application.Dtos.Orders
{
    [AutoMap(typeof(Order))]
    public class OrderDto : EntityDto<Guid>
    {
        public Guid UserId { get; set; }
        public string Currency { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public bool IsDeleted { get; set; }


        public string Status { get; set; }
        public string Type { get; set; }
    }
}
