using System.Web.Http;
using System.Web.Mvc;
using Dolstagis.Web.Infrastructure;

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
            context.MapHttpRoute(
                name: "User_ajax",
                routeTemplate: "user/ajax/{action}/{id}",
                defaults: new {
                    controller = "UserAjax",
                    id = RouteParameter.Optional
                }
            );

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
