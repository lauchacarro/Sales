using System;
using System.Collections.Generic;

using Abp.Domain.Entities;

namespace Sales.Domain.Entities.Invoices
{
    public class InvocePaymentProvider : Entity<Guid>
    {
        public InvocePaymentProvider()
        {
            InvoceWebhooks = new HashSet<InvoceWebhook>();
        }

        public Guid InvoceId { get; set; }
        public Guid PaymentProviderId { get; set; }
        public string Transaction { get; set; }
        public string Link { get; set; }

        public virtual Invoice Invoce { get; set; }
        public virtual PaymentProvider PaymentProvider { get; set; }
        public virtual ICollection<InvoceWebhook> InvoceWebhooks { get; set; }
    }
}
