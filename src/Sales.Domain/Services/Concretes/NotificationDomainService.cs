
using Abp.Domain.Services;

using Sales.Domain.Entities.Notifications;
using Sales.Domain.Entities.Orders;
using Sales.Domain.Services.Abstracts;

namespace Sales.Domain.Services.Concretes
{
    public class NotificationDomainService : DomainService, INotificationDomainService
    {
        public void AddAttempt(Notification notification)
        {
            notification.Attempts++;
        }

        public Notification CreateNotification(Order order)
        {
            return new Notification
            {
                OrderId = order.Id,
                Type = new ValueObjects.Notifications.NotificationType(ValueObjects.Notifications.NotificationType.NotificationTypeValue.Normal)
            };
        }

        public void SetOrderPayed(Notification notification)
        {
            notification.Type.Type = ValueObjects.Notifications.NotificationType.NotificationTypeValue.OrderPayed;
        }
    }
}
