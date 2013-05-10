using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Dolstagis.Contrib.Auth;
using Dolstagis.Contrib.Auth.Models;

namespace Dolstagis.Web.Areas.User.Controllers
{
    public class UserControllerHelper : Controller
    {
        internal static HttpCookie GetAuthCookie(UserSession session, bool persist)
        {
            if (persist) {
                HttpCookie authCookie;
                var time = TimeSpan.FromDays(3652.5);
                var ticket = new FormsAuthenticationTicket
                    (session.SessionID, true, Convert.ToInt32(time.TotalMinutes));
                var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                authCookie.Expires = DateTime.Now.Add(time);
                return authCookie;
            }
            else {
                return FormsAuthentication.GetAuthCookie(session.SessionID, false);
            }
        }
    }
}