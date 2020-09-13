using System.Collections.Generic;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Notifications
{
    public class NotificationStatus : ValueObject
    {
        public enum NotificationTypeValue
        {
            Normal
        }

        public NotificationTypeValue Status { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Status;
        }
    }
}
