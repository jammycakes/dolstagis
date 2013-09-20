using System.Web.Mvc;

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
            context.MapRoute(
                "Admin_ajax",
                "admin/ajax/{action}/{id}",
                new {
                    controller = "AdminAjax",
                    id = UrlParameter.Optional
                }
            );


            context.MapRoute(
                "Admin_default",
                "admin/{controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
