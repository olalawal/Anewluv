


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



namespace Anewluv.Services.DependencyResolution.Ninject.Modules
{
	public class CommonModule : NinjectModule
	{
		public override void Load()
		{

            // IKernel kernel = new StandardKernel();
           // this.Bind<AnewluvContext>().ToSelf().InRequestScope();

            this.Bind<IUnitOfWork>().ToMethod(ctx => ctx.Kernel.Get<AnewluvContext>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Anewluv.Services.Common.CommonService")).InScope(c => OperationContext.Current); ;
            // this.Unbind(typeof(DbContext));
            this.Bind<DbContext>().ToMethod(ctx => ctx.Kernel.Get<AnewluvContext>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Anewluv.Services.Common.CommonService")).InScope(c => OperationContext.Current); 
                ;

            //the Unit of work module should already be loaded by now
           // this.Bind<IMemberService>().ToSelf();
            Bind<ICommonService>().To<ICommonService>().InRequestScope();


         
      }
	}
}