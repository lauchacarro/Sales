using System;

using Abp.Application.Services.Dto;
using Abp.AutoMapper;

using Sales.Domain.Entities.Products;
using Sales.Domain.ValueObjects.Products;

namespace Sales.Application.Dtos.Products
{
    [AutoMap(typeof(Product))]
    public class ProductDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public ProductStatus Status { get; set; }
        public ProductType Type { get; set; }
    }
}
