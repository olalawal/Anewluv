


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Data;
using Shell.MVC2.Interfaces;
//using Shell.MVC2.Services.Media;
//using Shell.MVC2.Services.Dating;
//using Dating.Server.Data.Services;
using Anewluv.Services.Contracts;

//to do do away with this when we go to code first , we would pull this from entities 
//using Dating.Server.Data.Models ;
using Anewluv.Domain;

//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;



namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class MemberEditModule : NinjectModule
	{
		public override void Load()
		{

           // Kernel.Bind<IAPIkeyRepository>().ToConstructor(ctorArg => new APIkeyRepository(ctorArg.Inject<ApiKeyContext>()));
            Kernel.Bind<IMemberEditRepository>().ToConstructor(
             ctorArg => new MemberEditRepository(ctorArg.Inject<AnewluvContext>()));
            
			//services
            Kernel.Bind<IMemberEditService>().ToSelf();
         
      }
	}
}