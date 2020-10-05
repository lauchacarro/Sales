
using AutoMapper;

using Sales.Application.Dtos.Orders;
using Sales.Application.Extensions;
using Sales.Domain.Entities.Orders;
using Sales.Domain.ValueObjects.Orders;

namespace Sales.Application.MapperProfiles.Orders
{
    public class UpdateOrderInputProfile : Profile
    {
        public UpdateOrderInputProfile()
        {
            CreateMap<UpdateOrderInput, Order>()
                    .ForMember(u => u.Id, options => options.MapFrom(input => input.Id))
                    .ForMember(u => u.TotalAmount, options => options.MapFrom(input => input.TotalAmount))
                    .ForMember(u => u.Status, options => options.MapFrom(input => new OrderStatus(input.Status.ParseToEnum<OrderStatus.OrderStatusValue>())));

            CreateMap<Order, UpdateOrderInput>()
                    .ForMember(u => u.Id, options => options.MapFrom(input => input.Id))
                    .ForMember(u => u.TotalAmount, options => options.MapFrom(input => input.TotalAmount))
                    .ForMember(u => u.Status, options => options.MapFrom(input => input.Status.Status.ToString()));
        }
    }
}
