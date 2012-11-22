using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using CommonInstanceFactory.Extensions.Wcf;
using CommonInstanceFactory.Extensions.Wcf.Ninject;
using Ninject;
using NinjectModules = Shell.MVC2.DependencyResolution.Ninject.Modules;


namespace Shell.MVC2.Web.MediaService.ServiceHostFactories
{
    public class NinjectServiceHostFactory : InjectedServiceHostFactory<IKernel>
    {
        protected override IKernel CreateContainer()
        {
            IKernel container = new StandardKernel();
            container.Load<NinjectModules.ApiKeyContextModule>();
            container.Load<NinjectModules.AnewLuvContextModule>();
           // container.Load<NinjectModules.DatingContextModule>();
           // container.Load<NinjectModules.DatingServiceModule>();
           // container.Load<NinjectModules.DatingServicesModule>();
            container.Load<NinjectModules.MediaModule>();
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