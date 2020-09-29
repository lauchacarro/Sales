
using AutoMapper;

using Sales.Application.Dtos.Orders;
using Sales.Application.Extensions;
using Sales.Domain.Entities.Orders;
using Sales.Domain.ValueObjects;
using Sales.Domain.ValueObjects.Orders;

namespace HablemosPorHablar
{
    public class OrderDtoProfile : Profile
    {
        public OrderDtoProfile()
        {
            CreateMap<OrderDto, Order>()
                    .ForMember(u => u.UserId, options => options.MapFrom(input => input.UserId))
                    .ForMember(u => u.CreationTime, options => options.MapFrom(input => input.CreationTime))
                    .ForMember(u => u.IsDeleted, options => options.MapFrom(input => input.IsDeleted))
                    .ForMember(u => u.LastModificationTime, options => options.MapFrom(input => input.LastModificationTime))
                    .ForMember(u => u.TotalAmount, options => options.MapFrom(input => input.TotalAmount))
                    .ForMember(u => u.Id, options => options.MapFrom(input => input.Id))
                    .ForMember(u => u.Currency, options => options.MapFrom(input => new Currency(input.Currency.ParseToEnum<Currency.CurrencyValue>())))
                    .ForMember(u => u.Status, options => options.MapFrom(input => new OrderStatus(input.Status.ParseToEnum<OrderStatus.OrderStatusValue>())))
                    .ForMember(u => u.Type, options => options.MapFrom(input => new OrderType(input.Type.ParseToEnum<OrderType.OrderTypeValue>())));

            CreateMap<Order, OrderDto>()
                     .ForMember(u => u.UserId, options => options.MapFrom(input => input.UserId))
                    .ForMember(u => u.CreationTime, options => options.MapFrom(input => input.CreationTime))
                    .ForMember(u => u.IsDeleted, options => options.MapFrom(input => input.IsDeleted))
                    .ForMember(u => u.LastModificationTime, options => options.MapFrom(input => input.LastModificationTime))
                    .ForMember(u => u.TotalAmount, options => options.MapFrom(input => input.TotalAmount))
                    .ForMember(u => u.Id, options => options.MapFrom(input => input.Id))
                    .ForMember(u => u.Currency, options => options.MapFrom(input => input.Currency.Code.ToString()))
                    .ForMember(u => u.Status, options => options.MapFrom(input => input.Status.Status.ToString()))
                    .ForMember(u => u.Type, options => options.MapFrom(input => input.Type.Type.ToString()));
        }
    }
}
