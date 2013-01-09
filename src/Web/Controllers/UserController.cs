using Dolstagis.Accounts;
using Dolstagis.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dolstagis.Web.Controllers
{
    public class UserController : Controller
    {
        private UserManager users;

        public UserController(UserManager userManager)
        {
            this.users = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult RequestPasswordReset()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult RequestPasswordReset(string name)
        {
            int count = this.users.RequestPasswordReset(name);
            if (count > 0) {
                return View("PasswordResetSent");
            }
            else {
                this.Flash("No user with this name was found.");
                return View();
            }
        }
    }
}
