using System;
using Dolstagis.Web.Http;

namespace Dolstagis.Web
{
    public interface IExceptionHandler
    {
        void HandleException(IRequestContext context, Exception ex);
    }
}
