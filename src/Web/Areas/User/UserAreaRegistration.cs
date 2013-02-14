using System.Web.Mvc;

namespace Dolstagis.Web.Areas.User
{
    public class UserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "User";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "User_token",
                "User/{token}",
                new { controller = "UserToken" },
                new { token = @"^\{[0-9A-Fa-f]{8}\-([0-9A-Fa-f]{4}\-){3}[0-9A-Fa-f]{12}\}$" }
            );

            context.MapRoute(
                "User_main",
                "User/{action}",
                new { controller = "User" }
            );

            context.MapRoute(
                "User_default",
                "User/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
