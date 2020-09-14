
using Abp.Domain.Services;

using Sales.Domain.Entities.Plans;
using Sales.Domain.Entities.Products;
using Sales.Domain.Services.Abstracts;
using Sales.Domain.ValueObjects.Plans;

namespace Sales.Domain.Services.Concretes
{
    public class PlanDomainService : DomainService, IPlanDomainService
    {
        public void ActivePlan(Plan plan)
        {
            plan.Status = new PlanStatus(PlanStatus.PlanStatusValue.Active);
        }

        public void LinkPlanWithProduct(Plan plan, Product product)
        {
            plan.ProductId = product.Id;
        }
    }
}
