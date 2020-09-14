using System;

using Abp.Application.Services.Dto;
using Abp.AutoMapper;

using Sales.Domain.Entities.Plans;
using Sales.Domain.ValueObjects;

namespace Sales.Application.Dtos.Plans
{
    [AutoMap(typeof(Plan))]
    public class PlanPriceDto : EntityDto<Guid>
    {
        public Guid PlanId { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }

    }
}
