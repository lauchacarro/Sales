
using System;

using Abp.Domain.Services;

using Sales.Domain.Entities.Invoices;
using Sales.Domain.Entities.Orders;

namespace Sales.Domain.Services.Abstracts
{
    public interface IInvoiceDomainService : IDomainService
    {
        Invoice CreateInvoice(Order order, DateTime creationDate);
        void ActiveInvoice(Invoice invoice);
        bool IsExpirated(Invoice invoice, DateTime today);
        void ExpirateInvoice(Invoice invoice);
        void PayInvoice(Invoice invoice);
        void CancelInvoice(Invoice invoice);
    }
}
