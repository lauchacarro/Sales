using System.Collections.Generic;
using System.Diagnostics;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Invoices
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class InvoiceStatus : ValueObject
    {
        public InvoiceStatus(InvoiceStatusValue status)
        {
            Status = status;
        }

        public InvoiceStatus()
        {
        }

        public enum InvoiceStatusValue
        {
            Created,
            Active,
            Payed,
            Expirated,
            Canceled
        }

        public InvoiceStatusValue Status { get; set; }

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
