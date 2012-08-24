


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Data;
using Shell.MVC2.Interfaces;
//using Shell.MVC2.Services.Media;
//to do do away with this when we go to code first , we would pull this from entities 
using Dating.Server.Data.Services ;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{

    /// <summary>
    /// maybe Should be depereiceated into repository pattern ( at least dating service ?)
    /// </summary>
    public class DatingServiceModule : NinjectModule
	{
		public override void Load()
		{
            this.Bind<DatingService>().ToSelf();
          
            
		}
	}
}