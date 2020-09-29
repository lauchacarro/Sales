
using AutoMapper;

using Sales.Application.Dtos.Plans;
using Sales.Application.Extensions;
using Sales.Domain.Entities.Plans;
using Sales.Domain.ValueObjects.Plans;

namespace Sales.Application.MapperProfiles.Plans
{
    public class CreatePlanInputProfile : Profile
    {
        public CreatePlanInputProfile()
        {
            CreateMap<CreatePlanInput, Plan>()
                      .ForMember(u => u.ProductId, options => options.MapFrom(input => input.ProductId))
                      .ForMember(u => u.Name, options => options.MapFrom(input => input.Name))
                      .ForMember(u => u.Description, options => options.MapFrom(input => input.Description))
                      .ForMember(u => u.Status, options => options.MapFrom(input => new PlanStatus(PlanStatus.PlanStatusValue.Created)))
                      .ForMember(u => u.Duration, options => options.MapFrom(input => new PlanCycleDuration(input.Duration.ParseToEnum<PlanCycleDuration.PlanCycleDurationValue>())))
                      .ForMember(u => u.Type, options => options.MapFrom(input => new PlanType(input.Type.ParseToEnum<PlanType.PlanTypeValue>())));
        }
    }
}
