


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Anewluv.Domain.Chat;
using Ninject.Web.Common;


namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class ChatContextModule : NinjectModule
	{
		public override void Load()
		{
            //TO DO should be a separate service or something
            //bind the dbset context
            this.Bind<ChatContext>().ToConstructor(x => new ChatContext()).InRequestScope();
                      
		}
	}
}