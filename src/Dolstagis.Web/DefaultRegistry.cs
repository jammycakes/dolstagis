using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap.Configuration.DSL;

namespace Dolstagis.Web
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            For<IRequestProcessor>().Use<RequestProcessor>();
        }
    }
}
