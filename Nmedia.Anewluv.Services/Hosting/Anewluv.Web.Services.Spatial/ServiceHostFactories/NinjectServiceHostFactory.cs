﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using CommonInstanceFactory.Extensions.Wcf;
using CommonInstanceFactory.Extensions.Wcf.Ninject;
using Ninject;
using NinjectModules = Anewluv.Services.DependencyResolution.Ninject.Modules;


namespace Anewluv.Web.Services.Spatial.ServiceHostFactories
{
    public class NinjectServiceHostFactory : InjectedServiceHostFactory<IKernel>
    {
        protected override IKernel CreateContainer()
        {
            IKernel container = new StandardKernel();    
            container.Load<NinjectModules.GeoModule>();
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