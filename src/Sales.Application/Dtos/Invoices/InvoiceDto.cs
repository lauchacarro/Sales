using System;

using Abp.Application.Services.Dto;
using Abp.AutoMapper;

using Sales.Domain.Entities.Invoices;
using Sales.Domain.ValueObjects.Invoices;

namespace Sales.Application.Dtos.Invoices
{
    [AutoMap(typeof(Invoice))]
    public class InvoiceDto : EntityDto<Guid>
    {
        public DateTime ExpirationDate { get; set; }
        public Guid OrderId { get; set; }
        public InvoiceStatus Status { get; set; }
        public InvoiceType Type { get; set; }
    }
}
