using Dolstagis.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Contrib.Auth.Passwords.BCrypt
{
    public class BCryptSettings : AppConfigSettingsBase, Dolstagis.Contrib.Auth.Passwords.BCrypt.IBCryptSettings
    {
        public BCryptSettings()
        {
            this.WorkFactor = 10;
        }

        public int WorkFactor { get; private set; }
    }
}
