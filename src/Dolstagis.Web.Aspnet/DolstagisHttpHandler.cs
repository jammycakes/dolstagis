using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Dolstagis.Web.Aspnet
{
    public class DolstagisHttpHandler : IHttpHandler
    {
        private Application application;

        public DolstagisHttpHandler(Application application)
        {
            this.application = application;
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var rc = new AspnetRequestContext(new HttpContextWrapper(context));
            application.ProcessRequest(rc);
        }
    }
}
