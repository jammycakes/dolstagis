using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web.Views;

namespace Dolstagis.Web
{
    public class ViewBuilder : IViewBuilder
    {
        public IView CreateView(object model)
        {
            return new TextView(model.ToString());
        }
    }
}
