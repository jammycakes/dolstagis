using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web.Controllers;

namespace Dolstagis.Web
{
    public class Module : StructureMap.Configuration.DSL.Registry
    {
        private List<Controller> controllers = new List<Controller>();

        public IList<Controller> Controllers { get { return controllers; } }

        /// <summary>
        ///  Adds a controller to the module definition.
        /// </summary>
        /// <typeparam name="TController"></typeparam>
        public void AddController<TController>()
        {
            this.controllers.Add(new Controller(typeof(TController)));
        }
    }
}
