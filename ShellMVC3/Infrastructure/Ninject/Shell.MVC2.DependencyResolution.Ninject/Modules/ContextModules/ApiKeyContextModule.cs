


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Infrastructure.Entities.ApiKeyModel ;


namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class ApiKeyContextModule : NinjectModule
	{
		public override void Load()
		{
            //TO DO should be a separate service or something
            //bind the dbset context
            this.Bind<ApiKeyContext>().ToConstructor(x => new ApiKeyContext());
                      
		}
	}
}