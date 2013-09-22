using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dolstagis.Web
{
    public interface IActionInvoker
    {
        object Invoke(Type type, MethodInfo method, object[] parameters);
    }
}
