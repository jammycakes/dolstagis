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
        public IList<Module> Modules { get; private set; }

        public IList<HandlerDefinition> HandlerDefinitions { get; private set; }

        public HandlerRegistry(IEnumerable<Module> modules)
        {
            this.Modules = modules.ToList().AsReadOnly();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void Load()
        {
            if (HandlerDefinitions == null) {
                HandlerDefinitions =
                    (from module in Modules from handler in module.Handlers select handler)
                    .ToList().AsReadOnly();
            }
        }
    }
}
