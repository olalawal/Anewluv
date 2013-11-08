


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Nmedia.Infrastructure.Domain.Errorlog;


namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class ErrorlogContextModule : NinjectModule
	{
		public override void Load()
		{
            //TO DO should be a separate service or something
            //bind the dbset context
            this.Bind<ErrorlogContext>().ToConstructor(x => new ErrorlogContext());
                      
		}
	}
}