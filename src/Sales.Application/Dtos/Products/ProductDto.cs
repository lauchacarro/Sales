using System;

using Abp.Application.Services.Dto;
using Abp.AutoMapper;

using Sales.Domain.Entities.Products;

namespace Sales.Application.Dtos.Products
{
    [AutoMap(typeof(Product))]
    public class ProductDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }
}
