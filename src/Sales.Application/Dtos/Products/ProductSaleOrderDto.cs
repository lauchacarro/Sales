using System;

using Abp.Application.Services.Dto;
using Abp.AutoMapper;

using Sales.Domain.Entities.Products;

namespace Sales.Application.Dtos.Products
{
    [AutoMap(typeof(ProductSaleOrder))]
    public class ProductSaleOrderDto : EntityDto<Guid>
    {
        public Guid ProductSaleId { get; set; }
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
    }
}
