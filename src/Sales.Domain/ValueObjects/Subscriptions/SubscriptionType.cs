using System.Collections.Generic;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Subscriptions
{
    public class SubscriptionType : ValueObject
    {
        public enum SubscriptionTypeValue
        {
            Normal
        }
        public SubscriptionTypeValue Type { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Type;
        }
    }
}
