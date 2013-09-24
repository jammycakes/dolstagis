using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dolstagis.Web
{
    public interface IAction
    {
        Type HandlerType { get; set; }

        MethodInfo Method { get; set; }

        object[] Parameters { get; set; }

        object Invoke();
    }
}
