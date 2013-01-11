using Dolstagis.Core;
using Dolstagis.Core.IO;
using Dolstagis.Core.Templates;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using NHibernate;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Web;
using System.Web.Hosting;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Dolstagis.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethod(typeof(Dolstagis.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Dolstagis.Web.App_Start
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
                new Dolstagis.Web.Helpers.HelperNinjectModule()
            );
            kernel.Bind<LazyDisposable<ISession>>()
                .ToMethod(
                    x => new LazyDisposable<ISession>
                        (() => x.Kernel.Get<ISessionFactory>().OpenSession())
                )
                .When(x => HttpContext.Current != null)
                .InRequestScope();
            kernel.Bind<ITemplateEngine>().To<SimpleTemplateEngine>().InSingletonScope();
            kernel.Bind<IFilespace>()
                .ToMethod(x => new LocalFilespace(HostingEnvironment.MapPath("~/Views")))
                .WhenInjectedInto<ITemplateEngine>();
        }
    }
}
