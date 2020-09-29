
using Abp.Domain.Services;

using Sales.Domain.Entities.Products;
using Sales.Domain.ValueObjects;

namespace Sales.Domain.Services.Abstracts
{
    public interface IProductDomainService : IDomainService
    {
        void ActiveProduct(Product product);
        ProductSale CreateProductSale(Product product);
        ProductSalePrice AssingPrice(ProductSale productSale, decimal price, Currency currency);
    }
}
