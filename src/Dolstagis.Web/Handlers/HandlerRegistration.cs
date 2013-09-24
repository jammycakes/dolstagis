using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolstagis.Web.Handlers
{
    public class HandlerRegistration
    {
        private IDictionary<string, HandlerRegistration> children
            = new Dictionary<string, HandlerRegistration>(StringComparer.OrdinalIgnoreCase);

        public HandlerDefinition Definition { get; private set; }

        public HandlerRegistration Parent { get; private set; }

        public HandlerRegistration GetChild(string name)
        {
            HandlerRegistration result;
            return children.TryGetValue(name, out result) ? result : null;
        }

        public HandlerRegistration(HandlerDefinition definition, HandlerRegistration parent, string name)
        {
            this.Definition = definition;
            this.Parent = parent;
            this.Parent.children[name] = this;
        }
    }
}
