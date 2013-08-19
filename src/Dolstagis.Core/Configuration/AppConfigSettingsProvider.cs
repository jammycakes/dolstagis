using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolstagis.Core.Configuration
{
    public class AppConfigSettingsProvider : ISettingsProvider
    {
        public bool TryGetSetting(string ns, string key, out object value)
        {
            value = ConfigurationManager.AppSettings[ns + "." + key];
            return (value != null);
        }

        public System.Configuration.ConnectionStringSettings GetConnectionString(string connectionStringName)
        {
            return ConfigurationManager.ConnectionStrings[connectionStringName];
        }
    }
}
