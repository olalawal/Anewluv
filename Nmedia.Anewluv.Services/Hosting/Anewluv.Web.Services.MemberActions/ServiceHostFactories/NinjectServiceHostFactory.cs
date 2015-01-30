using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;
using NinjectModules = Anewluv.Services.DependencyResolution.Ninject.Modules;

namespace Anewluv.Web.Services.MemberActions
{
    public class NInjectServiceHostFactory : ServiceHostFactory
    {
        //protected override IKernel CreateContainer()
        //{
        //    IKernel container = new StandardKernel();
        //    //container.Load<NinjectModules.ApiKeyContextModule>();
        //    // container.Load<NinjectModules.AnewLuvContextModule>();
        //    // container.Load<NinjectModules.PostalDataContextModule>();
        //    //loading membership module first since they both share IMEMberepo, and mappers module server items from membership module
        //    //  container.Load<NinjectModules.MembersModule>();
        //    // container.Load<NinjectModules.MembershipModule>();
        //    // container.Load<NinjectModules.MembersMapperModule  >();
        //    container.Load<NinjectModules.MemberActionsModule>();

        //    return container;
        //}


        private readonly IKernel kernel;
       
        
        
        
        public NInjectServiceHostFactory()
        {
           kernel = new StandardKernel();
          //  kernel.Bind<IDummyDependency>().To<DummyDepencency>();

           kernel.Load<NinjectModules.MemberActionsModule>();

        }



        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new NInjectServiceHost(kernel, serviceType, baseAddresses);
        }
    }

    public class NInjectServiceHost : ServiceHost
    {
        public NInjectServiceHost(IKernel container, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            if (container == null) throw new ArgumentNullException("kernel");
            foreach (var cd in ImplementedContracts.Values)
            {
                cd.Behaviors.Add(new NInjectInstanceProvider(container));
            }
        }
    }

    public class NInjectInstanceProvider : IInstanceProvider, IContractBehavior
    {
        private readonly IKernel kernel;
        public NInjectInstanceProvider(IKernel kernel)
        {
            if (kernel == null) throw new ArgumentNullException("kernel");
            this.kernel = kernel;
        }
        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return GetInstance(instanceContext);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
           // var dd = kernel.Get(instanceContext.Host.Description.ServiceType);
           // if (dd == null) kernel.Load<NinjectModules.MemberActionsModule>();
            return kernel.Get(instanceContext.Host.Description.ServiceType);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {

            //var dd = kernel.GetModules();
           //var n =   dd.FirstOrDefault();
           // kernel.Unload("Anewluv.Services.DependencyResolution.Ninject.Modules.MemberActionsModule"); 

            // kernel.Dispose();
            kernel.Release(instance); //  dfault
          //  kernel.Rebind(instance);
            //nuclear option kill host 
            //instanceContext.Host.Close();// .Close();
            
        }

        public void AddBindingParameters(ContractDescription contractDescription,
            ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        { }

        public void ApplyClientBehavior(ContractDescription contractDescription,
            ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        { }

        public void ApplyDispatchBehavior(ContractDescription contractDescription,
            ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.InstanceProvider = this;
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        { }
    }
}