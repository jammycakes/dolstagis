using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Accounts
{
    public class AccountsNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAccountSettings>().To<AccountSettings>().InSingletonScope();
        }
    }
}
