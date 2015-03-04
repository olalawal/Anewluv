


using System;
using System.Collections.Generic;
using System.Linq;





//using Anewluv.Services.Contracts;


//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;

using Anewluv.Domain;
//using Nmedia.DataAccess.Interfaces;
using System.Data.Entity;

using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Activation;
using Ninject;
using Repository.Pattern.DataContext;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Ef6;

namespace Anewluv.Services.DependencyResolution.Ninject.Modules
{
	public class PhotoServiceModule : NinjectModule
	{
		public override void Load()
		{
          

           

            this.Bind<IDataContextAsync>().To<AnewluvContext>().InRequestScope();
            this.Bind<IUnitOfWorkAsync>().To<UnitOfWork>().InRequestScope();


		}
	}
}