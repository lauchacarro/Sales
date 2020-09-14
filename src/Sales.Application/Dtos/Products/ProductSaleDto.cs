using System;

using Abp.Application.Services.Dto;
using Abp.AutoMapper;

using Sales.Domain.Entities.Products;

namespace Sales.Application.Dtos.Products
{
    [AutoMap(typeof(ProductSale))]
    public class ProductSaleDto : EntityDto<Guid>
    {
        public Guid ProductId { get; set; }
    }
}
