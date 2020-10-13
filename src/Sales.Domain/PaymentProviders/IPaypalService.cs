using System;
using System.Threading.Tasks;

using Sales.Domain.Entities.Invoices;

namespace Sales.Domain.PaymentProviders
{
    public interface IPaypalService : IPaymentProviderService
    {
        Task<Guid> ConfirmOrder(string token);
    }
}
