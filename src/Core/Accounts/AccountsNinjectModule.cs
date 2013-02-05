using Dolstagis.Accounts.Passwords;
using Dolstagis.Accounts.Passwords.BCrypt;
using Dolstagis.Accounts.Passwords.Sha512;
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
            /* ====== Settings ====== */

            Bind<IAccountSettings>().To<AccountSettings>();
            Bind<IBCryptSettings>().To<BCryptSettings>();

            /* ====== Password providers ====== */

            Unbind<IPasswordProvider>();
            Bind<IPasswordProvider>().To<AggregatePasswordProvider>().WhenInjectedInto<UserManager>();

            /*
             * Add multiple password providers if you like here, but always put the
             * most secure one first. Passwords hashed using providers other than
             * the first one will be upgraded to use the first one.
             */

            Bind<IPasswordProvider>().To<BCryptPasswordProvider>().WhenInjectedInto<AggregatePasswordProvider>();
            Bind<IPasswordProvider>().To<Sha512PasswordProvider>().WhenInjectedInto<AggregatePasswordProvider>();
        }
    }
}
