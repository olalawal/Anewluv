using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using CommonInstanceFactory.Extensions.Wcf;
using CommonInstanceFactory.Extensions.Wcf.Ninject;
using Ninject;
using NinjectModules = Anewluv.Services.DependencyResolution.Ninject.Modules;


         
namespace Anewluv.Web.Services.MemberActions.ServiceHostFactories
{
    public class NinjectServiceHostFactory : InjectedServiceHostFactory<IKernel>
    {
        protected override IKernel CreateContainer()
        {
            IKernel container = new StandardKernel();
            //container.Load<NinjectModules.ApiKeyContextModule>();
            // container.Load<NinjectModules.AnewLuvContextModule>();
            // container.Load<NinjectModules.PostalDataContextModule>();
            //loading membership module first since they both share IMEMberepo, and mappers module server items from membership module
            //  container.Load<NinjectModules.MembersModule>();
            // container.Load<NinjectModules.MembershipModule>();
            // container.Load<NinjectModules.MembersMapperModule  >();
            container.Load<NinjectModules.MemberActionsModule>();

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