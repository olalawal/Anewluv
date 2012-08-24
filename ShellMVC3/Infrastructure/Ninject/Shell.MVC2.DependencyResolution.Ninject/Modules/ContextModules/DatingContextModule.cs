


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Data;
using Shell.MVC2.Interfaces;
//to do do away with this when we go to code first , we would pull this from entities 
using Dating.Server.Data.Models;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class DatingContextModule : NinjectModule
	{
		public override void Load()
		{
            this.Bind<AnewLuvFTSEntities>().ToConstructor(x => new AnewLuvFTSEntities());
       
                      
		}
	}
}