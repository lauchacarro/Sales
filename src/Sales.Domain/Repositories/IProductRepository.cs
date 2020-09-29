using System;
using System.Collections.Generic;

using Abp.Domain.Repositories;

using Sales.Domain.Entities.Products;

namespace Sales.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        IEnumerable<Product> GetProductPlans();
        IEnumerable<Product> GetProductSales();
    }
}
