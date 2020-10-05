using System;

using Abp.Events.Bus;
using Abp.Events.Bus.Entities;

using Sales.Domain.Entities.Notifications;

namespace Sales.Application.Events.SendNotificationEvent
{
    public class SendNotificationEventData : EventData
    {
        public SendNotificationEventData(Guid notificationId)
        {
            NotificationId = notificationId;
        }

        public Guid NotificationId { get; }
    }
}
