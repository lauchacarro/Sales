using System.Collections.Generic;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Subscriptions
{
    public class SubscriptionCycleStatus : ValueObject
    {
        public enum SubscriptionCycleStatusValue
        {
            PaymentPending,
            Active,
            Finished,
            Canceled
        }

        public SubscriptionCycleStatusValue Status { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Status;
        }
    }
}
