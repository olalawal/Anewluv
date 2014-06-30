using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nmedia.Infrastructure.Domain;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Nmedia.Infrastructure.DependencyResolution.Ninject.Modules
{
   public class ErrorlogContextModule : NinjectModule
    {
        public override void Load()
        {
            //TO DO should be a separate service or something
            //bind the dbset context
            //  this.Bind<IContext>().ToConstructor(x => new PromotionContext()).InTransientScope().Named("PromotionContext");
            this.Bind<ErrorlogContext>().ToConstructor(x => new ErrorlogContext()).InRequestScope();
        }

    }
}
