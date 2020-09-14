
using Abp.Domain.Services;

using Sales.Domain.Entities.Plans;
using Sales.Domain.Entities.Products;

namespace Sales.Domain.Services.Abstracts
{
    public interface IPlanDomainService : IDomainService
    {
        void ActivePlan(Plan plan);
        void LinkPlanWithProduct(Plan plan, Product product);
    }
}
