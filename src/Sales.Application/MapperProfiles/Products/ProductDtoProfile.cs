
using AutoMapper;

using Sales.Application.Dtos.Products;
using Sales.Application.Extensions;
using Sales.Domain.Entities.Products;
using Sales.Domain.ValueObjects.Products;

namespace Sales.Application.MapperProfiles.Products
{
    public class ProductDtoProfile : Profile
    {
        public ProductDtoProfile()
        {
            CreateMap<ProductDto, Product>()
                     .ForMember(u => u.Description, options => options.MapFrom(input => input.Description))
                     .ForMember(u => u.Name, options => options.MapFrom(input => input.Name))
                     .ForMember(u => u.Id, options => options.MapFrom(input => input.Id))
                     .ForMember(u => u.Status, options => options.MapFrom(input => new ProductStatus(input.Status.ParseToEnum<ProductStatus.ProductStatusValue>())))
                     .ForMember(u => u.Type, options => options.MapFrom(input => new ProductType(input.Type.ParseToEnum<ProductType.ProductTypeValue>())));

            CreateMap<Product, ProductDto>()
                     .ForMember(u => u.Description, options => options.MapFrom(input => input.Description))
                     .ForMember(u => u.Name, options => options.MapFrom(input => input.Name))
                     .ForMember(u => u.Id, options => options.MapFrom(input => input.Id))
                     .ForMember(u => u.Status, options => options.MapFrom(input => input.Status.Status.ToString()))
                     .ForMember(u => u.Type, options => options.MapFrom(input => input.Type.Type.ToString()));
        }
    }
}
