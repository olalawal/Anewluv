using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using CommonInstanceFactory.Extensions.Wcf;
using CommonInstanceFactory.Extensions.Wcf.Ninject;
using Ninject;
using NinjectModules =  Nmedia.Infrastructure.DependencyResolution.Ninject.Modules;


namespace Nmedia.Infrastructure.Web.Services.Notification.ServiceHostFactories
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

        
           // container.Load<NinjectModules.LoggingContextModule>();
           // container.Load<NinjectModules.AnewLuvContextModule>();
           // container.Load<NinjectModules.ApiKeyContextModule>();        
            container.Load<NinjectModules.NotificationServiceModule>();
                           
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