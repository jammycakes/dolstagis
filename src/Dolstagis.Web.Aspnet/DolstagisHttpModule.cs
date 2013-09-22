using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Dolstagis.Web.Aspnet
{
    public class DolstagisHttpModule : IHttpModule
    {
        public static Func<Application> ApplicationFactory { get; set; }

        private static Lazy<Application> application
            = new Lazy<Application>(CreateApplication, LazyThreadSafetyMode.ExecutionAndPublication);

        private static int refCount = 0;

        private static Application CreateApplication()
        {
            return ApplicationFactory != null ? ApplicationFactory() : new Application();
        }

        public void Dispose()
        {
            lock (application) {
                if (--refCount == 0 && application.IsValueCreated) {
                    application.Value.Dispose();
                }
            }
        }

        public void Init(HttpApplication context)
        {
            refCount++;
            context.BeginRequest += (s, e) => context.Context.RemapHandler(new DolstagisHttpHandler(application.Value));
        }
    }
}
