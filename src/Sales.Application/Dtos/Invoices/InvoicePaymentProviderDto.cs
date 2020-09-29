using System;

using Abp.Application.Services.Dto;
using Abp.AutoMapper;

using Sales.Domain.Entities.Invoices;

namespace Sales.Application.Dtos.Invoices
{
    [AutoMap(typeof(InvoicePaymentProvider))]
    public class InvoicePaymentProviderDto : EntityDto<Guid>
    {
        public Guid InvoceId { get; set; }
        public string Transaction { get; set; }
        public Uri Link { get; set; }
        public string PaymentProvider { get; set; }
    }
}
