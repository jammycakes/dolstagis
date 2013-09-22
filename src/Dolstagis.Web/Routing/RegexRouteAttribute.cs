using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dolstagis.Web.Routing
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class RegexRouteAttribute : RouteAttribute
    {
        public Regex Regex { get; private set; }

        public RegexRouteAttribute(string route, string method = null, RegexOptions options = RegexOptions.None)
            : base(route, method)
        {
            this.Regex = new Regex(route, options);
        }
    }
}
