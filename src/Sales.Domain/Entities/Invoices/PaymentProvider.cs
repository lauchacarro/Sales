using System;
using System.Collections.Generic;

using Abp.Domain.Entities;

namespace Sales.Domain.Entities.Invoices
{
    public class PaymentProvider : Entity<Guid>
    {
        public PaymentProvider()
        {
            InvocePaymentProviders = new HashSet<InvocePaymentProvider>();
        }

        public string Name { get; set; }

        public virtual ICollection<InvocePaymentProvider> InvocePaymentProviders { get; set; }
    }
}
