using Dolstagis.Accounts;
using Dolstagis.Core;
using Dolstagis.Web.Helpers;
using Dolstagis.Web.Helpers.Flash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Dolstagis.Web.Areas.User.Controllers
{
    [Authorize]
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
            var session = users.Login(username, password, this.Request);
            if (session != null) {
                HttpCookie authCookie = UserControllerHelper.GetAuthCookie(session, persist);
                this.Response.SetCookie(authCookie);
                var url = FormsAuthentication.GetRedirectUrl(session.SessionID, persist);
                this.Flash("Welcome back, " + session.User.DisplayName, Level.Info);
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

        [HttpGet]
        public ActionResult Logout()
        {
            this.users.DeleteSession(this.User as UserSession);
            this.Flash("You are now logged out.");
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.DefaultUrl);
        }

        [HttpGet]
        public new ActionResult Profile()
        {
            return View(this.User.Identity);
        }

        [HttpPost]
        public new ActionResult Profile(string displayName, string emailAddress)
        {
            var user = this.User.Identity as Accounts.User;
            user.DisplayName = displayName;
            user.EmailAddress = emailAddress;
            this.Flash("Your details have been saved.");
            return View(user);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            this.ViewData["HashAlgorithm"] = this.users.PasswordProvider.Description;
            return View(this.User.Identity);
        }

        [HttpPost]
        public ActionResult ChangePassword(string oldPassword, string newPassword, string confirm)
        {
            if (newPassword != confirm) {
                this.Flash("Passwords do not match.", Level.Error);
                return ChangePassword();
            }
            else {
                try {
                    this.users.ChangePassword((Accounts.User)this.User.Identity, oldPassword, newPassword);
                }
                catch (UserException ex) {
                    this.Flash(ex.Message, Level.Error);
                    return ChangePassword();
                }
            }
            return View("PasswordChanged");
        }

        [HttpGet]
        public ActionResult Sessions()
        {
            return View(this.users.GetSessionsForUser(this.User.Identity as Accounts.User).ToList());
        }
    }
}
