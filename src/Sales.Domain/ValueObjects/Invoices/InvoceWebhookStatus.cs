using System.Collections.Generic;
using System.Diagnostics;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Invoices
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class InvoiceWebhookStatus : ValueObject
    {
        public InvoiceWebhookStatus(InvoiceWebhookStatusValue status)
        {
            Status = status;
        }

        public InvoiceWebhookStatus()
        {
        }

        public enum InvoiceWebhookStatusValue
        {
            Done,
            Error
        }

        public InvoiceWebhookStatusValue Status { get; set; }

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
