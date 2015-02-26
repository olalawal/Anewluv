


using System;
using System.Collections.Generic;
using System.Linq;


using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Activation;
using Ninject;
//using Nmedia.DataAccess.Interfaces;


//to do do away with this when we go to code first , we would pull this from entities 

using Anewluv.Services.Contracts;
using Anewluv.Domain;
using System.Data.Entity;
using System.ServiceModel;
using Nmedia.DataAcess.Test;
using Nmedia.DataAccess.Test;
//using Anewluv.Services.Members;


//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;

namespace Anewluv.Services.DependencyResolution.Ninject.Modules
{
	public class MembersModule : NinjectModule
	{
		public override void Load()
		{

            //  if (!Kernel.HasModule("Anewluv.Services.DependencyResolution.Ninject.Modules.MembersModule")) //only load if not already loaded into kernel
            //      this.Bind<AnewLuvContextModule>()
            //this.Bind<AnewluvContext>().ToSelf().InRequestScope();
            //this.Bind<WellsFargo.DataAccess.Interfaces.IContext>().ToConstructor(x => new PromotionContext()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("WellsFargo.Promotion.Services.PromotionService")).InTransientScope ();
            //this.Bind<WellsFargo.DataAccess.Interfaces.IContext>().ToMethod(ctx => ctx.Kernel.Get<PromotionContext>());//).ToMethod()(x => new PromotionContext()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("WellsFargo.Promotion.Services.PromotionService")).InTransientScope();

            // var webApiEFRepository = kernel.Get<IRepository<Entity>>("WebApiEFRepository");
            //  this.Unbind(typeof(IUnitOfWork));
            //Kernel.Bind<IUnitOfWork>().ToConstructor(ctorArg => new EFUnitOfWork(ctorArg.Inject<WellsFargo.DataAccess.Interfaces.IContext>())).InTransientScope();

           
            //new binding .. repositories will be handled by the unitof work for now, if this falis we will use generic repo
            this.Bind<AnewluvContext>().ToSelf().InRequestScope();
            this.Bind<IUnitOfWork<AnewluvContext>>().To<UnitOfWork<AnewluvContext>>();


           


           // this.Bind<UnitOfWork>().ToConstructor(x => new AnewluvContext());//.When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Anewluv.Services.Members.MemberService"));
           
           //  this.Bind<DbContext>().ToMethod(ctx => ctx.Kernel.Get<AnewluvContext>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Anewluv.Services.Members.MemberService")); 
                ;

            //the Unit of work module should already be loaded by now
           // Bind<IMemberService>().ToSelf().InRequestScope();

            //Bind<IMemberService, MemberService>().To<MemberService>().InRequestScope();


          


           // Bind<IUnitOfWork>().To<AnewluvContext>().InRequestScope();
           //// Bind<IUnitOfWork>().To<PostalData2Context>().WhenTargetHas<InSpatialEntitesScope>().InRequestScope();


           // // DataContexts: When any ancestor in the inheritance chain has been labeled with any of these attributes.
           // Bind<DbContext>().ToMethod(c => c.Kernel.Get<AnewluvContext>()).InRequestScope(); ;

           // Bind<DbContext>().ToMethod(c => c.Kernel.Get<PostalData2Context>())
            // .WhenAnyAncestorMatches(Predicates.TargetHas<InSpatialEntitesScope>).InRequestScope(); ;


         
      }
	}
}