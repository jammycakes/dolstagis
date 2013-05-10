using System;
using System.Web;
using System.Web.Mvc;
using Dolstagis.Contrib.Auth;
using Dolstagis.Contrib.Auth.Models;
using Dolstagis.Web.Helpers;
using Dolstagis.Web.Helpers.Flash;

namespace Dolstagis.Web.Areas.User.Controllers
{
    public class UserTokenController : Controller
    {
        private UserManager userManager;
        private UserToken token;

        public UserTokenController(UserManager userManager)
        {
            this.userManager = userManager;
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            string s = requestContext.RouteData.Values["token"] as string;
            Guid g;
            if (Guid.TryParse(s, out g)) {
                this.token = userManager.GetToken(g);
                if (this.token != null) {
                    requestContext.RouteData.Values["action"] = this.token.Action;
                    base.Initialize(requestContext);
                    return;
                }
            }
            throw new HttpException(404, "This token does not exist, is not valid, or has expired.");
        }

        public ActionResult TestToken()
        {
            return View(token);
        }

        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword) {
                this.Flash("Passwords do not match.", Level.Error);
                return View();
            }
            else {
                userManager.SetPassword(this.token.User, newPassword);
                var session = userManager.CreateSessionFor
                    (this.token.User, this.Request.UserAgent, this.Request.UserHostAddress);
                HttpCookie authCookie = UserControllerHelper.GetAuthCookie(session, false);
                this.Flash("Welcome back, " + session.User.DisplayName +
                    ". Your password has been changed and you have been logged in.",
                    Level.Info);
                this.Response.SetCookie(authCookie);
                userManager.DeleteToken(token);
                return Redirect("/");
            }
        }
    }
}
