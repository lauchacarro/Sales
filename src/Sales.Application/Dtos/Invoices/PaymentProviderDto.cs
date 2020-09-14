using System;

using Abp.Application.Services.Dto;
using Abp.AutoMapper;

using Sales.Domain.Entities.Invoices;

namespace Sales.Application.Dtos.Invoices
{
    [AutoMap(typeof(PaymentProvider))]
    public class PaymentProviderDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
