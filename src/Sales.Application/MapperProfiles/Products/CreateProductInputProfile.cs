
using AutoMapper;

using Sales.Application.Dtos.Products;
using Sales.Application.Extensions;
using Sales.Domain.Entities.Products;
using Sales.Domain.ValueObjects.Products;

namespace Sales.Application.MapperProfiles.Products
{
    public class CreateProductInputProfile : Profile
    {
        public CreateProductInputProfile()
        {
            CreateMap<CreateProductInput, Product>()
                      .ForMember(u => u.Name, options => options.MapFrom(input => input.Name))
                      .ForMember(u => u.Status, options => options.MapFrom(input => new ProductStatus(ProductStatus.ProductStatusValue.Created)))
                      .ForMember(u => u.Type, options => options.MapFrom(input => new ProductType(input.Type.ParseToEnum<ProductType.ProductTypeValue>())));
        }
    }
}
