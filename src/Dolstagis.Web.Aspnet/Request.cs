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
            this.AppRoot = httpRequest.ApplicationPath;
            if (!this.AppRoot.EndsWith("/")) this.AppRoot += "/";
            this.Url = httpRequest.Url;
            this.AppRelativePath = this.Url.AbsolutePath.Substring(this.AppRoot.Length);
            this.Method = httpRequest.HttpMethod;
        }

        public string AppRoot { get; private set; }

        public string AppRelativePath { get; private set; }

        public string Method { get; private set; }

        public Uri Url { get; private set; }
    }
}
