using System;

using Abp.Application.Services.Dto;
using Abp.AutoMapper;

using Sales.Domain.Entities.Products;
using Sales.Domain.ValueObjects;

namespace Sales.Application.Dtos.Products
{

    [AutoMap(typeof(ProductSalePrice))]
    public class ProductSalePriceDto : EntityDto<Guid>
    {
        public Guid ProductSaleId { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
    }
}
