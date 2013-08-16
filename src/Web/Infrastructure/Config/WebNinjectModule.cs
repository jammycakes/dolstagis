using System.Web;
using System.Web.Hosting;
using Dolstagis.Core.Caching;
using Dolstagis.Core.IO;
using Dolstagis.Core.Mail;
using Dolstagis.Core.Templates;
using Dolstagis.Web.Helpers.Caching;
using Ninject.Modules;
using Ninject.Web.Common;
using Dolstagis.Web.Helpers.Flash;

namespace Dolstagis.Web.Infrastructure.Config
{
    public class WebNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IFlashMessageStore>().To<FlashMessageStore>().InRequestScope();
            Bind<IFilespace>()
                .ToMethod(x => new LocalFilespace(HostingEnvironment.MapPath("~/Views")))
                .WhenInjectedInto<ITemplateEngine>();
            Bind<ICache>().ToMethod(x => new HttpRuntimeCache(HttpRuntime.Cache));
#if DEBUG
            Rebind<IMailer>().To<MailLogger>().InSingletonScope();
#endif
        }
    }
}
