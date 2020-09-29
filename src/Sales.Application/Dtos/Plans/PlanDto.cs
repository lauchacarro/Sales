using System;

using Abp.Application.Services.Dto;
using Abp.AutoMapper;

using Sales.Domain.Entities.Plans;

namespace Sales.Application.Dtos.Plans
{
    [AutoMap(typeof(Plan))]
    public class PlanDto : EntityDto<Guid>
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Duration { get; set; }
    }
}
