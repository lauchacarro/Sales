using System;
using System.Runtime.Serialization;

namespace Sales.EntityFrameworkCore.PaymentProviders.Mobbex.Models
{
    public class MobbexWebhookModel
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "data")]
        public WhData Data { get; set; }
    }

    [DataContract]
    public class WhData
    {
        [DataMember(Name = "result")]
        public bool Result { get; set; }

        [DataMember(Name = "view")]
        public WhView View { get; set; }

        [DataMember(Name = "payment")]
        public WhPayment Payment { get; set; }

        [DataMember(Name = "user")]
        public WhUser User { get; set; }

        [DataMember(Name = "source")]
        public WhSource Source { get; set; }

        [DataMember(Name = "subscription")]
        public WhSubscription Subscription { get; set; }

        [DataMember(Name = "subscriber")]
        public WhSubscriber Subscriber { get; set; }
    }

    [DataContract]
    public class WhSubscription
    {
        [DataMember(Name = "uid")]
        public string Uid { get; set; }

        [DataMember(Name = "interval")]
        public string Interval { get; set; }

        [DataMember(Name = "limit")]
        public int Limit { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }

    [DataContract]
    public class WhSubscriber
    {
        [DataMember(Name = "uid")]
        public string Uid { get; set; }

        [DataMember(Name = "reference")]
        public string Reference { get; set; }

        [DataMember(Name = "customer")]
        public WhCustomer Customer { get; set; }
    }

    [DataContract]
    public class WhCustomer
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "identification")]
        public string Identification { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }
    }

    [DataContract]
    public class WhView
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        //    "options": {
        //        "barcode": true
        //    }
    }

    [DataContract]
    public class WhPayment
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "operation")]
        public WhOperation Operation { get; set; }

        [DataMember(Name = "status")]
        public WhStatus Status { get; set; }

        [DataMember(Name = "total")]
        public decimal Total { get; set; }

        [DataMember(Name = "currency")]
        public WhCurrency Currency { get; set; }

        [DataMember(Name = "created")]
        public DateTime Created { get; set; }

        [DataMember(Name = "updated")]
        public DateTime Updated { get; set; }

        [DataMember(Name = "reference")]
        public string Reference { get; set; }

        [DataMember(Name = "source")]
        public WhSource Source { get; set; }
    }

    [DataContract]
    public class WhUser
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }
    }

    [DataContract]
    public class WhInstallment
    {
        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        [DataMember(Name = "count")]
        public string Count { get; set; }
    }

    [DataContract]
    public class WhSource
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "number")]
        public string Number { get; set; }

        [DataMember(Name = "installment")]
        public string WhInstallment { get; set; }

        [DataMember(Name = "barcode")]
        public string Barcode { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }
    }

    [DataContract]
    public class WhOperation
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }

    [DataContract]
    public class WhStatus
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }
    }

    [DataContract]
    public class WhCurrency
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "symbol")]
        public string Symbol { get; set; }
    }
}
