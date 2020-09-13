using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;

using Sales.Web.Startup;
namespace Sales.Web.Tests
{
    [DependsOn(
        typeof(SalesWebModule),
        typeof(AbpAspNetCoreTestBaseModule)
        )]
    public class SalesWebTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SalesWebTestModule).GetAssembly());
        }
    }
}