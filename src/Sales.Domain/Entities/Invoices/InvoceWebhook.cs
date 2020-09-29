using System;

using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

using Sales.Domain.ValueObjects.Invoices;

namespace Sales.Domain.Entities.Invoices
{
    public class InvoiceWebhook : Entity<Guid>, IHasCreationTime
    {
        public Guid InvocePaymentProviderId { get; set; }
        public DateTime CreationTime { get; set; }
        public InvoiceWebhookStatus Status { get; set; }
        public virtual InvoicePaymentProvider InvocePaymentProvider { get; set; }
    }
}
