using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity;
using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Activation;
using Ninject;
using Nmedia.Infrastructure.Domain;
//using Nmedia.DataAccess.Interfaces;
using Nmedia.Services.Contracts;

using System.ServiceModel;
using Repository.Pattern.DataContext;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Ef6;

namespace Nmedia.Infrastructure.DependencyResolution.Ninject.Modules
{
   public  class ApiKeyServiceModule : NinjectModule
    {
        public override void Load()
        {

            // public ApikeyService(IUnitOfWorkAsync unitOfWork, IApiKeyStoredProcedures storedProcedures)
            // IKernel kernel = new StandardKernel();

          //  this.Bind<ApiKeyContext>().ToSelf().InScope(c => OperationContext.Current);

            
            //this.Bind<WellsFargo.DataAccess.Interfaces.IContext>().ToConstructor(x => new PromotionContext()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("WellsFargo.Promotion.Services.PromotionService")).InTransientScope ();
            //this.Bind<WellsFargo.DataAccess.Interfaces.IContext>().ToMethod(ctx => ctx.Kernel.Get<PromotionContext>());//).ToMethod()(x => new PromotionContext()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("WellsFargo.Promotion.Services.PromotionService")).InTransientScope();

            // var webApiEFRepository = kernel.Get<IRepository<Entity>>("WebApiEFRepository");
            //  this.Unbind(typeof(IUnitOfWorkAsync));
            //Kernel.Bind<IUnitOfWorkAsync>().ToConstructor(ctorArg => new EFUnitOfWork(ctorArg.Inject<WellsFargo.DataAccess.Interfaces.IContext>())).InTransientScope();
           // this.Bind<IUnitOfWorkAsync>().ToMethod(ctx => ctx.Kernel.Get<ApiKeyContext>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Nmedia.Services.Authorization.ApikeyService")).InScope(c => OperationContext.Current); ;
            // this.Unbind(typeof(DbContext));
           // this.Bind<DbContext>().ToMethod(ctx => ctx.Kernel.Get<ApiKeyContext>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Nmedia.Services.Authorization.ApikeyService")).InScope(c => OperationContext.Current); ;

            //the Unit of work module should already be loaded by now
           // this.Bind<IApikeyService>().ToSelf().InRequestScope();
            //Bind<IApikeyService, ApiKeyService>().To<ApiKeyService>().InRequestScope();
           // this.Bind<IApikeyService>().ToSelf().InRequestScope();

            Bind<IDataContextAsync>().To<ApiKeyContext>().InRequestScope();  //.WhenTargetHas<IAnewluvEntitesScope>().InRequestScope();
            Bind<IUnitOfWorkAsync>().To<UnitOfWork>().InRequestScope();  //.WhenTargetHas<InSpatialEntitesScope>().InRequestScope();
            Bind<IApiKeyStoredProcedures>().To<ApiKeyContext>().InRequestScope();

        }

    }
}
