using System;

using Abp.Domain.Entities;

using Sales.Domain.ValueObjects;

namespace Sales.Domain.Entities.Products
{
    public class ProductSalePrice : Entity<Guid>
    {
        public Guid ProductSaleId { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }

        public virtual ProductSale ProductSale { get; set; }
    }
}
