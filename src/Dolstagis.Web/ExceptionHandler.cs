using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolstagis.Web
{
    public class ExceptionHandler : Dolstagis.Web.IExceptionHandler
    {
        public void HandleException(IRequestContext context, Exception ex)
        {
        }
    }
}
