using Dolstagis.Contrib.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dolstagis.Web.Controllers
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
