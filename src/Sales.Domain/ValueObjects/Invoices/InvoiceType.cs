using System.Collections.Generic;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Invoices
{
    public class InvoiceType : ValueObject
    {
        public enum InvoiceTypeValue
        {
            Normal
        }

        public InvoiceTypeValue Type { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Type;
        }
    }
}
