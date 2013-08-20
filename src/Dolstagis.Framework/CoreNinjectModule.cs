using System.Web;
using Dolstagis.Framework.Caching;
using cfg = Dolstagis.Framework.Configuration;
using Dolstagis.Framework.Mail;
using Dolstagis.Framework.Templates;
using Dolstagis.Framework.Time;
using Dolstagis.Framework.Web.Caching;
using Dolstagis.Framework.Web.Flash;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Dolstagis.Framework
{
    public class CoreNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMailer>().To<SystemNetMailMailer>().InSingletonScope();
            Bind<IClock>().To<SystemClock>().InSingletonScope();
            Bind<ITemplateEngine>().To<SimpleTemplateEngine>().InSingletonScope();
            Bind<IMailSettings>().ToMethod(x => cfg.Settings.Get<MailSettings>()).InSingletonScope();
            Bind<ICache>().ToMethod(x => new HttpRuntimeCache(HttpRuntime.Cache)).InSingletonScope();
            Bind<IFlashMessageStore>().To<FlashMessageStore>().InRequestScope();
        }

        private object Settings<T1>()
        {
            throw new System.NotImplementedException();
        }
    }
}
