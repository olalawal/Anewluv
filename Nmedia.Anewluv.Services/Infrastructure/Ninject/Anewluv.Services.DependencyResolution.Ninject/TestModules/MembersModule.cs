


using System;
using System.Collections.Generic;
using System.Linq;


using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Activation;
using Ninject;
//using Nmedia.DataAccess.Interfaces;


//to do do away with this when we go to code first , we would pull this from entities 

//using Anewluv.Services.Contracts;
using Anewluv.Domain;
using System.Data.Entity;
using System.ServiceModel;
using Repository.Pattern.DataContext;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Ef6;


//using Anewluv.Services.Members;


//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;

namespace Anewluv.Services.DependencyResolution.Ninject.Modules
{
	public class MembersModule : NinjectModule
	{
		public override void Load()
		{


            this.Bind<IDataContextAsync>().To<AnewluvContext>().InRequestScope();
            this.Bind<IUnitOfWorkAsync>().To<UnitOfWork>().InRequestScope();
         
      }
	}
}