using Dolstagis.Accounts;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dolstagis.Web.Areas.User.Controllers
{
    public class UserAjaxController : ApiController
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
        public void EndSession(string id)
        {
            this.userManager.DeleteSession(id);
        }

        [HttpDelete, ActionName("other-sessions")]
        public void EndOtherSessions()
        {
            this.userManager.DeleteOtherSessions(this.User as UserSession);
        }

        [HttpGet, ActionName("session")]
        public string GetSession(string id)
        {
            return id;
        }
    }
}