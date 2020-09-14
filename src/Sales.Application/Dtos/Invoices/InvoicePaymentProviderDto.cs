using System;

using Abp.Application.Services.Dto;
using Abp.AutoMapper;

using Sales.Domain.Entities.Invoices;

namespace Sales.Application.Dtos.Invoices
{
    [AutoMap(typeof(InvocePaymentProvider))]
    public class InvoicePaymentProviderDto : EntityDto<Guid>
    {
        public Guid InvoceId { get; set; }
        public Guid PaymentProviderId { get; set; }
        public string Transaction { get; set; }
        public string Link { get; set; }
    }
}
