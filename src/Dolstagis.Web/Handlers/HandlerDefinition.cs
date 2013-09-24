using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web.Routing;

namespace Dolstagis.Web.Handlers
{
    public class HandlerDefinition
    {
        public Type Type { get; private set; }

        public string Path { get; private set; }

        public Module Module { get; private set; }

        public HandlerDefinition(Module module, Type type)
        {
            var attr = type.GetCustomAttributes(typeof(RouteAttribute), true).FirstOrDefault();
            if (attr == null) {
                throw new ArgumentException(
                    String.Format("Handler of type {0} does not have a [RouteAttribute], and no " +
                        "route was explicitly specified.", type.FullName), "type"
                );
            }

            this.Module = module;
            this.Type = type;
            this.Path = ((RouteAttribute)attr).Path;
        }

        public HandlerDefinition(Module module, Type type, string path)
        {
            this.Module = module;
            this.Type = type;
            this.Path = path;
        }
    }
}
