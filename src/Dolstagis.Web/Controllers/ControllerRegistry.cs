using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolstagis.Web.Controllers
{
    public class ControllerRegistry
    {
        private Func<Module[]> modules;

        public ControllerRegistry(Func<Module[]> modules)
        {
            this.modules = modules;
        }
    }
}
