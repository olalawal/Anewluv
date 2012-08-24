


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Data;
using Shell.MVC2.Interfaces;
using MediaService;
using Shell.MVC2.Services.Dating;

//to do do away with this when we go to code first , we would pull this from entities 
using Dating.Server.Data.Models ;

//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class MembersModule : NinjectModule
	{
		public override void Load()
		{
                
    
            //Bind repostiories 
            //Kernel.Bind<IPhotoRepository>().To<WesternGreetingRepository>();
            //We can bind the photo repo to a different repo as in above that implements the class
			Kernel.Bind<IPhotoRepository>().To<PhotoRepository>();
            Kernel.Bind<IGeoRepository>().To<GeoRepository>();
            Kernel.Bind<IMembersMapperRepository>().To<MembersMapperRepository>();

			//services
            Kernel.Bind<IMembersService>().ToSelf();
         


       
      }
	}
}