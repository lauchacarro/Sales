using System;

using Abp.Domain.Services;

using Sales.Domain.Entities.Invoices;
using Sales.Domain.Services.Abstracts;
using Sales.Domain.ValueObjects.Invoices;

namespace Sales.Domain.Services.Concretes
{
    public class InvoiceWebhookDomainService : DomainService, IInvoiceWebhookDomainService
    {
        public InvoiceWebhook CreateInvoiceWebhook(InvoicePaymentProvider invoicePaymentProvider, DateTime datenow)
        {
            return new InvoiceWebhook
            {
                CreationTime = datenow,
                InvocePaymentProviderId = invoicePaymentProvider.Id,
                Status = new InvoiceWebhookStatus(InvoiceWebhookStatus.InvoiceWebhookStatusValue.Done)
            };
        }

        public void ChangeToError(InvoiceWebhook invoiceWebhook)
        {
            invoiceWebhook.Status.Status = InvoiceWebhookStatus.InvoiceWebhookStatusValue.Error;
        }
    }
}
