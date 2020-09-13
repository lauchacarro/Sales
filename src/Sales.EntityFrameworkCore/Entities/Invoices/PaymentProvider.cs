using System;
using System.Collections.Generic;

using Abp.Domain.Entities;

namespace Sales.Entities
{
    public class PaymentProvider : Entity<Guid>
    {
        public PaymentProvider()
        {
            InvocePaymentProviders = new HashSet<InvocePaymentProvider>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<InvocePaymentProvider> InvocePaymentProviders { get; set; }
    }
}
