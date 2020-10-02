using Sales.Domain.Enums;

namespace Sales.Domain.Options
{
    public interface IPaypalOptions : IPaymentProviderOptions
    {

    }

    public class PaypalOptions : IPaypalOptions
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public PaymentProviderEnvironment Environment { get; set; }
        public string ReturnUrl { get; set; }
        public string CancelUrl { get; set; }
        public string WebhookUrl { get; set; }
    }
}
