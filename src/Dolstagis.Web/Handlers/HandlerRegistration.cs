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

        public HandlerDefinition Definition { get; internal set; }

        public HandlerRegistration Parent { get; private set; }

        public HandlerRegistration()
        {
            this.Definition = null;
            this.Parent = null;
        }

        private HandlerRegistration(HandlerRegistration parent)
        {
            this.Definition = null;
            this.Parent = parent;
        }

        public HandlerRegistration GetChild(string name)
        {
            HandlerRegistration result;
            return children.TryGetValue(name, out result) ? result : null;
        }

        internal HandlerRegistration GetOrCreateChild(string name)
        {
            var result = GetChild(name);
            if (result == null) {
                result = new HandlerRegistration(this);
                this.children.Add(name, result);
            }
            return result;
        }
    }
}
