using System.Collections.Generic;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Invoices
{
    public class InvoiceStatus : ValueObject
    {
        public enum InvoiceStatusValue
        {
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
    }
}
