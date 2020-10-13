using System.Collections.Generic;
using System.Diagnostics;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Orders
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class OrderType : ValueObject
    {
        public OrderType(OrderTypeValue type)
        {
            Type = type;
        }

        public OrderType()
        {
        }

        public enum OrderTypeValue
        {
            Subscription,
            RenewSubscription,
            Sales,
            Extra
        }
        public OrderTypeValue Type { get; set; }
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
