using System;

using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

using Sales.Domain.ValueObjects.Invoices;

namespace Sales.Entities
{
    public class InvoceWebhook : Entity<Guid>, IHasCreationTime
    {
        public Guid InvocePaymentProviderId { get; set; }
        public DateTime CreationTime { get; set; }
        public InvoceWebhookStatus Status { get; set; }
        public virtual InvocePaymentProvider InvocePaymentProvider { get; set; }
    }
}
