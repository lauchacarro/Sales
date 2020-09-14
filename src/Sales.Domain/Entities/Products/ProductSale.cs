using System;
using System.Collections.Generic;

using Abp.Domain.Entities;

namespace Sales.Domain.Entities.Products
{
    public class ProductSale : Entity<Guid>
    {
        public ProductSale()
        {
            ProductSaleOrders = new HashSet<ProductSaleOrder>();
            ProductSalePrices = new HashSet<ProductSalePrice>();
        }

        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<ProductSaleOrder> ProductSaleOrders { get; set; }
        public virtual ICollection<ProductSalePrice> ProductSalePrices { get; set; }
    }
}
