using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolstagis.Web.Routing
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class RouteAttribute : Attribute
    {
        public string Route { get; private set; }

        public string Method { get; private set; }

        public RouteAttribute(string route, string method = null)
        {
            this.Route = route;
            this.Method = method;
        }
    }
}
