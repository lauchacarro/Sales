
using AutoMapper;

using Sales.Application.Dtos.Plans;
using Sales.Application.Extensions;
using Sales.Domain.Entities.Plans;
using Sales.Domain.ValueObjects;

namespace Sales.Application.MapperProfiles.Plans
{
    public class PlanPriceDtoProfile : Profile
    {
        public PlanPriceDtoProfile()
        {
            CreateMap<PlanPriceDto, PlanPrice>()
                     .ForMember(u => u.Price, options => options.MapFrom(input => input.Price))
                     .ForMember(u => u.Id, options => options.MapFrom(input => input.Id))
                     .ForMember(u => u.PlanId, options => options.MapFrom(input => input.PlanId))
                     .ForMember(u => u.Currency, options => options.MapFrom(input => new Currency(input.Currency.ParseToEnum<Currency.CurrencyValue>())));

            CreateMap<PlanPrice, PlanPriceDto>()
                     .ForMember(u => u.PlanId, options => options.MapFrom(input => input.PlanId))
                     .ForMember(u => u.Id, options => options.MapFrom(input => input.Id))
                     .ForMember(u => u.Price, options => options.MapFrom(input => input.Price))
                     .ForMember(u => u.Currency, options => options.MapFrom(input => input.Currency.Code.ToString()));
        }
    }
}
