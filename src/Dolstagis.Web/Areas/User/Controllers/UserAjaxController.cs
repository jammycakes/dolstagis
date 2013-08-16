using System.Web.Mvc;
using Dolstagis.Contrib.Auth;
using Dolstagis.Contrib.Auth.Models;

namespace Dolstagis.Web.Areas.User.Controllers
{
    public class UserAjaxController : Controller
    {
        private UserManager userManager;

        public UserAjaxController(UserManager userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        ///  Ends a user session.
        /// </summary>
        /// <param name="id">
        ///  The ID of the session to delete.
        /// </param>

        [HttpDelete, ActionName("session")]
        public ActionResult EndSession(string id)
        {
            this.userManager.DeleteSession(id);
            return Content("OK");
        }

        [HttpDelete, ActionName("other-sessions")]
        public ActionResult EndOtherSessions()
        {
            this.userManager.DeleteOtherSessions(this.User as UserSession);
            return Content("OK");
        }

        [HttpGet, ActionName("session")]
        public ActionResult GetSession(string id)
        {
            return Content(id.ToString());
        }
    }
}