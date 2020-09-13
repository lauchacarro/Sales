﻿using System;
using System.Collections.Generic;

using Abp.Domain.Entities;

using Sales.Domain.ValueObjects.Invoices;

namespace Sales.Entities
{
    public class Invoice : Entity<Guid>
    {
        public Invoice()
        {
            InvocePaymentProviders = new HashSet<InvocePaymentProvider>();
        }

        public DateTime ExpirationDate { get; set; }
        public Guid OrderId { get; set; }

        public virtual Order Order { get; set; }
        public InvoiceStatus Status { get; set; }
        public InvoiceType Type { get; set; }
        public virtual ICollection<InvocePaymentProvider> InvocePaymentProviders { get; set; }
    }
}
