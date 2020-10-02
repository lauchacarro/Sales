
using AutoMapper;

using Sales.Application.Dtos.Notifications;
using Sales.Application.Extensions;
using Sales.Domain.Entities.Notifications;
using Sales.Domain.ValueObjects.Notifications;

namespace Sales.Application.MapperProfiles.Notifications
{
    public class NotificationDtoProfile : Profile
    {
        public NotificationDtoProfile()
        {
            CreateMap<NotificationDto, Notification>()
                     .ForMember(u => u.Attempts, options => options.MapFrom(input => input.Attempts))
                     .ForMember(u => u.OrderId, options => options.MapFrom(input => input.OrderId))
                     .ForMember(u => u.Type, options => options.MapFrom(input => new NotificationType(input.Type.ParseToEnum<NotificationType.NotificationTypeValue>())))
                     .ForMember(u => u.Status, options => options.MapFrom(input => new NotificationStatus(input.Status.ParseToEnum<NotificationStatus.NotificationStatusValue>())));

            CreateMap<Notification, NotificationDto>()
                     .ForMember(u => u.Attempts, options => options.MapFrom(input => input.Attempts))
                     .ForMember(u => u.OrderId, options => options.MapFrom(input => input.OrderId))
                     .ForMember(u => u.Type, options => options.MapFrom(input => input.Type.Type.ToString()))
                     .ForMember(u => u.Status, options => options.MapFrom(input => input.Status.Status.ToString()));
        }
    }
}
