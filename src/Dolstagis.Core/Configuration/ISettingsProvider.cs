using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolstagis.Core.Configuration
{
    public interface ISettingsProvider
    {
        /// <summary>
        ///  Gets an application configuration setting, if it has been defined.
        /// </summary>
        /// <param name="ns">
        ///  The setting namespace.
        /// </param>
        /// <param name="key">
        ///  The name of the setting to get.
        /// </param>
        /// <param name="value">
        ///  A variable which receives the setting value.
        /// </param>
        /// <returns>
        ///  true if the setting was found, otherwise false.
        /// </returns>
        bool TryGetSetting(string ns, string key, out object value);

        /// <summary>
        ///  Gets a connection string from the settings.
        /// </summary>
        /// <param name="connectionStringName">
        ///  The name of the connection string to fetch.
        /// </param>
        /// <returns>
        ///  A <see cref="ConnectionStringSettings"/> object, or null if this
        ///  connection string was not configured.
        /// </returns>
        ConnectionStringSettings GetConnectionString(string connectionStringName);
    }
}
