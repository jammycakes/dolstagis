using System;
using Dolstagis.Web.Http;

namespace Dolstagis.Web
{
    public interface IActionLocator
    {
        IAction GetAction(IRequest request);
    }
}
