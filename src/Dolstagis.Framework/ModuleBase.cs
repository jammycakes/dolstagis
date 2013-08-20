using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate;
using FluentNHibernate.Mapping;
using Ninject.Modules;

namespace Dolstagis.Framework
{
    /// <summary>
    ///  Defines a module for our application. As well as registering Ninject
    ///  bindings, it will also register NHibernate mappings and database
    ///  migrations.
    /// </summary>

    public abstract class ModuleBase : NinjectModule
    {
        private IEnumerable<Type> EnumerateSubclasses(Type baseClass)
        {
            var rootNamespace = this.GetType().Namespace;
            var rootNamespace2 = rootNamespace + ".";

            return this.GetType().Assembly.GetTypes()
                .Where(t => baseClass.IsAssignableFrom(t) &&
                    (t.Namespace == rootNamespace || t.Namespace.StartsWith(rootNamespace2)));
        }

        /// <summary>
        ///  Gets the mapping classes to register with NHibernate.
        /// </summary>
        /// <returns>A list of <see cref="Type"/> instances.</returns>
        /// <remarks>
        ///  The default behaviour here is to use all mapping classes in the
        ///  same assembly and namespace (including child namespaces) as the
        ///  module itself.
        /// </remarks>

        public virtual IEnumerable<Type> GetNHibernateMappings()
        {
            return this.EnumerateSubclasses(typeof(IMappingProvider));
        }

        /// <summary>
        ///  Registers bindings with the IOC container.
        /// </summary>
        /// <remarks>
        ///  Classes that override this method MUST call this implementation
        ///  using base.Load() otherwise the sky will fall on your head!
        /// </remarks>

        public override void Load()
        {
            /*
             * This allows us to get all the loaded modules with a simple IOC resolution.
             */

            Bind<ModuleBase>().ToConstant(this).InSingletonScope();
        }
    }
}
