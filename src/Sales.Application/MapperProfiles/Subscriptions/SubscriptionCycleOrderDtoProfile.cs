
using AutoMapper;

using Sales.Application.Dtos.Subscriptions;
using Sales.Domain.Entities.Subscriptions;

namespace Sales.Application.MapperProfiles.Subscriptions
{
    class SubscriptionCycleOrderDtoProfile : Profile
    {
        public SubscriptionCycleOrderDtoProfile()
        {
            CreateMap<SubscriptionCycleOrderDto, SubscriptionCycleOrder>()
                     .ForMember(u => u.SubscriptionCycleId, options => options.MapFrom(input => input.SubscriptionCycleId))
                     .ForMember(u => u.OrderId, options => options.MapFrom(input => input.OrderId))
                     .ForMember(u => u.Id, options => options.MapFrom(input => input.Id));

            CreateMap<SubscriptionCycleOrder, SubscriptionCycleOrderDto>()
                     .ForMember(u => u.SubscriptionCycleId, options => options.MapFrom(input => input.SubscriptionCycleId))
                     .ForMember(u => u.OrderId, options => options.MapFrom(input => input.OrderId))
                     .ForMember(u => u.Id, options => options.MapFrom(input => input.Id));
        }
    }
}
