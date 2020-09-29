using System.Collections.Generic;
using System.Diagnostics;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Invoices
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class InvoiceType : ValueObject
    {
        public InvoiceType(InvoiceTypeValue type)
        {
            Type = type;
        }

        public InvoiceType()
        {
        }

        public enum InvoiceTypeValue
        {
            Normal
        }

        public InvoiceTypeValue Type { get; set; }

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
