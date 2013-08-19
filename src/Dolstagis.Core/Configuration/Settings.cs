﻿using System;
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
        private string GetBaseName()
        {
            return this.GetType().Namespace + ".";
        }

        public Settings()
        {
            string baseName = GetBaseName();

            foreach (var prop in this.GetType().GetProperties()) {
                if (prop.CanWrite && !prop.GetIndexParameters().Any()) {
                    string key = baseName + prop.Name;
                    string value = ConfigurationManager.AppSettings[key];
                    if (value != null) {
                        SetValue(prop, value, key);
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

        private void SetValue(PropertyInfo prop, string value, string key)
        {
            if (prop.PropertyType.IsEnum) {
                try {
                    prop.SetValue(this, Enum.Parse(prop.PropertyType, value, true), null);
                }
                catch (ArgumentException ex) {
                    ThrowConfigException(key, ex);
                }
            }
            else {
                try {
                    if (prop.PropertyType.IsAssignableFrom(typeof(string))) {
                        prop.SetValue(this, value, null);
                    }
                    else if (prop.PropertyType.IsAssignableFrom(typeof(Int32))) {
                        prop.SetValue(this, Int32.Parse(value), null);
                    }
                    else if (prop.PropertyType.IsAssignableFrom(typeof(Boolean))) {
                        prop.SetValue(this, Boolean.Parse(value), null);
                    }
                    else if (prop.PropertyType.IsAssignableFrom(typeof(DateTime))) {
                        prop.SetValue(this, DateTime.Parse(value), null);
                    }
                    else if (prop.PropertyType.IsAssignableFrom(typeof(TimeSpan))) {
                        try {
                            prop.SetValue(this, TimeSpan.Parse(value), null);
                        }
                        catch (OverflowException ex) {
                            ThrowConfigException(key, ex);
                        }
                    }
                }
                catch (FormatException ex) {
                    ThrowConfigException(key, ex);
                }
            }
        }
    }
}