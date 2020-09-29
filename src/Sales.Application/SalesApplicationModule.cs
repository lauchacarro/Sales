
using Abp.AspNetCore.Configuration;
using Abp.AutoMapper;
using Abp.FluentValidation;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Sales
{
    [DependsOn(
        typeof(SalesCoreModule),
        typeof(AbpFluentValidationModule),
        typeof(AbpAutoMapperModule))]
    public class SalesApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            var thisAssembly = typeof(SalesApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);
            Configuration.Modules.AbpAspNetCore().CreateControllersForAppServices(thisAssembly, moduleName: "app", useConventionalHttpVerbs: true);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(csg => csg.AddMaps(typeof(SalesApplicationModule).GetAssembly()));
        }
    }
}