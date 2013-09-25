using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Dolstagis.Web.Http;

namespace Dolstagis.Web.Aspnet
{
    public class ApplicationInfo : IApplicationInfo
    {
        public ApplicationInfo(HttpContextBase context)
        {
            this.Root = context.Request.ApplicationPath;
            if (!this.Root.EndsWith("/")) this.Root += "/";
            this.PhysicalRoot = context.Request.PhysicalApplicationPath;
        }

        public string Root { get; private set; }

        public string PhysicalRoot { get; private set; }
    }
}
