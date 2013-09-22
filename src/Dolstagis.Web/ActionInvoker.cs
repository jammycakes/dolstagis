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

        public object Invoke(Type type, MethodInfo method, object[] parameters)
        {
            object instance = this.container.GetInstance(type);
            return method.Invoke(instance, parameters);
        }
    }
}
