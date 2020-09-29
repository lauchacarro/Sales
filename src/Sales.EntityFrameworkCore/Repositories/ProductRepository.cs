using System;
using System.Collections.Generic;
using System.Linq;

using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;

using Microsoft.EntityFrameworkCore;

using Sales.Domain.Entities.Products;
using Sales.Domain.Repositories;

namespace Sales.EntityFrameworkCore.Repositories
{
    public class ProductRepository : EfCoreRepositoryBase<SalesDbContext, Product, Guid>, IProductRepository
    {
        public ProductRepository(IDbContextProvider<SalesDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public IEnumerable<Product> GetProductPlans()
        {
            var result = (from pr in GetAll().AsNoTracking()
                          join pl in Context.Plans.AsNoTracking() on pr.Id equals pl.ProductId
                          select pr).Distinct().ToList();

            return result;
        }

        public IEnumerable<Product> GetProductSales()
        {
            var result = (from pr in GetAll().AsNoTracking()
                          join ps in Context.ProductSales.AsNoTracking() on pr.Id equals ps.ProductId
                          select pr).Distinct().ToList();

            return result;
        }
    }
}
