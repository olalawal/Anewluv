using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using CommonInstanceFactory.Extensions.Wcf;
using CommonInstanceFactory.Extensions.Wcf.Ninject;
using Ninject;
using NinjectModules = Shell.MVC2.DependencyResolution.Ninject.Modules;


namespace Shell.MVC2.Web.NotificationService.ServiceHostFactories
{
    public class NinjectServiceHostFactory : InjectedServiceHostFactory<IKernel>
    {
        /// <summary>
        /// TO DO might split info notification away from error notification
        /// </summary>
        /// <returns></returns>
        protected override IKernel CreateContainer()
        {
            IKernel container = new StandardKernel();

        
           // container.Load<NinjectModules.CustomErrorLogContextModule>();
            container.Load<NinjectModules.AnewLuvContextModule>();
            container.Load<NinjectModules.NotificationContextModule >();
            container.Load<NinjectModules.ErrorNotificationModule>();
            container.Load<NinjectModules.InfoNotificationModule>();           
            return container;
        }

        protected override ServiceHost CreateInjectedServiceHost
            (IKernel container, Type serviceType, Uri[] baseAddresses)
        {
            ServiceHost serviceHost = new NinjectServiceHost
                (container, serviceType, baseAddresses);
            return serviceHost;
        }
    }
}