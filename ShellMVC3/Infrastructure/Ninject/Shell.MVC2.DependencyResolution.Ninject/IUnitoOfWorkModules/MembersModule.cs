


using System;
using System.Collections.Generic;
using System.Linq;


using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Activation;
using Ninject;
using Nmedia.DataAccess.Interfaces;


//to do do away with this when we go to code first , we would pull this from entities 

using Anewluv.Services.Contracts;
using Anewluv.Domain;
using System.Data.Entity;
using System.ServiceModel;
using Anewluv.Services.Members;


//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class MembersModule : NinjectModule
	{
		public override void Load()
		{

            //  if (!Kernel.HasModule("Shell.MVC2.DependencyResolution.Ninject.Modules.MembersModule")) //only load if not already loaded into kernel
            //      this.Bind<AnewLuvContextModule>()
            //this.Bind<AnewluvContext>().ToSelf().InRequestScope();
            //this.Bind<WellsFargo.DataAccess.Interfaces.IContext>().ToConstructor(x => new PromotionContext()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("WellsFargo.Promotion.Services.PromotionService")).InTransientScope ();
            //this.Bind<WellsFargo.DataAccess.Interfaces.IContext>().ToMethod(ctx => ctx.Kernel.Get<PromotionContext>());//).ToMethod()(x => new PromotionContext()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("WellsFargo.Promotion.Services.PromotionService")).InTransientScope();

            // var webApiEFRepository = kernel.Get<IRepository<Entity>>("WebApiEFRepository");
            //  this.Unbind(typeof(IUnitOfWork));
            //Kernel.Bind<IUnitOfWork>().ToConstructor(ctorArg => new EFUnitOfWork(ctorArg.Inject<WellsFargo.DataAccess.Interfaces.IContext>())).InTransientScope();
            this.Bind<IUnitOfWork>().ToMethod(ctx => ctx.Kernel.Get<AnewluvContext>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Anewluv.Services.Members.MemberService")).InScope(c => OperationContext.Current); ;
            // this.Unbind(typeof(DbContext));
            this.Bind<DbContext>().ToMethod(ctx => ctx.Kernel.Get<AnewluvContext>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Anewluv.Services.Members.MemberService")).InScope(c => OperationContext.Current); 
                ;

            //the Unit of work module should already be loaded by now
           // this.Bind<IMemberService>().ToSelf();
            Bind<IMemberService, MemberService>().To<MemberService>().InRequestScope();


         
      }
	}
}