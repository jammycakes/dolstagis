using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Dolstagis.Web.Http;

namespace Dolstagis.Web.Aspnet
{
    public class AspnetRequestContext : IRequestContext
    {
        private HttpContextBase context;

        public AspnetRequestContext(HttpContextBase context)
        {
            this.context = context;
            this.Application = new ApplicationInfo(context);
            this.Request = new Request(context.Request);
            this.Response = new Response(context.Response);
        }

        public IApplicationInfo Application { get; private set; }

        public IRequest Request { get; private set; }

        public IResponse Response { get; private set; }
    }
}
