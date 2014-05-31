


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Domain.Entities.Anewluv.Chat;


namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class ChatContextModule : NinjectModule
	{
		public override void Load()
		{
            //TO DO should be a separate service or something
            //bind the dbset context
            this.Bind<ChatContext>().ToConstructor(x => new ChatContext());
                      
		}
	}
}