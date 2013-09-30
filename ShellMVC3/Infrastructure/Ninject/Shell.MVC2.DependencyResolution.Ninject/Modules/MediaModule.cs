


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Data;
using Shell.MVC2.Interfaces;
//using Shell.MVC2.Services.Media;
using Dating.Server.Data.Models;
using Dating.Server.Data.Services;
using Shell.MVC2.Services.Contracts;
using Shell.MVC2.Domain.Entities.Anewluv;

//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;
using Ninject.Web.Common;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class MediaModule : NinjectModule
	{
		public override void Load()
		{
           // Kernel.Bind<IPhotoRepository>().To<WesternGreetingRepository>();
            //We can bind the photo repo to a different repo as in above that implements the class

            Kernel.Bind<IMemberRepository>().ToConstructor(ctorArg => new MemberRepository(ctorArg.Inject<AnewluvContext>())).InRequestScope();

			Kernel.Bind<IPhotoRepository>().ToConstructor(
            ctorArg => new PhotoRepository(ctorArg.Inject<AnewluvContext>(), ctorArg.Inject<IMemberRepository>())).InRequestScope();

			Kernel.Bind<IPhotoService>().ToSelf().InRequestScope();
		}
	}
}