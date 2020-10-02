
using Abp.Domain.Services;

using Sales.Domain.Entities.Notifications;
using Sales.Domain.Entities.Orders;
using Sales.Domain.Services.Abstracts;
using Sales.Domain.ValueObjects.Notifications;

namespace Sales.Domain.Services.Concretes
{
    public class NotificationDomainService : DomainService, INotificationDomainService
    {
        public void AddAttempt(Notification notification)
        {
            notification.Attempts++;
            if (notification.Attempts > 3)
                notification.Status.Status = NotificationStatus.NotificationStatusValue.Error;
        }

        public Notification CreateNotification(Order order)
        {
            return new Notification
            {
                OrderId = order.Id,
                Type = new NotificationType(NotificationType.NotificationTypeValue.Normal),
                Status = new NotificationStatus(NotificationStatus.NotificationStatusValue.Created)
            };
        }

        public void Processing(Notification notification)
        {
            notification.Status.Status = NotificationStatus.NotificationStatusValue.Processing;
        }

        public void SetError(Notification notification)
        {
            notification.Status.Status = NotificationStatus.NotificationStatusValue.Error;
        }

        public void SetOrderPayed(Notification notification)
        {
            notification.Type.Type = NotificationType.NotificationTypeValue.OrderPayed;
        }
    }
}
