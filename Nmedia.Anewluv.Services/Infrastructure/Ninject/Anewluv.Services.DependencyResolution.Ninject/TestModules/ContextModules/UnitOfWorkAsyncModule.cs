


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Anewluv.Domain.Chat;
using Ninject.Web.Common;

using Ninject.Activation;
using Ninject;
using Repository.Pattern.DataContext;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Ef6;


namespace Anewluv.Services.DependencyResolution.Ninject.Modules
{
	public class UnitOfWorkAsyncModule : NinjectModule
	{
		public override void Load()
		{
            this.Bind<IUnitOfWorkAsync>().To<UnitOfWork>().InRequestScope();
                      
		}
	}
}