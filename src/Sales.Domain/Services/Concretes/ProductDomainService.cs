
using Abp.Domain.Services;

using Sales.Domain.Entities.Products;
using Sales.Domain.Services.Abstracts;
using Sales.Domain.ValueObjects;
using Sales.Domain.ValueObjects.Products;

namespace Sales.Domain.Services.Concretes
{
    public class ProductDomainService : DomainService, IProductDomainService
    {
        public void ActiveProduct(Product product)
        {
            product.Status = new ProductStatus(ProductStatus.ProductStatusValue.Active);
        }

        public ProductSalePrice AssingPrice(ProductSale productSale, decimal price, Currency currency)
        {
            return new ProductSalePrice
            {
                ProductSaleId = productSale.Id,
                Price = price,
                Currency = currency
            };
        }

        public ProductSale CreateProductSale(Product product)
        {
            return new ProductSale
            {
                ProductId = product.Id
            };
        }
    }
}
