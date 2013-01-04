using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Core.Mail
{
    public class MailNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMailer>().To<SystemNetMailMailer>().InSingletonScope();
        }
    }
}
