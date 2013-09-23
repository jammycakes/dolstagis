using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web.Handlers;

namespace Dolstagis.Web
{
    public class Module : StructureMap.Configuration.DSL.Registry
    {
        private List<Handler> handlers = new List<Handler>();

        public IList<Handler> Handlers { get { return handlers; } }

        /// <summary>
        ///  Adds a controller to the module definition.
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        public void AddHandler<THandler>()
        {
            this.handlers.Add(new Handler(typeof(THandler)));
        }
    }
}
