using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate;
using FluentNHibernate.Mapping;
using Ninject.Modules;

namespace Dolstagis.Core
{
    /// <summary>
    ///  Defines a module for our application. As well as registering Ninject
    ///  bindings, it will also register NHibernate mappings and database
    ///  migrations.
    /// </summary>

    public abstract class Module : NinjectModule
    {
        private IEnumerable<Type> EnumerateSubclasses(Type baseClass)
        {
            var rootNamespace = this.GetType().Namespace;
            var rootNamespace2 = rootNamespace + ".";

            return this.GetType().Assembly.GetTypes()
                .Where(t => baseClass.IsAssignableFrom(t) &&
                    (t.Namespace == rootNamespace || t.Namespace.StartsWith(rootNamespace2)));
        }

        public virtual IEnumerable<Type> GetNHibernateMappings()
        {
            return this.EnumerateSubclasses(typeof(IMappingProvider));
        }
    }
}
