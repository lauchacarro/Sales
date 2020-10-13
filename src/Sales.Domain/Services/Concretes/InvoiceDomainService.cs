using System;

using Abp.Domain.Services;

using Sales.Domain.Entities.Invoices;
using Sales.Domain.Entities.Orders;
using Sales.Domain.Services.Abstracts;

namespace Sales.Domain.Services.Concretes
{
    public class InvoiceDomainService : DomainService, IInvoiceDomainService
    {
        public void ActiveInvoice(Invoice invoice)
        {
            invoice.Status.Status = ValueObjects.Invoices.InvoiceStatus.InvoiceStatusValue.Active;
        }

        public void CancelInvoice(Invoice invoice)
        {
            invoice.Status.Status = ValueObjects.Invoices.InvoiceStatus.InvoiceStatusValue.Canceled;
        }

        public Invoice CreateInvoice(Order order, DateTime creationDate)
        {
            return new Invoice
            {
                OrderId = order.Id,
                ExpirationDate = creationDate.AddDays(10),
                Status = new ValueObjects.Invoices.InvoiceStatus(ValueObjects.Invoices.InvoiceStatus.InvoiceStatusValue.Created),
                Type = new ValueObjects.Invoices.InvoiceType(ValueObjects.Invoices.InvoiceType.InvoiceTypeValue.Normal)
            };
        }

        public void ExpirateInvoice(Invoice invoice)
        {
            invoice.Status.Status = ValueObjects.Invoices.InvoiceStatus.InvoiceStatusValue.Expirated;
        }

        public bool IsExpirated(Invoice invoice, DateTime today)
        {
            throw new NotImplementedException();
        }

        public void PayInvoice(Invoice invoice)
        {
            invoice.Status.Status = ValueObjects.Invoices.InvoiceStatus.InvoiceStatusValue.Payed;
        }
    }
}
