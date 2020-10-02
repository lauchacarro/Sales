using System;

using Abp.Domain.Entities;

using Sales.Domain.Entities.Orders;
using Sales.Domain.ValueObjects.Notifications;

namespace Sales.Domain.Entities.Notifications
{
    public class Notification : Entity<Guid>
    {
        public Guid OrderId { get; set; }
        public int Attempts { get; set; }

        public NotificationType Type { get; set; }
        public NotificationStatus Status { get; set; }
        public virtual Order Order { get; set; }
    }
}
