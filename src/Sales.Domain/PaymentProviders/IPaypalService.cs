using System;
using System.Threading.Tasks;

namespace Sales.Domain.PaymentProviders
{
    public interface IPaypalService : IPaymentProviderService
    {
        Task<Guid> ConfirmOrder(string token);
    }
}
