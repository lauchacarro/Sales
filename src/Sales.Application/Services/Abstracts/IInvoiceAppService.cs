
using System;

using Abp.Application.Services;

using Sales.Application.Dtos.Invoices;

namespace Sales.Application.Services.Abstracts
{
    public interface IInvoiceAppService : IApplicationService
    {
        InvoicePaymentProviderDto GetInvoicePaymentProviderByOrder(Guid orderId);
    }
}
