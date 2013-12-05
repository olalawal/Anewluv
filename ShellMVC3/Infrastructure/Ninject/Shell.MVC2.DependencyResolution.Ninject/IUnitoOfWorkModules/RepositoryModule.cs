using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using Nmedia.DataAccess.Interfaces;
using Nmedia.DataAccess;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
           // Kernel.Bind(typeof(IRepository<>))
         //      .To(typeof(EFRepository<>)).InTransientScope();
            //TO DO for now binding all the repo's when we re-factor to user generics we only want to bind the active repo
            //bind  repositories as needed
            // Kernel.Bind<IUnitOfWork>().ToConstructor(ctorArg => new EFUnitOfWork (ctorArg.Inject<IContext>())).InTransientScope();
            //  Kernel.Bind<IPromotionRepository>().ToConstructor(ctorArg => new PromotionRepository(ctorArg.Inject<IContext>())).InSingletonScope();
            // Kernel.Bind<IReviewRepository>().ToConstructor(ctorArg => new ReviewRepository(ctorArg.Inject<IContext>())).InSingletonScope();
            // Kernel.Bind<IDeploymentRepository >().ToConstructor(ctorArg => new DeploymentRepository (ctorArg.Inject<IContext>())).InSingletonScope();
            //  Kernel.Bind<ISurfRepository >().ToConstructor(ctorArg => new SurfRepository (ctorArg.Inject<IContext>())).InSingletonScope();



        }
    }
}
