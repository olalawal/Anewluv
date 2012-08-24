


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Data;
using Shell.MVC2.Interfaces;
using MediaService;

//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class MediaModule : NinjectModule
	{
		public override void Load()
		{
           // Kernel.Bind<IPhotoRepository>().To<WesternGreetingRepository>();
            //We can bind the photo repo to a different repo as in above that implements the class
			Kernel.Bind<IPhotoRepository>().To<PhotoRepository>();
			Kernel.Bind<IPhotoService>().ToSelf();
		}
	}
}