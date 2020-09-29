using Abp.AutoMapper;

using Sales.Domain.Entities.Products;

namespace Sales.Application.Dtos.Products
{
    [AutoMap(typeof(Product))]
    public class CreateProductInput
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
