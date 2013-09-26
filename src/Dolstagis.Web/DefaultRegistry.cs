using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web.Handlers;
using Dolstagis.Web.Static;
using StructureMap.Configuration.DSL;

namespace Dolstagis.Web
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            For<IRequestProcessor>().Use<RequestProcessor>();
            For<IAction>().Use<Action>();
            For<IActionLocator>().Use<ActionLocator>();
            For<IViewBuilder>().Use<ViewBuilder>();

            For<IMimeTypes>().Singleton().Use<MimeTypes>();
            For<HandlerRegistry>().Singleton().Use<HandlerRegistry>();
        }
    }
}
