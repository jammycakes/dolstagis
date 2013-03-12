using Dolstagis.Accounts;
using Dolstagis.Web.Infrastructure;
using Ninject;
using System;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Routing;

namespace Dolstagis.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        [Inject]
        public Func<UserManager> UserManagerFactory { get; set; }

        protected void Application_Start()
        {
            LoggingConfig.InitLogging();
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerSelector),
                new AreaHttpControllerSelector(GlobalConfiguration.Configuration)
            );
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (this.Context.User == null || this.Context.User.Identity == null) return;
            if (!this.Context.User.Identity.IsAuthenticated) return;

            using (var userManager = UserManagerFactory()) {
                var session = userManager.AccessSession(this.Context.User.Identity.Name);
                this.Context.User = session;
            }
        }
    }
}
