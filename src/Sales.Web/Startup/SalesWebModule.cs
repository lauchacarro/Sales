using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;

using Sales.Configuration;
using Sales.Domain;
using Sales.EntityFrameworkCore;

namespace Sales.Web.Startup
{
    [DependsOn(
        typeof(SalesApplicationModule),
        typeof(SalesEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreModule))]
    public class SalesWebModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public SalesWebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(SalesConsts.ConnectionStringName);

            Configuration.Navigation.Providers.Add<SalesNavigationProvider>();

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(SalesApplicationModule).GetAssembly()
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SalesWebModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(SalesWebModule).Assembly);
        }
    }
}