using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;

using Sales.Web.Rules;

namespace Sales.Web.Extensions
{
    public static class RedirectToProxiedHttpsExtensions
    {
        public static RewriteOptions AddRedirectToProxiedHttps(this RewriteOptions options)
        {
            options.Rules.Add(new RedirectToProxiedHttpsRule());
            return options;
        }

        public static IApplicationBuilder UseRedirectToProxiedHttps(this IApplicationBuilder app)
        {
            var options = new RewriteOptions()
               .AddRedirectToProxiedHttps()
               .AddRedirect("(.*)/$", "$1");  // remove trailing slash
            app.UseRewriter(options);
            return app;
        }
    }
}
