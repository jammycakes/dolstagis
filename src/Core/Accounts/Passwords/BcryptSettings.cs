using Dolstagis.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Accounts.Passwords
{
    public class BcryptSettings : AppConfigSettingsBase
    {
        public BcryptSettings()
        {
            this.WorkFactor = 10;
        }

        public int WorkFactor { get; private set; }
    }
}
