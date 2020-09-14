using System;

using Abp.AspNetCore.Configuration;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

using Sales.Application.Dtos.Products;
using Sales.Domain.Entities.Products;
using Sales.Domain.ValueObjects.Products;

namespace Sales
{
    [DependsOn(
        typeof(SalesCoreModule),
        typeof(AbpAutoMapperModule))]
    public class SalesApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.CreateMap<CreateProductInput, Product>()
                      .ForMember(u => u.Name, options => options.MapFrom(input => input.Name))
                      .ForMember(u => u.Type, options => options.MapFrom(input => new ProductType((ProductType.ProductTypeValue)Enum.Parse(typeof(ProductType.ProductTypeValue), input.Type))));
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SalesApplicationModule).GetAssembly());
            Configuration.Modules.AbpAspNetCore().CreateControllersForAppServices(typeof(SalesApplicationModule).Assembly, moduleName: "app", useConventionalHttpVerbs: true);

        }
    }
}