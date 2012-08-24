


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Data;
using Shell.MVC2.Infrastructure.Interfaces;
using Shell.MVC2.Data.Infrastructure;
//using Shell.MVC2.Services.Notification;
using Shell.MVC2.Infrastructure.Entities.NotificationModel;
using Shell.MVC2.Services.Contracts;




//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class ErrorNotificationModule : NinjectModule
	{
		public override void Load()
		{
         

            Kernel.Bind<IErrorNotificationRepository>().ToConstructor(
             ctorArg => new ErrorNotificationRepository(ctorArg.Inject<NotificationContext>()));

            Kernel.Bind<IErrorNotificationService>().ToSelf();


		}
	}
}