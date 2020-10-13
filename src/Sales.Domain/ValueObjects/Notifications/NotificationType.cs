using System.Collections.Generic;
using System.Diagnostics;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Notifications
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class NotificationType : ValueObject
    {
        public NotificationType(NotificationTypeValue type)
        {
            Type = type;
        }

        public enum NotificationTypeValue
        {
            Normal,
            OrderPayed,
            NewSubscribeCycle
        }
        public NotificationTypeValue Type { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Type;
        }

        private string GetDebuggerDisplay()
        {
            return Type.ToString();
        }
    }
}
