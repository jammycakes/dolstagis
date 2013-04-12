using Dolstagis.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Accounts
{
    /// <summary>
    ///  Encapsulates the configuration settings for the user account manager.
    /// </summary>

    public class AccountSettings : AppConfigSettingsBase, IAccountSettings
    {
        public AccountSettings()
        {
            /// Put defaults in here.

            this.TokenLifetime = TimeSpan.FromHours(12);
        }

        /// <summary>
        ///  Gets the lifetime of a password reset token, in hours.
        /// </summary>

        public TimeSpan TokenLifetime { get; private set; }

        /// <summary>
        ///  Indicates that registration is open to the public.
        ///  If this is not the case, registration is by invitation only.
        /// </summary>

        public bool AllowRegistration { get; private set; }
    }
}
