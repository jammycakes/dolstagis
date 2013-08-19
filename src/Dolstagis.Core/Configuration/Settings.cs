using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Dolstagis.Core.Configuration
{
    /// <summary>
    ///  Provides a base class for reading settings from the appSettings section
    ///  of the web.config file.
    /// </summary>
    /// <remarks>
    ///  <para>
    ///   To add a new configuration setting, simply create a new property in the
    ///   derived class. It will automatically be configured by reflection if it is
    ///   present in the base class. Properties may have private setters if desired.
    ///  </para>
    ///  <para>
    ///   By default, appSettings should be prefixed by the namespace of the class
    ///   in which they are defined. See the test cases for an example.
    ///  </para>
    /// </remarks>

    public class Settings
    {
        private static IDictionary<Type, Settings> cachedSettings
            = new Dictionary<Type, Settings>();

        public static IList<ISettingsProvider> Providers { get; private set; }

        static Settings()
        {
            Providers = new List<ISettingsProvider>(new ISettingsProvider[] {
                new AppConfigSettingsProvider()
            });
        }

        public static T Get<T>() where T : Settings, new()
        {
            Settings cached;
            if (cachedSettings.TryGetValue(typeof(T), out cached)) {
                if (cached is T) return (T)cached;
            }

            var result = new T();
            cachedSettings[typeof(T)] = result;
            return result;
        }

        public Settings()
        {
            Configure();
        }

        private bool TryGetSetting(string ns, string key, out object value)
        {
            foreach (var provider in Providers) {
                if (provider.TryGetSetting(ns, key, out value)) {
                    return true;
                }
            }
            value = null;
            return false;
        }

        private void Configure()
        {
            string ns = this.GetType().Namespace;

            foreach (var prop in this.GetType().GetProperties()) {
                if (prop.CanWrite && !prop.GetIndexParameters().Any()) {
                    string key = prop.Name;

                    object value;
                    if (TryGetSetting(ns, key, out value)) {
                        try {
                            SetValue(prop, value, key);
                        }
                        catch (Exception ex) {
                            ThrowConfigException(key, ex);
                        }
                    }
                    else if (prop.GetCustomAttributes(typeof(RequiredAttribute), true).Any()) {
                        throw new ConfigurationErrorsException(String.Format
                            ("Required value {0} has not been configured.", key));
                    }
                }
            }
        }

        private void ThrowConfigException(string key, Exception innerException)
        {
            throw new ConfigurationErrorsException("Could not configure value: " + key, innerException);
        }

        private void SetValue(PropertyInfo prop, object value, string key)
        {
            if (value == null) {
                prop.SetValue(this, null);
            }
            else if (prop.PropertyType.IsAssignableFrom(value.GetType())) {
                prop.SetValue(this, value);
            }
            else {
                string str = value.ToString();
                if (prop.PropertyType.IsAssignableFrom(typeof(string))) {
                    prop.SetValue(this, str);
                }
                else if (prop.PropertyType.IsAssignableFrom(typeof(Int32))) {
                    prop.SetValue(this, Int32.Parse(str), null);
                }
                else if (prop.PropertyType.IsAssignableFrom(typeof(Boolean))) {
                    prop.SetValue(this, Boolean.Parse(str), null);
                }
                else if (prop.PropertyType.IsAssignableFrom(typeof(DateTime))) {
                    prop.SetValue(this, DateTime.Parse(str), null);
                }
                else if (prop.PropertyType.IsAssignableFrom(typeof(TimeSpan))) {
                    prop.SetValue(this, TimeSpan.Parse(str), null);
                }
                else if (prop.PropertyType.IsEnum) {
                    prop.SetValue(this, Enum.Parse(prop.PropertyType, str, true));
                }
            }
        }
    }
}
