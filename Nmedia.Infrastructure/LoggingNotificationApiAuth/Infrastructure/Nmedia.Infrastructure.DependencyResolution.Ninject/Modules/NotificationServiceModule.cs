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
using Repository.Pattern.DataContext;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Ef6;
using Nmedia.Infrastructure.DependencyInjection;
using Anewluv.Domain;

namespace Nmedia.Infrastructure.DependencyResolution.Ninject.Modules
{
   public  class NotificationServiceModule : NinjectModule
    {
        public override void Load()
        {
        
            
            //old
            //Bind<IDataContextAsync>().To<NotificationContext>().InRequestScope(); ;  //.WhenTargetHas<IAnewluvEntitesScope>().InRequestScope();
           // Bind<IUnitOfWorkAsync>().To<UnitOfWork>().InRequestScope(); ;  //.WhenTargetHas<InSpatialEntitesScope>().InRequestScope();

             Bind<IDataContextAsync>().ToMethod(c => c.Kernel.Get<AnewluvContext>())
            .WhenAnyAncestorMatches(Predicates.TargetHas<IAnewluvEntitesScope>).InRequestScope();
             Bind<IDataContextAsync>().ToMethod(c => c.Kernel.Get<NotificationContext>())
            .WhenAnyAncestorMatches(Predicates.TargetHas<INotificationEntitiesScope>).InRequestScope();

           // this.Bind<IDataContextAsync>().To<AnewluvContext>().WhenTargetHas<IAnewluvEntitesScope>().InRequestScope();
           // this.Bind<IDataContextAsync>().To<PostalData2Context>().WhenTargetHas<InSpatialEntitesScope>().InRequestScope();
            this.Bind<IUnitOfWorkAsync>().To<UnitOfWork>().WhenTargetHas<IAnewluvEntitesScope>().InRequestScope();
            this.Bind<IUnitOfWorkAsync>().To<UnitOfWork>().WhenTargetHas<ISpatialEntitesScope>().InRequestScope();;


        }

    }
}
