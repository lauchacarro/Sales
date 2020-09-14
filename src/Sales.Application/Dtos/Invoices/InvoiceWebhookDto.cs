using System;

using Abp.Application.Services.Dto;
using Abp.AutoMapper;

using Sales.Domain.Entities.Invoices;
using Sales.Domain.ValueObjects.Invoices;

namespace Sales.Application.Dtos.Invoices
{
    [AutoMap(typeof(InvoceWebhook))]
    public class InvoiceWebhookDto : EntityDto<Guid>
    {
        public Guid InvocePaymentProviderId { get; set; }
        public DateTime CreationTime { get; set; }
        public InvoceWebhookStatus Status { get; set; }
    }
}
