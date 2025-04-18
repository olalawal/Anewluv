﻿


using System;
using System.Collections.Generic;
using System.Linq;


using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Activation;
using Ninject;
using Nmedia.DataAccess.Interfaces;


//to do do away with this when we go to code first , we would pull this from entities 

using Anewluv.Services.Contracts;
using Anewluv.Domain;
using System.Data.Entity;
using System.ServiceModel;
//using Anewluv.Services.Members;



//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;

namespace Anewluv.Services.DependencyResolution.Ninject.Modules
{
	public class LookupModule : NinjectModule
	{
		public override void Load()
		{

            // IKernel kernel = new StandardKernel();
           // this.Bind<AnewluvContext>().ToSelf().InRequestScope();

            this.Bind<IUnitOfWork>().ToMethod(ctx => ctx.Kernel.Get<AnewluvContext>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Anewluv.Services.Common.LookupService")).InScope(c => OperationContext.Current); ;
            // this.Unbind(typeof(DbContext));
            this.Bind<DbContext>().ToMethod(ctx => ctx.Kernel.Get<AnewluvContext>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Anewluv.Services.Common.LookupService")).InScope(c => OperationContext.Current); 
                ;

            Bind<ILookupService>().ToSelf().InRequestScope();


         
      }
	}
}