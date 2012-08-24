


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Infrastructure.Entities.NotificationModel;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
    public class NotificationContextModule : NinjectModule
	{
		public override void Load()
		{
        
            this.Bind<NotificationContext>().ToConstructor(x => new NotificationContext());
                      
		}
	}
}