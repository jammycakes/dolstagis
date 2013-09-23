using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolstagis.Web.Handlers
{
    public class HandlerRegistry
    {
        private Func<Module[]> modules;

        public HandlerRegistry(Func<Module[]> modules)
        {
            this.modules = modules;
        }
    }
}
