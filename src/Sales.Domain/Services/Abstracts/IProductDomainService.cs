
using Abp.Domain.Services;

using Sales.Domain.Entities.Products;

namespace Sales.Domain.Services.Abstracts
{
    public interface IProductDomainService : IDomainService
    {
        void ActiveProduct(Product product);
    }
}
