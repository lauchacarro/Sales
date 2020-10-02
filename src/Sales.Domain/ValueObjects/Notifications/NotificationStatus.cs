using System.Collections.Generic;
using System.Diagnostics;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Notifications
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class NotificationStatus : ValueObject
    {
        public NotificationStatus(NotificationStatusValue status)
        {
            Status = status;
        }

        public NotificationStatus()
        {
        }

        public enum NotificationStatusValue
        {
            Created,
            Processing,
            Error
        }

        public NotificationStatusValue Status { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Status;
        }

        private string GetDebuggerDisplay()
        {
            return Status.ToString();
        }
    }
}
