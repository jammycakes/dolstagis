using Dolstagis.Core;
using Dolstagis.Core.Caching;
using Dolstagis.Core.IO;
using Dolstagis.Core.Templates;
using Dolstagis.Web.Helpers.Caching;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using NHibernate;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Dolstagis.Web.Infrastructure.Config.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethod(typeof(Dolstagis.Web.Infrastructure.Config.NinjectWebCommon), "Stop")]

namespace Dolstagis.Web.Infrastructure.Config
{
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Load(
                new Dolstagis.Core.Data.NHibernateNinjectModule("Dolstagis"),
                new Dolstagis.Core.CoreNinjectModule(),
                new Dolstagis.Accounts.AccountsNinjectModule(),
                new Dolstagis.Web.Helpers.HelperNinjectModule()
            );
            kernel.Bind<ISession>().ToMethod(x => x.Kernel.Get<ISessionFactory>().OpenSession())
                .When(x => HttpContext.Current != null)
                .InRequestScope()
                .OnDeactivation(x => x.Flush());
            kernel.Bind<ITemplateEngine>().To<SimpleTemplateEngine>().InSingletonScope();
            kernel.Bind<IFilespace>()
                .ToMethod(x => new LocalFilespace(HostingEnvironment.MapPath("~/Views")))
                .WhenInjectedInto<ITemplateEngine>();
            kernel.Bind<ICache>().ToMethod(x => new HttpRuntimeCache(HttpRuntime.Cache));
        }
    }
}
