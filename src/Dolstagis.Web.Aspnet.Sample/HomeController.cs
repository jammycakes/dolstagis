using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dolstagis.Web.Routing;

namespace Dolstagis.Web.Aspnet.Sample
{
    [Route("/")]
    public class HomeController
    {
        /// <summary>
        ///  When an action has no routing attribute, we match on HTTP method name.
        ///  So this will handle GET requests.
        /// </summary>
        /// <returns></returns>

        public string Get()
        {
            return "Hello world";
        }

        /// <summary>
        ///  Same is true when an action's routing attribute does not specify a method.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        [Route("{data}")]
        public string Get(string data)
        {
            return "Data: " + data;
        }

        /// <summary>
        ///  To specify a custom route and method, use both.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        [Route("subpage/{data}", "GET")]
        public string SubPage(string data)
        {
            return "SubPage: " + data;
        }
    }
}