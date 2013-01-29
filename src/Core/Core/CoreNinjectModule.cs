using Dolstagis.Core.Mail;
using Dolstagis.Core.Templates;
using Dolstagis.Core.Time;
using Ninject.Modules;

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
        }
    }
}
