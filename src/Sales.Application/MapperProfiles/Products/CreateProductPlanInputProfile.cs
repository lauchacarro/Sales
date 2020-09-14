
using AutoMapper;

using Sales.Application.Dtos.Products;
using Sales.Application.Extensions;
using Sales.Domain.Entities.Plans;
using Sales.Domain.Entities.Products;
using Sales.Domain.ValueObjects.Plans;
using Sales.Domain.ValueObjects.Products;

namespace Sales.Application.MapperProfiles.Products
{
    public class CreateProductPlanInputProfile : Profile
    {
        public CreateProductPlanInputProfile()
        {
            CreateMap<CreateProductPlanInput, Product>()
                      .ForMember(u => u.Name, options => options.MapFrom(input => input.Name))
                      .ForMember(u => u.Status, options => options.MapFrom(input => new ProductStatus(ProductStatus.ProductStatusValue.Created)))
                      .ForMember(u => u.Type, options => options.MapFrom(input => new ProductType(ProductType.ProductTypeValue.Plan)));

            CreateMap<CreateProductPlanInput, Plan>()
                      .ForMember(u => u.Type, options => options.MapFrom(input => new PlanType(PlanType.PlanTypeValue.Normal)))
                      .ForMember(u => u.Status, options => options.MapFrom(input => new PlanStatus(PlanStatus.PlanStatusValue.Created)))
                      .ForMember(u => u.Duration, options => options.MapFrom(input => new PlanCycleDuration(input.Duration.ParseToEnum<PlanCycleDuration.PlanCycleDurationValue>())));
        }
    }
}
