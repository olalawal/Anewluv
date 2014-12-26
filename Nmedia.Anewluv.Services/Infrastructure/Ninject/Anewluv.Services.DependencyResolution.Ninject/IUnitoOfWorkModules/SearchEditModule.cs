


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;

//using Shell.MVC2.Services.Authentication; 
//to do do away with this when we go to code first , we would pull this from entities 
using Anewluv.Services.Contracts;

//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;
using Ninject.Web.Common;
using Ninject.Activation;
using Ninject;
using Anewluv.Domain;
using Nmedia.DataAccess.Interfaces;
using System.Data.Entity;
using System.ServiceModel;
//using Anewluv.Services.Edit;


namespace Anewluv.Services.DependencyResolution.Ninject.Modules
{
    public class SearchEditModule : NinjectModule
	{
        //TO DO fix this shit
		public override void Load()
		{

            // IKernel kernel = new StandardKernel();

            this.Bind<IUnitOfWork>().ToMethod(ctx => ctx.Kernel.Get<AnewluvContext>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Anewluv.Services.Edit.SearchEditService")).InScope(c => OperationContext.Current); 
            // this.Unbind(typeof(DbContext));
            this.Bind<DbContext>().ToMethod(ctx => ctx.Kernel.Get<AnewluvContext>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Anewluv.Services.Edit.SearchEditService")).InScope(c => OperationContext.Current); 

            //the Unit of work module should already be loaded by now
           // this.Bind<ISearchEditService>().ToSelf().InScope(c => OperationContext.Current);
            //Bind<ISearchEditService,SearchEditService>().To<SearchEditService>().InRequestScope();

            //the Unit of work module should already be loaded by now
            this.Bind<ISearchEditService>().ToSelf().InRequestScope();
         
      }
	}
}