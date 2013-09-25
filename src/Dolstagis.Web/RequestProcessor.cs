using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web.Http;

namespace Dolstagis.Web
{
    public class RequestProcessor : IRequestProcessor
    {
        private IActionLocator actionLocator;
        private Func<IViewBuilder> getViewBuilder;

        public RequestProcessor(IActionLocator actionLocator, Func<IViewBuilder> getViewBuilder)
        {
            this.actionLocator = actionLocator;
            this.getViewBuilder = getViewBuilder;
        }

        public void Process(IRequestContext context)
        {
            var action = actionLocator.GetAction(context.Request);
            if (action != null) {
                var model = action.Invoke();
                IView view = (model as IView) ?? getViewBuilder().CreateView(model);
                if (view != null) {
                    view.Render(context.Response);
                }
            }
        }
    }
}
