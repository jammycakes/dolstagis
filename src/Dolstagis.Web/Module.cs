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
        ///  Adds a handler to the module definition.
        /// </summary>
        /// <typeparam name="THandler">
        ///  The type of the handler to add.
        /// </typeparam>
        public void AddHandler<THandler>()
        {
            this.handlers.Add(new HandlerDefinition(this, typeof(THandler)));
        }

        /// <summary>
        ///  Adds a handler to the module definition, with an explicitly specified route.
        /// </summary>
        /// <typeparam name="THandler">
        ///  The type of the handler to add.
        /// </typeparam>
        /// <param name="route">
        ///  The route to the handler.
        /// </param>

        public void AddHandler<THandler>(string route)
        {
            this.handlers.Add(new HandlerDefinition(this, typeof(THandler), route));
        }
    }
}
