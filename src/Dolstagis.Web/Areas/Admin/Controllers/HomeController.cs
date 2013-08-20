using System.Web.Mvc;

namespace Dolstagis.Web.Areas.Admin.Controllers
{
    [Authorize(Roles="admin")]
    public class HomeController : Controller
    {
        //
        // GET: /Admin/Home/

        public ActionResult Index()
        {
            return View();
        }
    }
}
