using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using StructureMap;

namespace Dolstagis.Web
{
    public class ActionInvoker : IActionInvoker
    {
        private IContainer container;

        public ActionInvoker(IContainer container)
        {
            this.container = container;
        }

        public Type HandlerType { get; set; }

        public MethodInfo Method { get; set; }

        public object[] Parameters { get; set; }

        public object Invoke()
        {
            object instance = this.container.GetInstance(HandlerType);
            return Method.Invoke(instance, Parameters.ToArray());
        }
    }
}
