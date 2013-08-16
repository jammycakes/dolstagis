using System.Web;
using Dolstagis.Core.Caching;
using Dolstagis.Core.Mail;
using Dolstagis.Core.Templates;
using Dolstagis.Core.Time;
using Dolstagis.Core.Web.Caching;
using Dolstagis.Core.Web.Flash;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Dolstagis.Core
{
    public class CoreNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMailer>().To<SystemNetMailMailer>().InSingletonScope();
            Bind<IClock>().To<SystemClock>().InSingletonScope();
            Bind<ITemplateEngine>().To<SimpleTemplateEngine>().InSingletonScope();
            Bind<IMailSettings>().To<MailSettings>().InSingletonScope();
            Bind<ICache>().ToMethod(x => new HttpRuntimeCache(HttpRuntime.Cache)).InSingletonScope();

            Bind<IFlashMessageStore>().To<FlashMessageStore>().InRequestScope();
        }
    }
}
