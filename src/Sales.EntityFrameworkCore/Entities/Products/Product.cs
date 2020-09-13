﻿using System;
using System.Collections.Generic;

using Abp.Domain.Entities;

using Sales.Domain.ValueObjects.Products;

namespace Sales.Entities
{
    public class Product : Entity<Guid>
    {
        public Product()
        {
            Plans = new HashSet<Plan>();
            ProductSales = new HashSet<ProductSale>();
        }

        public string Name { get; set; }

        public ProductStatus Status { get; set; }
        public ProductType Type { get; set; }
        public virtual ICollection<Plan> Plans { get; set; }
        public virtual ICollection<ProductSale> ProductSales { get; set; }
    }
}
