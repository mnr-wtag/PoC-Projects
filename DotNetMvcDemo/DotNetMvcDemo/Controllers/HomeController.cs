using System.Web.Mvc;

namespace DotNetMvcDemo.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [Route("home")]
        public ActionResult Index()
        {
            return View();
        }
    }
}