using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Dolstagis.Core.Configuration
{
    public class RegistrySettingsProvider : ISettingsProvider
    {
        private RegistryKey root;
        private string baseKey;

        public RegistrySettingsProvider(RegistryKey root, string baseKey = null)
        {
            this.root = root;
            this.baseKey = baseKey ?? ProductInfo.Company + "\\" + ProductInfo.Product;
        }

        public bool TryGetSetting(string ns, string key, out object value)
        {
            var objKey = root.OpenSubKey(this.baseKey + "\\" + ns);
            if (objKey == null) {
                value = null;
                return false;
            }
            else {
                try {
                    value = objKey.GetValue(key);
                    return true;
                }
                finally {
                    objKey.Close();
                    objKey.Dispose();
                }
            }
        }

        private static string ToString(object obj)
        {
            if (obj == null) return null;
            return (obj as string) ?? obj.ToString();
        }

        public ConnectionStringSettings GetConnectionString(string connectionStringName)
        {
            var objKey = root.OpenSubKey("SOFTWARE\\" + this.baseKey + "\\ConnectionStrings\\" + connectionStringName);
            if (objKey == null) {
                return null;
            }
            else {
                try {
                    var connectionString = ToString(objKey.GetValue(null));
                    var providerName = ToString(objKey.GetValue("Provider"));
                    return new ConnectionStringSettings(connectionStringName, connectionString, providerName);
                }
                finally {
                    objKey.Close();
                    objKey.Dispose();
                }
            }
        }
    }
}
