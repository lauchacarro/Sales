
using AutoMapper;

using Sales.Application.Dtos.Subscriptions;
using Sales.Application.Extensions;
using Sales.Domain.Entities.Subscriptions;
using Sales.Domain.ValueObjects.Subscriptions;

namespace Sales.Application.MapperProfiles.Subscriptions
{
    public class SubscriptionCycleDtoProfile : Profile
    {
        public SubscriptionCycleDtoProfile()
        {
            CreateMap<SubscriptionCycleDto, SubscriptionCycle>()
                     .ForMember(u => u.SubscriptionId, options => options.MapFrom(input => input.SubscriptionId))
                     .ForMember(u => u.CreationDate, options => options.MapFrom(input => input.CreationDate))
                     .ForMember(u => u.ActivationDate, options => options.MapFrom(input => input.ActivationDate))
                     .ForMember(u => u.ExpirationDate, options => options.MapFrom(input => input.ExpirationDate))
                     .ForMember(u => u.Id, options => options.MapFrom(input => input.Id))
                     .ForMember(u => u.Status, options => options.MapFrom(input => new SubscriptionCycleStatus(input.Status.ParseToEnum<SubscriptionCycleStatus.SubscriptionCycleStatusValue>())));

            CreateMap<SubscriptionCycle, SubscriptionCycleDto>()
                     .ForMember(u => u.SubscriptionId, options => options.MapFrom(input => input.SubscriptionId))
                     .ForMember(u => u.CreationDate, options => options.MapFrom(input => input.CreationDate))
                     .ForMember(u => u.ActivationDate, options => options.MapFrom(input => input.ActivationDate))
                     .ForMember(u => u.ExpirationDate, options => options.MapFrom(input => input.ExpirationDate))
                     .ForMember(u => u.Id, options => options.MapFrom(input => input.Id))
                     .ForMember(u => u.Status, options => options.MapFrom(input => input.Status.Status.ToString()));
        }
    }
}
