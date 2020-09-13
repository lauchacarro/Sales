using Abp.Modules;
using Abp.Reflection.Extensions;

using Sales.Localization;

namespace Sales
{
    public class SalesCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            SalesLocalizationConfigurer.Configure(Configuration.Localization);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SalesCoreModule).GetAssembly());
        }
    }
}