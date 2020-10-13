using System;
using System.Collections.Generic;
using System.Text;

using Abp.Domain.Repositories;

using Sales.Domain.Entities.Invoices;

namespace Sales.Domain.Repositories
{
    public interface IInvoicePaymentProviderRepository : IRepository<InvoicePaymentProvider, Guid>
    {
        InvoicePaymentProvider GetByOrder(Guid orderId);
    }
}
