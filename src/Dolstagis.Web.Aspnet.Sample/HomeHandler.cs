﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dolstagis.Web.Routing;

namespace Dolstagis.Web.Aspnet.Sample
{
    /// <summary>
    ///  This is a sample route handler to handle requests for the home page.
    ///  By default, we will enforce the "one route, one handler" pattern, since
    ///  anything else is a violation of the Single Responsibiltiy Principle.
    /// </summary>

    [Route("/")]
    public class HomeHandler
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
        ///  This one handles POST requests.
        /// </summary>
        /// <param name="data">
        ///  Comes from the form.
        /// </param>
        /// <returns></returns>

        public string Post(string data)
        {
            return "Data: " + data;
        }
    }
}