using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web.Routing;

namespace Dolstagis.Tests.Web.Handlers.TestHandlers
{
    [Route("/group/one")]
    public class FirstHandler
    {
        [Parameters("forename/surname")]
        public object Get(string forename, string surname)
        {
            return null;
        }
    }
}
