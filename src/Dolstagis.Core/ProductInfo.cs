using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Dolstagis.Core
{
    public static class ProductInfo
    {
        public static string Company { get; private set; }

        public static string Product { get; private set; }

        public static string Copyright { get; private set; }

        public static string Trademark { get; private set; }

        public static string Culture { get; private set; }

        public static Version Version { get; private set; }

        public static string InformationalVersion { get; private set; }

        private static string GetAttribute<TAttribute>(Func<TAttribute, string> getter)
            where TAttribute : Attribute
        {
            var attr = typeof(ProductInfo).Assembly.GetCustomAttributes(typeof(TAttribute), true)
                .FirstOrDefault()
                as TAttribute;
            if (attr != null) return getter(attr);
            return default(string);
        }

        static ProductInfo()
        {
            Company = GetAttribute<AssemblyCompanyAttribute>(x => x.Company);
            Product = GetAttribute<AssemblyProductAttribute>(x => x.Product);
            Copyright = GetAttribute<AssemblyCopyrightAttribute>(x => x.Copyright);
            Trademark = GetAttribute<AssemblyTrademarkAttribute>(x => x.Trademark);
            Culture = GetAttribute<AssemblyCultureAttribute>(x => x.Culture);
            Version = typeof(ProductInfo).Assembly.GetName().Version;
            InformationalVersion = GetAttribute<AssemblyInformationalVersionAttribute>(x => x.InformationalVersion);
        }
    }
}
