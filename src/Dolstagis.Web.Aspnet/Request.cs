using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Dolstagis.Web.Aspnet
{
    public class Request : IRequest
    {
        private HttpRequestBase httpRequest;

        public Request(HttpRequestBase httpRequest)
        {
            this.httpRequest = httpRequest;
        }
    }
}
