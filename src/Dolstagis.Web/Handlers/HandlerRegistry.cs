using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dolstagis.Web.Handlers
{
    public class HandlerRegistry
    {
        private IList<Module> modules;

        public HandlerRegistry(IEnumerable<Module> modules)
        {
            this.modules = modules.ToList().AsReadOnly();
        }
    }
}
