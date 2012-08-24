


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;


namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class CustomErrorLogContextModule : NinjectModule
	{
		public override void Load()
		{
            //TO DO should be a separate service or something
            //bind the dbset context
            this.Bind<CustomErrorLogContext>().ToConstructor(x => new CustomErrorLogContext());
                      
		}
	}
}