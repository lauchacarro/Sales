
using Abp.Application.Services;

using Microsoft.AspNetCore.Mvc;

using Sales.Application.Dtos.Products;

namespace Sales.Application.Services.Abstracts
{
    public interface IProductAppService : IApplicationService
    {
        [HttpPost]
        ProductDto CreateProductPlan(CreateProductPlanInput input);
    }
}
