using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;

using Sales.Domain;

namespace Sales.EntityFrameworkCore
{
    [DependsOn(
        typeof(SalesCoreModule),
        typeof(SalesDomainModule),
        typeof(AbpEntityFrameworkCoreModule))]
    public class SalesEntityFrameworkCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SalesEntityFrameworkCoreModule).GetAssembly());
        }
    }
}