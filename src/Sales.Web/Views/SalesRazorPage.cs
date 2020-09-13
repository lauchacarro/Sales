using Abp.AspNetCore.Mvc.Views;

namespace Sales.Web.Views
{
    public abstract class SalesRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected SalesRazorPage()
        {
            LocalizationSourceName = SalesConsts.LocalizationSourceName;
        }
    }
}
