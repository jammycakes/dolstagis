using Dolstagis.Core.Data;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using NHibernate;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Web;
using System.Web.Mvc;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Dolstagis.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Dolstagis.Web.App_Start.NinjectWebCommon), "Stop")]

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

        private static bool IsInjectedInto<T>(Ninject.Activation.IRequest request)
        {
            while (request != null) {
                if (request.Service != null && typeof(T).IsAssignableFrom(request.Service))
                    return true;
                request = request.ParentRequest;
            }
            return false;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Load(new DbNinjectModule("Dolstagis"));
            kernel.Bind<ISession>()
                .ToMethod(x => x.Kernel.Get<ISessionFactory>().OpenSession())
                .When(x => IsInjectedInto<IController>(x) || IsInjectedInto<IActionFilter>(x))
                .InRequestScope();
        }
    }
}
