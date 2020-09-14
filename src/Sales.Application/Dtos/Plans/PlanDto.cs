using System;

using Abp.Application.Services.Dto;
using Abp.AutoMapper;

using Sales.Domain.Entities.Plans;
using Sales.Domain.ValueObjects.Plans;

namespace Sales.Application.Dtos.Plans
{
    [AutoMap(typeof(Plan))]
    public class PlanDto : EntityDto<Guid>
    {
        public Guid ProductId { get; set; }
        public PlanStatus Status { get; set; }
        public PlanType Type { get; set; }
        public PlanCycleDuration Duration { get; set; }
    }
}
