using System.Collections.Generic;

using Abp.AspNetCore.Configuration;
using Abp.AutoMapper;
using Abp.FluentValidation;
using Abp.Modules;
using Abp.Reflection.Extensions;

using AutoMapper;

using Sales.Application.MapperProfiles.Products;

namespace Sales
{
    [DependsOn(
        typeof(SalesCoreModule),
        typeof(AbpFluentValidationModule),
        typeof(AbpAutoMapperModule))]
    public class SalesApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.AddProfile(new CreateProductInputProfile());
            });
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(SalesApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);
            Configuration.Modules.AbpAspNetCore().CreateControllersForAppServices(thisAssembly, moduleName: "app", useConventionalHttpVerbs: true);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(new List<Profile>()
                {
                    new CreateProductInputProfile(),
                    new CreateProductPlanInputProfile()
                })
            );

        }
    }
}