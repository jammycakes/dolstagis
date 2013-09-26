using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web.Views;

namespace Dolstagis.Web
{
    public interface IViewBuilder
    {
        IView CreateView(object model);
    }
}
