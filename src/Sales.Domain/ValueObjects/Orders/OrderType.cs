using System.Collections.Generic;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Orders
{
    public class OrderType : ValueObject
    {
        public enum OrderTypeValue
        {
            Subscription,
            Sales
        }
        public OrderTypeValue Type { get; set; }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Type;
        }
    }
}
