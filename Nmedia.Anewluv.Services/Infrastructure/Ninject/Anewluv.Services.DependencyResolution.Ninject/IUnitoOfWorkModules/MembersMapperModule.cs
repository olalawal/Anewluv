


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;

//using Shell.MVC2.Services.Media;
//using Shell.MVC2.Services.Dating;


//to do do away with this when we go to code first , we would pull this from entities 
//using Dating.Server.Data.Models ;

//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;
using Anewluv.Services.Contracts;



using Anewluv.Domain;


using Ninject.Web.Common;
using Ninject.Activation;
using Ninject;
using Nmedia.DataAccess.Interfaces;
using System.Data.Entity;
using GeoData.Domain.Models;
using Nmedia.Infrastructure.DependencyInjection;
using Anewluv.Services.DependencyResolution.Ninject.predicates;

namespace Anewluv.Services.DependencyResolution.Ninject.Modules
{
	public class MembersMapperModule : NinjectModule
	{
		public override void Load()
		{




        // IKernel kernel = new StandardKernel();

        // this.Bind<AnewluvContext>().ToSelf().InRequestScope();
        //this.Bind<WellsFargo.DataAccess.Interfaces.IContext>().ToConstructor(x => new PromotionContext()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("WellsFargo.Promotion.Services.PromotionService")).InTransientScope ();
        //this.Bind<WellsFargo.DataAccess.Interfaces.IContext>().ToMethod(ctx => ctx.Kernel.Get<PromotionContext>());//).ToMethod()(x => new PromotionContext()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("WellsFargo.Promotion.Services.PromotionService")).InTransientScope();

        // var webApiEFRepository = kernel.Get<IRepository<Entity>>("WebApiEFRepository");
        //  this.Unbind(typeof(IUnitOfWork));
        //Kernel.Bind<IUnitOfWork>().ToConstructor(ctorArg => new EFUnitOfWork(ctorArg.Inject<WellsFargo.DataAccess.Interfaces.IContext>())).InTransientScope();
       // this.Bind<IUnitOfWork>().ToMethod(ctx => ctx.Kernel.Get<AnewluvContext>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Anewluv.Services.Mapping.MembersMapperService")).InRequestScope(); ;
        // this.Unbind(typeof(DbContext));
      //  this.Bind<DbContext>().ToMethod(ctx => ctx.Kernel.Get<AnewluvContext>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Anewluv.Services.Mapping.MembersMapperService")).InRequestScope(); ;



            //probbaly can get away with this since the mapping service does not do any writes 
            //so so chance of conufsing anewluv contexts with the members sevice which is hosted in the same service.


               // Unit of Work & Generic Repository Patterns.

            Bind<IUnitOfWork>().To<AnewluvContext>().WhenTargetHas<IAnewluvEntitesScope>().InRequestScope();
            Bind<IUnitOfWork>().To<PostalData2Context>().WhenTargetHas<InSpatialEntitesScope>().InRequestScope();
       // Bind<IUnitOfWork>().To<UnitOfWork>().WhenTargetHas<InVariosEntitiesScope>().InSingletonScope();

        // DataContexts: When any ancestor in the inheritance chain has been labeled with any of these attributes.
            Bind<DbContext>().ToMethod(c => c.Kernel.Get<AnewluvContext>())
            .WhenAnyAncestorMatches(Predicates.TargetHas<IAnewluvEntitesScope>);

            Bind<DbContext>().ToMethod(c => c.Kernel.Get<PostalData2Context>())
            .WhenAnyAncestorMatches(Predicates.TargetHas<InSpatialEntitesScope>);

       // Bind<IDataContext>().To<VariosEntities>()
       //     .WhenAnyAncestorMatches(Predicates.TargetHas<InVariosEntitiesScope>).InSingletonScope();
    



         
        //this.Bind<IMembersMapperService>().ToSelf().InRequestScope();

         
        }

       
      }
	
}