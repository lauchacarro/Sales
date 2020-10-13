
using AutoMapper;

using Sales.Application.Dtos.Orders;
using Sales.Application.Extensions;
using Sales.Domain.Entities.Orders;
using Sales.Domain.ValueObjects;
using Sales.Domain.ValueObjects.Orders;

namespace Sales.Application.MapperProfiles.Orders
{
    public class CreateOrderInputProfile : Profile
    {
        public CreateOrderInputProfile()
        {
            CreateMap<CreateOrderInput, Order>()
                    .ForMember(u => u.UserId, options => options.MapFrom(input => input.UserId))
                    .ForMember(u => u.TotalAmount, options => options.MapFrom(input => input.Price))
                    .ForMember(u => u.Type, options => options.MapFrom(input => new OrderType(OrderType.OrderTypeValue.Extra)))
                    .ForMember(u => u.Status, options => options.MapFrom(input => new OrderStatus(OrderStatus.OrderStatusValue.PaymentPending)))
                    .ForMember(u => u.Currency, options => options.MapFrom(input => new Currency(input.Currency.ParseToEnum<Currency.CurrencyValue>())));

            CreateMap<Order, CreateOrderInput>()
                    .ForMember(u => u.UserId, options => options.MapFrom(input => input.Id))
                    .ForMember(u => u.Price, options => options.MapFrom(input => input.TotalAmount))
                    .ForMember(u => u.Currency, options => options.MapFrom(input => input.Currency.Code.ToString()));
        }
    }
}
