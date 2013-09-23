using System.Web.Mvc;
using Dolstagis.Contrib.Auth;

namespace Dolstagis.Web.Handlers
{
    public class HomeController : Controller
    {
        private UserManager users;

        public HomeController(UserManager userManager) : base()
        {
            this.users = userManager;
        }

        public ActionResult Index()
        {
            return View(this.users.GetAllUsers());
        }
    }
}
