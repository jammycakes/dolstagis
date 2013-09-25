using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web.Http;

namespace Dolstagis.Web
{
    public class ExceptionHandler : Dolstagis.Web.IExceptionHandler
    {
        public void HandleException(IRequestContext context, Exception ex)
        {
        }
    }
}
