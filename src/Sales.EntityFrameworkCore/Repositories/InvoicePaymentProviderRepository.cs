using System;
using System.Collections.Generic;
using System.Linq;

using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;

using Microsoft.EntityFrameworkCore;

using Sales.Domain.Entities.Invoices;
using Sales.Domain.Repositories;

namespace Sales.EntityFrameworkCore.Repositories
{
    public class InvoicePaymentProviderRepository : EfCoreRepositoryBase<SalesDbContext, InvoicePaymentProvider, Guid>, IInvoicePaymentProviderRepository
    {
        public InvoicePaymentProviderRepository(IDbContextProvider<SalesDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public InvoicePaymentProvider GetByOrder(Guid orderId)
        {
            var result = (from ipp in GetAll().AsNoTracking()
                          join i in Context.Invoices.AsNoTracking() on ipp.InvoceId equals i.Id
                          join o in Context.Orders.AsNoTracking() on i.OrderId equals o.Id
                          where o.Id == orderId
                          select ipp).SingleOrDefault();

            return result;
        }
    }
}
