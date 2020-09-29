using System.Collections.Generic;
using System.Diagnostics;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Orders
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class OrderStatus : ValueObject
    {
        public OrderStatus(OrderStatusValue status)
        {
            Status = status;
        }

        public OrderStatus()
        {
        }

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

        private string GetDebuggerDisplay()
        {
            return Status.ToString();
        }
    }
}
