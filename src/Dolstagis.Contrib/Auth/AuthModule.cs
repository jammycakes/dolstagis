using Dolstagis.Contrib.Auth.Passwords;
using Dolstagis.Contrib.Auth.Passwords.BCrypt;
using Dolstagis.Contrib.Auth.Passwords.PlainText;
using Dolstagis.Contrib.Auth.Passwords.Sha512;
using Dolstagis.Framework;
using cfg = Dolstagis.Framework.Configuration;
using Ninject.Modules;

namespace Dolstagis.Contrib.Auth
{
    public class AuthModule : ModuleBase
    {
        public override void Load()
        {
            base.Load();

            /* ====== Components ====== */

            Bind<IUserRepository>().To<UserRepository>();

            /* ====== Settings ====== */

            Bind<IAuthSettings>().ToMethod(x => cfg.Settings.Get<AuthSettings>());
            Bind<IBCryptSettings>().ToMethod(x => cfg.Settings.Get<BCryptSettings>());

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
#if DEBUG
            Bind<IPasswordProvider>().To<PlainTextPasswordProvider>().WhenInjectedInto<AggregatePasswordProvider>();
#endif
        }
    }
}
