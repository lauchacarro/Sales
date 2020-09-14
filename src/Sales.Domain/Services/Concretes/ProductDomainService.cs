using System;
using System.Collections.Generic;
using System.Text;

using Abp.Domain.Services;

using Sales.Domain.Entities.Products;
using Sales.Domain.Services.Abstracts;

namespace Sales.Domain.Services.Concretes
{
    public class ProductDomainService : DomainService, IProductDomainService
    {
        public void ActiveProduct(Product product)
        {
            product.Status = new ValueObjects.Products.ProductStatus(ValueObjects.Products.ProductStatus.ProductStatusValue.Active);
        }
    }
}
