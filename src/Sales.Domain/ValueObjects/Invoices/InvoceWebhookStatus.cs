using System.Collections.Generic;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Invoices
{
    public class InvoceWebhookStatus : ValueObject
    {
        public enum InvoceWebhookStatusValue
        {
            Done,
            Error
        }

        public InvoceWebhookStatusValue Status { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Status;
        }
    }
}
