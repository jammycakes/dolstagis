using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web;
using Dolstagis.Web.Handlers;

namespace Dolstagis.Tests.Web.Handlers.TestHandlers
{
    public class TestModule : Module
    {
        public TestModule()
        {
            this.AddHandler<RootHandler>();
            this.AddHandler<FirstHandler>();
        }
    }
}
