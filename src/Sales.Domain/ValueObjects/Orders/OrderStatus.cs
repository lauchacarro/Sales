using System.Collections.Generic;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Orders
{
    public class OrderStatus : ValueObject
    {
        public enum OrderStatusValue
        {
            Created,
            PaymentPending,
            Payed,
            Expirated,
            Canceled
        }

        public OrderStatusValue Status { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Status;
        }
    }
}
