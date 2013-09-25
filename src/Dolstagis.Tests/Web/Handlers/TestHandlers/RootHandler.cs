using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web.Routing;

namespace Dolstagis.Tests.Web.Handlers.TestHandlers
{
    [Route("/")]
    public class RootHandler
    {
        public object Get()
        {
            return null;
        }

        [Parameters("args")]
        public object Post(string args)
        {
            return null;
        }
    }
}
