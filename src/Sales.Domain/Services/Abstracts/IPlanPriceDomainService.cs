
using Abp.Domain.Services;

using Sales.Domain.Entities.Plans;
using Sales.Domain.ValueObjects;

namespace Sales.Domain.Services.Abstracts
{
    public interface IPlanPriceDomainService : IDomainService
    {
        PlanPrice CreatePlanPrice(Plan plan, Currency currency, decimal price);
    }
}
