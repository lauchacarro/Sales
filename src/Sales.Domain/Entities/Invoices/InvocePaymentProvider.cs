using System;
using System.Collections.Generic;

using Abp.Domain.Entities;

namespace Sales.Domain.Entities.Invoices
{
    public class InvoicePaymentProvider : Entity<Guid>
    {
        public InvoicePaymentProvider()
        {
            InvoceWebhooks = new HashSet<InvoiceWebhook>();
        }

        public Guid InvoceId { get; set; }
        public string Transaction { get; set; }
        public Uri Link { get; set; }
        public PaymentProvider PaymentProvider { get; set; }
        public virtual Invoice Invoce { get; set; }
        public virtual ICollection<InvoiceWebhook> InvoceWebhooks { get; set; }
    }
}
