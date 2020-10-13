using System.Threading.Tasks;

using Abp.Dependency;

using Sales.Domain.Entities.Invoices;
using Sales.Domain.Entities.Orders;
using Sales.Domain.Entities.Plans;
using Sales.Domain.Entities.Products;

namespace Sales.Domain.PaymentProviders
{
    public interface IPaymentProviderService : ITransientDependency
    {
        Task<InvoicePaymentProvider> CreateUriForPayment(Invoice invoice, Order order, Plan plan);
        Task<InvoicePaymentProvider> CreateUriForPayment(Invoice invoice, Order order, ProductSale productSale);
        Task<InvoicePaymentProvider> CreateUriForPayment(Invoice invoice, Order order, string description);
        Task CancelInvoice(InvoicePaymentProvider invoice);
        Task<InvoicePaymentProvider> UpdateAmountInvoice(InvoicePaymentProvider invoicePaymentProvider, Domain.Entities.Orders.Order order);
    }
}
