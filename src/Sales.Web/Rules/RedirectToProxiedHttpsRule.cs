using System.Text;

using Microsoft.AspNetCore.Rewrite;

namespace Sales.Web.Rules
{
    public class RedirectToProxiedHttpsRule : IRule
    {
        public virtual void ApplyRule(RewriteContext context)
        {
            var request = context.HttpContext.Request;

            // #1) Did this request start off as HTTP?
            string reqProtocol;
            if (request.Headers.ContainsKey("X-Forwarded-Proto"))
            {
                reqProtocol = request.Headers["X-Forwarded-Proto"][0];
            }
            else
            {
                reqProtocol = request.IsHttps ? "https" : "http";
            }


            // #2) If so, redirect to HTTPS equivalent
            if (reqProtocol != "https")
            {
                var newUrl = new StringBuilder()
                    .Append("https://").Append(request.Host)
                    .Append(request.PathBase).Append(request.Path)
                    .Append(request.QueryString);

                context.HttpContext.Response.Redirect(newUrl.ToString(), true);
            }
        }
    }
}
