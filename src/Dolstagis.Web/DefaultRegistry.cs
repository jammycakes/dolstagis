using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web.Handlers;
using StructureMap.Configuration.DSL;

namespace Dolstagis.Web
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            For<IRequestProcessor>().Use<RequestProcessor>();
            For<IActionInvoker>().Use<ActionInvoker>();
            For<HandlerRegistry>().Singleton().Use<HandlerRegistry>();
        }
    }
}
