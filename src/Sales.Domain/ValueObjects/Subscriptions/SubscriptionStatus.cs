using System.Collections.Generic;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Subscriptions
{
    public class SubscriptionStatus : ValueObject
    {
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
    }
}
