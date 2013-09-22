using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Dolstagis.Web.Aspnet
{
    public class DolstagisHttpModule : IHttpModule
    {
        private static Application application = new Application();
        private static int refCount = 0;

        public void Dispose()
        {
            lock (application) {
                if (--refCount == 0) {
                    application.Dispose();
                }
            }
        }

        public void Init(HttpApplication context)
        {
            refCount++;
            context.BeginRequest += (s, e) => context.Context.RemapHandler(new DolstagisHttpHandler(application));
        }
    }
}
