


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Data;
using Shell.MVC2.Interfaces;
//using Shell.MVC2.Services.Media;
//using Shell.MVC2.Services.Dating;
using Dating.Server.Data.Services;

//to do do away with this when we go to code first , we would pull this from entities 
using Dating.Server.Data.Models ;

//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;
using Shell.MVC2.Services.Contracts;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class MembersMapperModule : NinjectModule
	{
		public override void Load()
		{
                
    
                 
            //TO DO should be a separate service or something
           

            Kernel.Bind<IMembersMapperRepository>().ToConstructor(
             ctorArg => new MembersMapperRepository(ctorArg.Inject<IGeoRepository>(),ctorArg.Inject<IPhotoRepository>(),ctorArg.Inject<DatingService>()));

			//services
            Kernel.Bind<IMembersMapperService>().ToSelf();
         


       
      }
	}
}