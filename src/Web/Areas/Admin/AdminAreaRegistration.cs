using System.Web.Http;
using System.Web.Mvc;
using Dolstagis.Web.Infrastructure;

namespace Dolstagis.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapHttpRoute(
                name: "Admin_ajax",
                routeTemplate: "admin/ajax/{action}/{id}",
                defaults: new {
                    controller = "AdminAjax",
                    id = UrlParameter.Optional
                }
            );


            context.MapRoute(
                "Admin_default",
                "admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
