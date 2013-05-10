using System.Web.Mvc;
using Dolstagis.Contrib.Auth;

namespace Dolstagis.Web.Areas.Admin.Controllers
{
    [Authorize(Roles="admin")]
    public class UsersController : Controller
    {
        private UserManager userManager;

        public UsersController(UserManager userManager)
        {
            this.userManager = userManager;
        }

        //
        // GET: /Admin/Users/

        public ActionResult Index()
        {
            var users = userManager.GetAllUsers();
            return View(users);
        }
    }
}
