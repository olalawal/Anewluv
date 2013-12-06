


using System;
using System.Collections.Generic;
using System.Linq;


using Shell.MVC2.Interfaces;
//using Shell.MVC2.Services.Media;
//using Dating.Server.Data.Models;
//using Dating.Server.Data.Services;
//using Shell.MVC2.Services.Dating;
using Anewluv.Services.Contracts;

//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;
using GeoData.Domain.Context;
using GeoData.Domain.Models;

using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Activation;
using Ninject;
using Nmedia.DataAccess.Interfaces;
using System.Data.Entity;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class GeoModule : NinjectModule
	{
		public override void Load()
		{

            this.Bind<PostalData2Context>().ToSelf().InRequestScope();
            this.Bind<IUnitOfWork>().ToMethod(ctx => ctx.Kernel.Get<PostalData2Context>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Anewluv.Services.Spatial.GeoService"));
            // this.Unbind(typeof(DbContext));
            this.Bind<DbContext>().ToMethod(ctx => ctx.Kernel.Get<PostalData2Context>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Anewluv.Services.Spatial.GeoService"));

            //the Unit of work module should already be loaded by now
            this.Bind<IGeoService>().ToSelf().InRequestScope();
		}
	}
}