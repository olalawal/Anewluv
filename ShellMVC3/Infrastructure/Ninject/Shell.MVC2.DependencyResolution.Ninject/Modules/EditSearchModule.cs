


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Data;
using Shell.MVC2.Interfaces;
//using Shell.MVC2.Services.Media;
//using Shell.MVC2.Services.Dating;
using Dating.Server.Data.Services;
using Shell.MVC2.Services.Contracts;

//to do do away with this when we go to code first , we would pull this from entities 
using Dating.Server.Data.Models ;

//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;
using Shell.MVC2.Domain.Entities.Anewluv;
namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class EditSearchModule : NinjectModule
	{
		public override void Load()
		{
                
           
            Kernel.Bind<IEditSearchRepository >().ToConstructor(
            ctorArg => new EditSearchRepository (ctorArg.Inject<AnewluvContext>()));
            
			//services
            Kernel.Bind<IEditSearchService>().ToSelf();
         
      }
	}
}