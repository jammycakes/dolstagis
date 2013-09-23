using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web.Handlers;
using StructureMap.Configuration.DSL;

namespace Dolstagis.Web
{
    public class Module
    {
        private List<HandlerDefinition> handlers = new List<HandlerDefinition>();

        public IList<HandlerDefinition> Handlers { get { return handlers; } }

        public Registry Services { get; private set; }

        public Module()
        {
            this.Services = new Registry();
        }

        /// <summary>
        ///  Adds a controller to the module definition.
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        public void AddHandler<THandler>()
        {
            this.handlers.Add(new HandlerDefinition(typeof(THandler)));
        }
    }
}
