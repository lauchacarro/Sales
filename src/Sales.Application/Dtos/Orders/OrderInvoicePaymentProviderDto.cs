using System;
using System.Collections.Generic;
using System.Text;

using Sales.Application.Dtos.Invoices;

namespace Sales.Application.Dtos.Orders
{
    public class OrderInvoicePaymentProviderDto
    {
        public OrderDto Order { get; set; }
        public InvoicePaymentProviderDto InvoicePaymentProvider { get; set; }
    }
}
