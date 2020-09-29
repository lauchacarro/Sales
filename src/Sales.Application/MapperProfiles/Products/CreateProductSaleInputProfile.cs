
using AutoMapper;

using Sales.Application.Dtos.Products;
using Sales.Application.Extensions;
using Sales.Domain.Entities.Products;
using Sales.Domain.ValueObjects;
using Sales.Domain.ValueObjects.Products;

namespace Sales.Application.MapperProfiles.Products
{
    class CreateProductSaleInputProfile : Profile
    {
        public CreateProductSaleInputProfile()
        {
            CreateMap<CreateProductSaleInput, Product>()
                      .ForMember(u => u.Name, options => options.MapFrom(input => input.Name))
                      .ForMember(u => u.Status, options => options.MapFrom(input => new ProductStatus(ProductStatus.ProductStatusValue.Created)))
                      .ForMember(u => u.Type, options => options.MapFrom(input => new ProductType(ProductType.ProductTypeValue.Plan)));

            CreateMap<CreateProductSaleInput, ProductSalePrice>()
                      .ForMember(u => u.Price, options => options.MapFrom(input => input.Price))
                      .ForMember(u => u.Currency, options => options.MapFrom(input => new Currency(input.Currency.ParseToEnum<Currency.CurrencyValue>())));

            CreateMap<CreateProductSaleInput, Currency>()
                      .ForMember(u => u.Code, options => options.MapFrom(input => input.Currency.ParseToEnum<Currency.CurrencyValue>()));


        }
    }
}
