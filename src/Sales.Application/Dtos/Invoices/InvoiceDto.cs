using System;

using Abp.Application.Services.Dto;
using Abp.AutoMapper;

using Sales.Domain.Entities.Invoices;

namespace Sales.Application.Dtos.Invoices
{
    [AutoMap(typeof(Invoice))]
    public class InvoiceDto : EntityDto<Guid>
    {
        public DateTime ExpirationDate { get; set; }
        public Guid OrderId { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }
}
