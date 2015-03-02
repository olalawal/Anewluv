


using System;
using System.Collections.Generic;
using System.Linq;


//using Shell.MVC2.Services.Media;
//using Dating.Server.Data.Models;
//using Dating.Server.Data.Services;
//using Shell.MVC2.Services.Dating;
//using Anewluv.Services.Contracts;

//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;
using GeoData.Domain.Context;
using GeoData.Domain.Models;

using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Activation;
using Ninject;
//using Nmedia.DataAccess.Interfaces;
using System.Data.Entity;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.UnitOfWork;

namespace Anewluv.Services.DependencyResolution.Ninject.Modules
{
	public class GeoModule : NinjectModule
	{
		public override void Load()
		{
            //  public GeoService(IUnitOfWorkAsync unitOfWork, IGeoDataStoredProcedures storedProcedures)

            this.Bind<IDataContextAsync>().To<PostalData2Context>().InRequestScope();
            this.Bind<IUnitOfWorkAsync>().To<UnitOfWork>().InRequestScope();
            this.Bind<IGeoDataStoredProcedures>().To<PostalData2Context>().InRequestScope();
           // this.Bind<IUnitOfWorkAsync<AnewluvContext>>().To<UnitOfWork<AnewluvContext>>();

           // this.Bind<PostalData2Context>().ToSelf().InRequestScope();
           // this.Bind<IUnitOfWorkAsync>().ToMethod(ctx => ctx.Kernel.Get<PostalData2Context>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Anewluv.Services.Spatial.GeoService"));
            // this.Unbind(typeof(DbContext));
           // this.Bind<DbContext>().ToMethod(ctx => ctx.Kernel.Get<PostalData2Context>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Anewluv.Services.Spatial.GeoService"));

            //the Unit of work module should already be loaded by now
           // this.Bind<IGeoService>().ToSelf().InRequestScope();
		}
	}
}