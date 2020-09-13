using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Sales
{
    [DependsOn(
        typeof(SalesCoreModule),
        typeof(AbpAutoMapperModule))]
    public class SalesApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SalesApplicationModule).GetAssembly());
        }
    }
}