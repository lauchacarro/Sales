using Microsoft.AspNetCore.Mvc;

namespace Sales.Web.Controllers
{
    public class HomeController : SalesControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}