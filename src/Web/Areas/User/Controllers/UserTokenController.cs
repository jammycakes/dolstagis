using Dolstagis.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}
