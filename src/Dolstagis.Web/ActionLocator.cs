using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web.Handlers;

namespace Dolstagis.Web
{
    public class ActionLocator
    {
        private HandlerRegistry handlers;
        private Func<IAction> getInvoker;

        public ActionLocator(HandlerRegistry handlers, Func<IAction> getInvoker)
        {
            this.handlers = handlers;
            this.getInvoker = getInvoker;
        }

        public IAction GetAction(IRequest request)
        {
            return null;
        }
    }
}
