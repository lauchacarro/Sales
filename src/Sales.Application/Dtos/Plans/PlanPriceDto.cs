using System;

using Abp.Application.Services.Dto;
using Abp.AutoMapper;

using Sales.Domain.Entities.Plans;

namespace Sales.Application.Dtos.Plans
{
    [AutoMap(typeof(PlanPrice))]
    public class PlanPriceDto : EntityDto<Guid>
    {
        public Guid PlanId { get; set; }
        public PlanDto Plan { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
}
