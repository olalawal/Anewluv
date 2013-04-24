


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Data;
using Shell.MVC2.Interfaces;
//using Shell.MVC2.Services.Media;
using Dating.Server.Data.Models;
using Dating.Server.Data.Services;
//using Shell.MVC2.Services.Dating;
using Shell.MVC2.Services.Contracts;

//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class GeoModule : NinjectModule
	{
		public override void Load()
		{
           // Kernel.Bind<IPhotoRepository>().To<WesternGreetingRepository>();
            //We can bind the photo repo to a different repo as in above that implements the class
            Kernel.Bind<IGeoRepository>().ToConstructor(ctorArg => new GeoRepository(ctorArg.Inject<PostalDataService >()));
			Kernel.Bind<IGeoService>().ToSelf();
		}
	}
}