using System;

using Abp.Domain.Entities;

using Sales.Domain.ValueObjects.Notifications;

namespace Sales.Entities
{
    public class Notification : Entity<Guid>
    {
        public Guid OrderId { get; set; }
        public int Attempts { get; set; }

        public NotificationType Type { get; set; }
        public virtual Order Order { get; set; }
    }
}
