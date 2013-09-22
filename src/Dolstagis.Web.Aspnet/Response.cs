using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public void AddHeader(string name, string value)
        {
            httpResponse.AppendHeader(name, value);
        }

        public System.IO.Stream ResponseStream
        {
            get { return httpResponse.OutputStream; }
        }

        public int StatusCode
        {
            get { return this.httpResponse.StatusCode; }
            set { this.httpResponse.StatusCode = value; }
        }

        public string StatusDescription
        {
            get { return this.httpResponse.StatusDescription; }
            set { this.httpResponse.StatusDescription = value; }
        }
    }
}
