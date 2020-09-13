using Abp.AspNetCore.Mvc.Controllers;

namespace Sales.Web.Controllers
{
    public abstract class SalesControllerBase : AbpController
    {
        protected SalesControllerBase()
        {
            LocalizationSourceName = SalesConsts.LocalizationSourceName;
        }
    }
}