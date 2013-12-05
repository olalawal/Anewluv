


using System;
using System.Collections.Generic;
using System.Linq;
using Shell.MVC2.Data;




using Shell.MVC2.Services.Contracts;


//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;

using Anewluv.Domain;
using Nmedia.DataAccess.Interfaces;
using System.Data.Entity;

using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Activation;
using Ninject;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class PhotoServiceModule : NinjectModule
	{
		public override void Load()
		{
          

            // IKernel kernel = new StandardKernel();

            this.Bind<AnewluvContext>().ToSelf().InRequestScope();
            //this.Bind<WellsFargo.DataAccess.Interfaces.IContext>().ToConstructor(x => new PromotionContext()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("WellsFargo.Promotion.Services.PromotionService")).InTransientScope ();
            //this.Bind<WellsFargo.DataAccess.Interfaces.IContext>().ToMethod(ctx => ctx.Kernel.Get<PromotionContext>());//).ToMethod()(x => new PromotionContext()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("WellsFargo.Promotion.Services.PromotionService")).InTransientScope();

            // var webApiEFRepository = kernel.Get<IRepository<Entity>>("WebApiEFRepository");
            //  this.Unbind(typeof(IUnitOfWork));
            //Kernel.Bind<IUnitOfWork>().ToConstructor(ctorArg => new EFUnitOfWork(ctorArg.Inject<WellsFargo.DataAccess.Interfaces.IContext>())).InTransientScope();
            this.Bind<IUnitOfWork>().ToMethod(ctx => ctx.Kernel.Get<AnewluvContext>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Anewluv.Services.Media.PhotoService"));
            // this.Unbind(typeof(DbContext));
            this.Bind<DbContext>().ToMethod(ctx => ctx.Kernel.Get<AnewluvContext>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Anewluv.Services.Media.PhotoService"));

            //the Unit of work module should already be loaded by now
            this.Bind<IPhotoService>().ToSelf().InRequestScope();


		}
	}
}