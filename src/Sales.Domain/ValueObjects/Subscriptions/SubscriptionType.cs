using System.Collections.Generic;
using System.Diagnostics;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Subscriptions
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class SubscriptionType : ValueObject
    {
        public SubscriptionType(SubscriptionTypeValue type)
        {
            Type = type;
        }

        public SubscriptionType()
        {
        }

        public enum SubscriptionTypeValue
        {
            Normal
        }
        public SubscriptionTypeValue Type { get; set; }

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
