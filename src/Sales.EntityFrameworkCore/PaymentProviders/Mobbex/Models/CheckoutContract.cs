using System.Runtime.Serialization;

namespace Sales.EntityFrameworkCore.PaymentProviders.Mobbex.Models
{
    [DataContract]
    public class CheckoutContract
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        [DataMember(Name = "total")]
        public decimal Total { get; set; }

        [DataMember(Name = "created")]
        public string Created { get; set; }
    }
}
