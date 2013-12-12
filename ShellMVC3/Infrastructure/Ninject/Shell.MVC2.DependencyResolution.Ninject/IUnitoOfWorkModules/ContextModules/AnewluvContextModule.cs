


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Anewluv.Domain;
using Ninject.Web.Common;
using System.ServiceModel;
using Ninject.Extensions.Wcf;


namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class AnewLuvContextModule : NinjectModule
	{
		public override void Load()
		{
            //TO DO should be a separate service or something
            //bind the dbset context
            this.Bind<AnewluvContext>().ToConstructor(x => new AnewluvContext()).InScope(c => OperationContext.Current); 
                      
		}
	}
}