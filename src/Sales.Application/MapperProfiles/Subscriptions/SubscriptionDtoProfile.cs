
using AutoMapper;

using Sales.Application.Dtos.Subscriptions;
using Sales.Application.Extensions;
using Sales.Domain.Entities.Subscriptions;

using Sales.Domain.ValueObjects.Subscriptions;

namespace Sales.Application.MapperProfiles.Subscriptions
{
    public class SubscriptionDtoProfile : Profile
    {
        public SubscriptionDtoProfile()
        {
            CreateMap<SubscriptionDto, Subscription>()
                     .ForMember(u => u.PlanId, options => options.MapFrom(input => input.PlanId))
                     .ForMember(u => u.Id, options => options.MapFrom(input => input.Id))
                     .ForMember(u => u.Status, options => options.MapFrom(input => new SubscriptionStatus(input.Status.ParseToEnum<SubscriptionStatus.SubscriptionStatusValue>())))
                     .ForMember(u => u.Type, options => options.MapFrom(input => new SubscriptionType(input.Type.ParseToEnum<SubscriptionType.SubscriptionTypeValue>())));

            CreateMap<Subscription, SubscriptionDto>()
                     .ForMember(u => u.PlanId, options => options.MapFrom(input => input.PlanId))
                     .ForMember(u => u.Id, options => options.MapFrom(input => input.Id))
                     .ForMember(u => u.Status, options => options.MapFrom(input => input.Status.Status.ToString()))
                     .ForMember(u => u.Type, options => options.MapFrom(input => input.Type.Type.ToString()));

        }
    }
}
