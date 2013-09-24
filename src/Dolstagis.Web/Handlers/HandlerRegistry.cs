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

        public HandlerRegistration Root { get; private set; }

        public HandlerRegistry(IEnumerable<Module> modules)
        {
            this.modules = modules.ToList();
            foreach (var module in this.modules) {
                AddModule(module);
            }
        }

        public void AddModule(Module module)
        {
            foreach (var handler in module.Handlers) {
                AddHandler(handler);
            }
        }

        public void AddHandler<THandler>()
        {
            AddHandler(new HandlerDefinition(null, typeof(THandler)));
        }

        public void AddHandler<THandler>(string route)
        {
            AddHandler(new HandlerDefinition(null, typeof(THandler), route));
        }

        private void AddHandler(HandlerDefinition definition)
        {
            AddHandler(definition, definition.Path);
        }

        private void AddHandler(HandlerDefinition definition, string path)
        {
            if (Root == null) Root = new HandlerRegistration();
            var parts = SplitPath(path);
            var entry = Root;
            foreach (var part in parts) {
                entry = entry.GetOrCreateChild(part);
            }
            entry.Definition = definition;
        }

        private string[] SplitPath(string path)
        {
            if (path == null) return new string[0];
            return path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
