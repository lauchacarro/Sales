using System.Collections.Generic;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Notifications
{
    public class NotificationType : ValueObject
    {
        public enum NotificationTypeValue
        {
            Normal
        }
        public NotificationTypeValue Type { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Type;
        }
    }
}
