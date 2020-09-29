using System.Runtime.Serialization;

namespace Sales.EntityFrameworkCore.PaymentProviders.Mobbex.Models
{
    [DataContract]
    public class Message<T>
    {
        [DataMember(Name = "result")]
        public bool Result { get; set; }

        [DataMember(Name = "data")]
        public T Data { get; set; }
    }
}
