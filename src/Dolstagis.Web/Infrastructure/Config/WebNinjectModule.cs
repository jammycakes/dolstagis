using System.Web;
using System.Web.Hosting;
using Dolstagis.Framework.Caching;
using Dolstagis.Framework.IO;
using Dolstagis.Framework.Mail;
using Dolstagis.Framework.Templates;
using Dolstagis.Framework.Web.Caching;
using Ninject.Modules;
using Ninject.Web.Common;
using Dolstagis.Framework.Web.Flash;

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
