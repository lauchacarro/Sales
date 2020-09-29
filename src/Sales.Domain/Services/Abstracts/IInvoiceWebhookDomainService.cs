using System;

using Abp.Domain.Services;

using Sales.Domain.Entities.Invoices;

namespace Sales.Domain.Services.Abstracts
{
    public interface IInvoiceWebhookDomainService : IDomainService
    {
        InvoiceWebhook CreateInvoiceWebhook(InvoicePaymentProvider invoicePaymentProvider, DateTime datenow);
        void ChangeToError(InvoiceWebhook invoiceWebhook);
    }
}
