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
using Nmedia.DataAccess.Interfaces;
using Nmedia.Services.Contracts;

namespace Nmedia.Infrastructure.DependencyResolution.Ninject.Modules
{
    public class logServiceModule : NinjectModule
    {

        public override void Load()
        {
            // IKernel kernel = new StandardKernel();

            this.Bind<LoggingContext>().ToSelf().InRequestScope();
            //this.Bind<WellsFargo.DataAccess.Interfaces.IContext>().ToConstructor(x => new PromotionContext()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("WellsFargo.Promotion.Services.PromotionService")).InTransientScope ();
            //this.Bind<WellsFargo.DataAccess.Interfaces.IContext>().ToMethod(ctx => ctx.Kernel.Get<PromotionContext>());//).ToMethod()(x => new PromotionContext()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("WellsFargo.Promotion.Services.PromotionService")).InTransientScope();

            // var webApiEFRepository = kernel.Get<IRepository<Entity>>("WebApiEFRepository");
            //  this.Unbind(typeof(IUnitOfWork));
            //Kernel.Bind<IUnitOfWork>().ToConstructor(ctorArg => new EFUnitOfWork(ctorArg.Inject<WellsFargo.DataAccess.Interfaces.IContext>())).InTransientScope();
            this.Bind<IUnitOfWork>().ToMethod(ctx => ctx.Kernel.Get<LoggingContext>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Nmedia.Services.Logging.loggingService"));
            // this.Unbind(typeof(DbContext));
            this.Bind<DbContext>().ToMethod(ctx => ctx.Kernel.Get<LoggingContext>()).When(t => t.IsInjectingToRepositoryDataSourceOfNamespace("Nmedia.Services.Logging.loggingService"));

            //the Unit of work module should already be loaded by now
            this.Bind<IAnewluvLoggingService>().ToSelf().InRequestScope();

        }
    }
}
