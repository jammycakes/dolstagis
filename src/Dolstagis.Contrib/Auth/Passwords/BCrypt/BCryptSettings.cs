using Dolstagis.Framework.Configuration;

namespace Dolstagis.Contrib.Auth.Passwords.BCrypt
{
    public class BCryptSettings : Settings, Dolstagis.Contrib.Auth.Passwords.BCrypt.IBCryptSettings
    {
        public BCryptSettings()
        {
            this.WorkFactor = 10;
        }

        public int WorkFactor { get; private set; }
    }
}
