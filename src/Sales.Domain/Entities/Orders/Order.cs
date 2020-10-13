using System;
using System.Collections.Generic;

using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

using Sales.Domain.Entities.Invoices;
using Sales.Domain.Entities.Notifications;
using Sales.Domain.Entities.Products;
using Sales.Domain.Entities.Subscriptions;
using Sales.Domain.Tools;
using Sales.Domain.ValueObjects;
using Sales.Domain.ValueObjects.Orders;

namespace Sales.Domain.Entities.Orders
{
    public class Order : Entity<Guid>, IHasUserId<Guid>, IHasCreationTime, IHasModificationTime
    {
        public Order()
        {
            Invoices = new HashSet<Invoice>();
            Notifications = new HashSet<Notification>();
            ProductSaleOrders = new HashSet<ProductSaleOrder>();
            SubscriptionCycleOrders = new HashSet<SubscriptionCycleOrder>();
        }

        public Guid UserId { get; set; }
        public Currency Currency { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }

        public OrderStatus Status { get; set; }
        public OrderType Type { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<ProductSaleOrder> ProductSaleOrders { get; set; }
        public virtual ICollection<SubscriptionCycleOrder> SubscriptionCycleOrders { get; set; }
    }
}
