using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using CommonInstanceFactory.Extensions.Wcf;
using CommonInstanceFactory.Extensions.Wcf.Ninject;
using Ninject;
using NinjectModules = Anewluv.Services.DependencyResolution.Ninject.Modules;


namespace Anewluv.Web.Services.Members.ServiceHostFactories
{
    public class NinjectServiceHostFactory : InjectedServiceHostFactory<IKernel>
    {
        protected override IKernel CreateContainer()
        {
            IKernel container = new StandardKernel();
            container.Load<NinjectModules.AnewLuvContextModule>();  //they both share the context so load it here and only once
            container.Load<NinjectModules.PostalDataContextModule>(); //used by mapper and posibly members as well
            //TO do create one module combinding all                     
            container.Load<NinjectModules.MembersModule>();   
            container.Load<NinjectModules.MembersMapperModule>();
            //container.Load<NinjectModules.GeoModule>();
          //  //to do just add this mapping peice to data I think not sure if we need to decouple it like this
           // container.Load<NinjectModules.MembersMapperModule>();
           // container.Load<NinjectModules.MembersModule>();
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