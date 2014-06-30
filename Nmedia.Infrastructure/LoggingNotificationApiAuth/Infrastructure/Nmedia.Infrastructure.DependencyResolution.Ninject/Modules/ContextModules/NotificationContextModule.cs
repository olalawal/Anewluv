using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Web.Common;
using Nmedia.Infrastructure.Domain;
using Ninject.Modules;

namespace Nmedia.Infrastructure.DependencyResolution.Ninject.Modules.ContextModules
{
    public class NotificationContextModule : NinjectModule
    {
        public override void Load()
        {
            //TO DO should be a separate service or something
            //bind the dbset context
            //  this.Bind<IContext>().ToConstructor(x => new PromotionContext()).InTransientScope().Named("PromotionContext");
            this.Bind<NotificationContext>().ToConstructor(x => new NotificationContext()).InRequestScope();
        }

    }
}
