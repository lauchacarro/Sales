using System;

using Abp.Application.Services.Dto;
using Abp.AutoMapper;

using Sales.Domain.Entities.Invoices;

namespace Sales.Application.Dtos.Invoices
{
    [AutoMap(typeof(InvoiceWebhook))]
    public class InvoiceWebhookDto : EntityDto<Guid>
    {
        public Guid InvocePaymentProviderId { get; set; }
        public DateTime CreationTime { get; set; }
        public string Status { get; set; }
    }
}
