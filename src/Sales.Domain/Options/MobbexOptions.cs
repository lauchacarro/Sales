using Sales.Domain.Enums;

namespace Sales.Domain.Options
{

    public interface IMobbexOptions : IPaymentProviderOptions
    {
    }

    public class MobbexOptions : IMobbexOptions
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public PaymentProviderEnvironment Environment { get; set; }
        public string ReturnUrl { get; set; }
        public string CancelUrl { get; set; }
    }
}
