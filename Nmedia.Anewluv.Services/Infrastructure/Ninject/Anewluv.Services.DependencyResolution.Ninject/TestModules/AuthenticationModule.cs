


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;

//using Shell.MVC2.Services.Authentication; 
//to do do away with this when we go to code first , we would pull this from entities 
//using Anewluv.Services.Contracts;

//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;
using Ninject.Web.Common;
using Ninject.Activation;
using Ninject;
using Anewluv.Domain;
//using Nmedia.DataAccess.Interfaces;
using System.Data.Entity;
using System.ServiceModel;
using Repository.Pattern.DataContext;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Ef6;
using GeoData.Domain.Models;



namespace Anewluv.Services.DependencyResolution.Ninject.Modules
{
    public class AuthenticationModule : NinjectModule
	{
		public override void Load()
		{
             //public AuthenticationService(IUnitOfWorkAsync unitOfWork, IGeoDataStoredProcedures storedProcedures)

            // IKernel kernel = new StandardKernel();
           // this.Bind<AnewluvContext>().ToSelf().InRequestScope();

            //this.Bind<IUnitOfWorkAsync>().ToMethod(ctx => ctx.Kernel.Get<AnewluvContext>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Anewluv.Services.Authentication.AuthenticationService")).InScope(c => OperationContext.Current); ;
           // this.Bind<DbContext>().ToMethod(ctx => ctx.Kernel.Get<AnewluvContext>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Anewluv.Services.Authentication.AuthenticationService")).InScope(c => OperationContext.Current); ;
   

           // this.Bind<IAuthenticationService>().ToSelf().InRequestScope();
            Bind<IDataContextAsync>().To<AnewluvContext>().InRequestScope(); ;  //.WhenTargetHas<IAnewluvEntitesScope>().InRequestScope();
            Bind<IUnitOfWorkAsync>().To<UnitOfWork>().InRequestScope(); ;  //.WhenTargetHas<InSpatialEntitesScope>().InRequestScope();
            Bind<IGeoDataStoredProcedures>().To<PostalData2Context>().InRequestScope(); ;



         
      }
	}
}