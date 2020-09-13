
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Sales.Domain
{
    public class SalesDomainModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SalesDomainModule).GetAssembly());
        }
    }
}
