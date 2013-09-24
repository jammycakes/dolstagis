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

        public string Path { get; private set; }

        public HandlerRegistration()
        {
            this.Definition = null;
            this.Parent = null;
            this.Path = "/";
        }

        private HandlerRegistration(HandlerRegistration parent, string name)
        {
            this.Definition = null;
            this.Parent = parent;
            this.Path = this.Parent.Path + name + "/";
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
                result = new HandlerRegistration(this, name);
                this.children.Add(name, result);
            }
            return result;
        }

        public bool IsValid
        {
            get
            {
                return this.Definition != null && this.Definition.Type != null;
            }
        }
    }
}
