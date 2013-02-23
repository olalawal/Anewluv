

using Ninject;
using NinjectModules = Shell.MVC2.DependencyResolution.Ninject.Modules;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Shell.MVC2.Web.Chat.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Shell.MVC2.Web.Chat.App_Start.NinjectWebCommon), "Stop")]


namespace Shell.MVC2.Web.Chat.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Shell.MVC2.DependencyResolution.Ninject.Infrastructure;
    using Microsoft.AspNet.SignalR;

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
            
            kernel.Load<NinjectModules.ApiKeyContextModule>();
            kernel.Load<NinjectModules.AnewLuvContextModule>();
            kernel.Load<NinjectModules.ChatContextModule>();
            kernel.Load<NinjectModules.ChatModule>();
          
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
           // Microsoft.AspNet.SignalR.set(new SignalR.Ninject.NinjectDependencyResolver(kernel));
            //var resolver = new NinjectSignalRDependencyResolver(kernel);
            // SignalR Injection
            GlobalHost.DependencyResolver = new NinjectSignalRDependencyResolver(kernel);


            
        }        
    }
}
