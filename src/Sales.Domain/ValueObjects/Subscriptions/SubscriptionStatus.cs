using System.Collections.Generic;
using System.Diagnostics;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Subscriptions
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class SubscriptionStatus : ValueObject
    {
        public SubscriptionStatus(SubscriptionStatusValue status)
        {
            Status = status;
        }

        public SubscriptionStatus()
        {
        }

        public enum SubscriptionStatusValue
        {
            Created,
            Active,
            Cancelated,
            Suspended,
            Finished
        }
        public SubscriptionStatusValue Status { get; set; }

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
