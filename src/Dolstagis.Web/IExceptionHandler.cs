using System;
namespace Dolstagis.Web
{
    public interface IExceptionHandler
    {
        void HandleException(IRequestContext context, Exception ex);
    }
}
