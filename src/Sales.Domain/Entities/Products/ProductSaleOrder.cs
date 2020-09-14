using System;

using Abp.Domain.Entities;

using Sales.Domain.Entities.Orders;

namespace Sales.Domain.Entities.Products
{
    public class ProductSaleOrder : Entity<Guid>
    {
        public Guid ProductSaleId { get; set; }
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual ProductSale ProductSale { get; set; }
    }
}
