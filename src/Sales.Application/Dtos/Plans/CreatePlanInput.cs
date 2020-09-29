using System;

using Abp.AutoMapper;

using Sales.Domain.Entities.Plans;

namespace Sales.Application.Dtos.Plans
{
    [AutoMap(typeof(Plan))]
    public class CreatePlanInput
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Duration { get; set; }
    }
}
