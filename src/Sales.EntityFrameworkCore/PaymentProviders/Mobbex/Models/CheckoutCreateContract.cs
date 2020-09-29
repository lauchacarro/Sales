using System.Runtime.Serialization;

namespace Sales.EntityFrameworkCore.PaymentProviders.Mobbex.Models
{
    [DataContract]
    public class CheckoutCreateContract
    {
        [DataMember(Name = "total")]
        public decimal Total { get; set; }

        [DataMember(Name = "currency")] //: "ARS",
        public string Currency { get; set; }

        [DataMember(Name = "reference")] //: "tIhbT6Zwc",
        public string Reference { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "return_url")] //: "https://tugimnasiaonline.com/es/cuenta/bienvenida",
        public string ReturnUrl { get; set; }

        [DataMember(Name = "webhook")] //: "https://tugimnasiaonline.com/es/suscripcion/notification",
        public string Webhook { get; set; }

        [DataMember(Name = "test")]
        public bool Test { get; set; }

        [DataMember(Name = "options")]
        public SubscriptionOptionsDto Options { get; set; }
    }
}
