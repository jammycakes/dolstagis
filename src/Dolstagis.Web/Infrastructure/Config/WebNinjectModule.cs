using System.Web;
using System.Web.Hosting;
using Dolstagis.Core.Caching;
using Dolstagis.Core.IO;
using Dolstagis.Core.Mail;
using Dolstagis.Core.Templates;
using Dolstagis.Core.Web.Caching;
using Ninject.Modules;
using Ninject.Web.Common;
using Dolstagis.Core.Web.Flash;

namespace Dolstagis.Web.Infrastructure.Config
{
    public class WebNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IFilespace>()
                .ToMethod(x => new LocalFilespace(HostingEnvironment.MapPath("~/Views")))
                .WhenInjectedInto<ITemplateEngine>();
#if DEBUG
            Rebind<IMailer>().To<MailLogger>().InSingletonScope();
#endif
        }
    }
}
