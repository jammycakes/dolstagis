using System;
namespace Dolstagis.Web
{
    public interface IActionLocator
    {
        IAction GetAction(IRequest request);
    }
}
