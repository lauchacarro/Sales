
using Abp.Domain.Services;

using Sales.Domain.Entities.Plans;
using Sales.Domain.Services.Abstracts;
using Sales.Domain.ValueObjects;

namespace Sales.Domain.Services.Concretes
{
    public class PlanPriceDomainService : DomainService, IPlanPriceDomainService
    {
        public PlanPrice CreatePlanPrice(Plan plan, Currency currency, decimal price)
        {
            return new PlanPrice
            {
                Currency = currency,
                Price = price,
                PlanId = plan.Id
            };
        }
    }
}
