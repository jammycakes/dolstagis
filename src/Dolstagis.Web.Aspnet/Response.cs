using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Dolstagis.Web.Aspnet
{
    public class Response : IResponse
    {
        private HttpResponseBase httpResponse;

        public Response(HttpResponseBase httpResponse)
        {
            this.httpResponse = httpResponse;
        }
    }
}
