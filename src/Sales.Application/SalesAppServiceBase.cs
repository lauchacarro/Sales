using Abp.Application.Services;

namespace Sales
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class SalesAppServiceBase : ApplicationService
    {
        protected SalesAppServiceBase()
        {
            LocalizationSourceName = SalesConsts.LocalizationSourceName;
        }
    }
}