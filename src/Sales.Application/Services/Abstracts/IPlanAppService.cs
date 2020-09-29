using System;
using System.Collections.Generic;

using Abp.Application.Services;

using Sales.Application.Dtos.Plans;

namespace Sales.Application.Services.Abstracts
{
    public interface IPlanAppService : IApplicationService
    {
        PlanPriceDto AddPrice(CreatePlanPriceInput input);

        IEnumerable<PlanDto> GetAllPlans();

        IEnumerable<PlanPriceDto> Get(Guid id);
    }
}
