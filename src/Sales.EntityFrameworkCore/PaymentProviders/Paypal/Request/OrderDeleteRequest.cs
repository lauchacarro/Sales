using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

using PayPalHttp;

namespace Sales.EntityFrameworkCore.PaymentProviders.Paypal.Request
{
    public class OrderDeleteRequest : HttpRequest
    {
        public OrderDeleteRequest(string OrderId) : base("/v1/checkout/orders/{order_id}?", HttpMethod.Delete, typeof(void))
        {
            try
            {
                this.Path = this.Path.Replace("{order_id}", Uri.EscapeDataString(Convert.ToString(OrderId)));
            }
            catch (IOException) { }

            this.ContentType = "application/json";
        }
    }
}
