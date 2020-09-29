
using AutoMapper;

using Sales.Application.Dtos.Plans;
using Sales.Application.Extensions;
using Sales.Domain.Entities.Plans;
using Sales.Domain.ValueObjects.Plans;

namespace Sales.Application.MapperProfiles.Plans
{
    public class PlanDtoProfile : Profile
    {
        public PlanDtoProfile()
        {
            CreateMap<PlanDto, Plan>()
                     .ForMember(u => u.ProductId, options => options.MapFrom(input => input.ProductId))
                     .ForMember(u => u.Id, options => options.MapFrom(input => input.Id))
                     .ForMember(u => u.Name, options => options.MapFrom(input => input.Name))
                     .ForMember(u => u.Description, options => options.MapFrom(input => input.Description))
                     .ForMember(u => u.Status, options => options.MapFrom(input => new PlanStatus(input.Status.ParseToEnum<PlanStatus.PlanStatusValue>())))
                     .ForMember(u => u.Type, options => options.MapFrom(input => new PlanType(input.Type.ParseToEnum<PlanType.PlanTypeValue>())))
                     .ForMember(u => u.Duration, options => options.MapFrom(input => new PlanCycleDuration(input.Duration.ParseToEnum<PlanCycleDuration.PlanCycleDurationValue>())));

            CreateMap<Plan, PlanDto>()
                     .ForMember(u => u.ProductId, options => options.MapFrom(input => input.ProductId))
                     .ForMember(u => u.Id, options => options.MapFrom(input => input.Id))
                     .ForMember(u => u.Name, options => options.MapFrom(input => input.Name))
                     .ForMember(u => u.Description, options => options.MapFrom(input => input.Description))
                     .ForMember(u => u.Status, options => options.MapFrom(input => input.Status.Status.ToString()))
                     .ForMember(u => u.Duration, options => options.MapFrom(input => input.Duration.Duration.ToString()))
                     .ForMember(u => u.Type, options => options.MapFrom(input => input.Type.Type.ToString()));
        }
    }
}
