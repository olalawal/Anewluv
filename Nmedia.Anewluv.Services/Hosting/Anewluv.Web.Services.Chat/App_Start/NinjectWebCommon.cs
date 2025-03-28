using NinjectModules = Anewluv.Services.DependencyResolution.Ninject.Modules;


[assembly: WebActivator.PreApplicationStartMethod(typeof(Anewluv.Web.Chat.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Anewluv.Web.Chat.App_Start.NinjectWebCommon), "Stop")]

namespace Anewluv.Web.Chat.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Shell.MVC2.DependencyResolution.Ninject.Infrastructure;
    using Shell.MVC2.SignalR.Hubs;

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


            var resolver = new NinjectSignalRDependencyResolver(kernel);
            Microsoft.AspNet.SignalR.GlobalHost.DependencyResolver = resolver;

            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {

            kernel.Bind<IDateTimeDependencyResolver>().To<DateTimeDependencyResolver>();
           // kernel.Load<NinjectModules.ApiKeyContextModule>();
           // kernel.Load<NinjectModules.AnewLuvContextModule>();
          //  kernel.Load<NinjectModules.ChatContextModule>();
            kernel.Load<NinjectModules.ChatModule>();
        }        
    }
}
