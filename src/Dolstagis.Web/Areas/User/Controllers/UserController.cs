using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Dolstagis.Contrib.Auth;
using Models = Dolstagis.Contrib.Auth.Models;
using Dolstagis.Framework;
using Dolstagis.Framework.Web;
using Dolstagis.Framework.Web.Flash;
using ViewModels = Dolstagis.Web.Areas.User.ViewModels;

namespace Dolstagis.Web.Areas.User.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private InvitationManager invitationManager;
        private UserManager users;

        public UserController(UserManager userManager, InvitationManager invitationManager)
        {
            this.users = userManager;
            this.invitationManager = invitationManager;
        }

        private Models.User AuthenticatedUser
        {
            get
            {
                return this.User.Identity as Models.User;
            }
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
            this.users.DeleteSession(this.User as Models.UserSession);
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
            var user = this.User.Identity as Dolstagis.Contrib.Auth.Models.User;
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
                    this.users.ChangePassword((Models.User)this.User.Identity, oldPassword, newPassword);
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
            return View(this.users.GetSessionsForUser(this.User.Identity as Models.User).ToList());
        }

        [HttpGet]
        public ActionResult Invite()
        {
            if (AuthenticatedUser == null || !AuthenticatedUser.CanInvite) {
                return HttpNotFound();
            }
            return View(new ViewModels.Invitation { User = AuthenticatedUser });
        }

        [HttpPost]
        public ActionResult Invite(ViewModels.Invitation invitation)
        {
            invitation.User = AuthenticatedUser;
            if (!this.ModelState.IsValid) {
                this.Flash("Please correct the errors in the form and try again.", Level.Error);
                return View(invitation);
            }
            else {
                // TODO: include the user's message
                invitationManager.Invite(AuthenticatedUser, invitation.Name, invitation.Email);
                return View("InvitationSent");
            }
        }
    }
}
