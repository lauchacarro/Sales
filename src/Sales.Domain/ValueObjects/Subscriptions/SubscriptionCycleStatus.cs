using System.Collections.Generic;
using System.Diagnostics;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Subscriptions
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class SubscriptionCycleStatus : ValueObject
    {
        public SubscriptionCycleStatus(SubscriptionCycleStatusValue status)
        {
            Status = status;
        }

        public SubscriptionCycleStatus()
        {
        }

        public enum SubscriptionCycleStatusValue
        {
            PaymentPending,
            Created,
            Active,
            Finished,
            Canceled
        }

        public SubscriptionCycleStatusValue Status { get; set; }

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
