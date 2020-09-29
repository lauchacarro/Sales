using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;

using Sales.Configuration;
using Sales.Domain.Options;
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

            IocManager.Register<IDatabaseOptions, DatabaseOptions>();
            Configuration.Get<IDatabaseOptions>().Schema = _appConfiguration.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>().Schema;

            IocManager.Register<INotificationOptions, NotificationOptions>();
            Configuration.Get<INotificationOptions>().Url = _appConfiguration.GetSection(nameof(NotificationOptions)).Get<NotificationOptions>().Url;

            IocManager.Register<IMobbexOptions, MobbexOptions>();
            Configuration.Get<IMobbexOptions>().ClientId = _appConfiguration.GetSection(nameof(MobbexOptions)).Get<MobbexOptions>().ClientId;
            Configuration.Get<IMobbexOptions>().ClientSecret = _appConfiguration.GetSection(nameof(MobbexOptions)).Get<MobbexOptions>().ClientSecret;
            Configuration.Get<IMobbexOptions>().Environment = _appConfiguration.GetSection(nameof(MobbexOptions)).Get<MobbexOptions>().Environment;
            Configuration.Get<IMobbexOptions>().ReturnUrl = _appConfiguration.GetSection(nameof(MobbexOptions)).Get<MobbexOptions>().ReturnUrl;
            Configuration.Get<IMobbexOptions>().CancelUrl = _appConfiguration.GetSection(nameof(MobbexOptions)).Get<MobbexOptions>().CancelUrl;

            IocManager.Register<IPaypalOptions, PaypalOptions>();
            Configuration.Get<IPaypalOptions>().ClientId = _appConfiguration.GetSection(nameof(PaypalOptions)).Get<PaypalOptions>().ClientId;
            Configuration.Get<IPaypalOptions>().ClientSecret = _appConfiguration.GetSection(nameof(PaypalOptions)).Get<PaypalOptions>().ClientSecret;
            Configuration.Get<IPaypalOptions>().Environment = _appConfiguration.GetSection(nameof(PaypalOptions)).Get<PaypalOptions>().Environment;
            Configuration.Get<IPaypalOptions>().ReturnUrl = _appConfiguration.GetSection(nameof(PaypalOptions)).Get<PaypalOptions>().ReturnUrl;
            Configuration.Get<IPaypalOptions>().CancelUrl = _appConfiguration.GetSection(nameof(PaypalOptions)).Get<PaypalOptions>().CancelUrl;

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