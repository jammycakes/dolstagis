using Dolstagis.Accounts;
using Dolstagis.Web.Helpers;
using Dolstagis.Web.Helpers.Flash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string username, string password, bool persist)
        {
            var user = users.Login(username, password);
            if (user != null) {
                var cookie = FormsAuthentication.GetAuthCookie(user.UserID.ToString(), persist);
                var url = FormsAuthentication.GetRedirectUrl(user.UserID.ToString(), persist);
                this.Response.SetCookie(cookie);
                this.Flash("Welcome back, " + user.DisplayName, Level.Info);
                return Redirect(url);
            }
            else {
                this.Flash("Your user name and/or password were not correct.", Level.Error);
                return View();
            }
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
