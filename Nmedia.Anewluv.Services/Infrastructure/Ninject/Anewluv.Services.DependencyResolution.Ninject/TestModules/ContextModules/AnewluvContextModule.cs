


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Anewluv.Domain;
using Ninject.Web.Common;
using System.ServiceModel;
using Ninject.Extensions.Wcf;
using Repository.Pattern.DataContext;





using Ninject.Activation;
using Ninject;
//using Anewluv.Services.DependencyResolution.Ninject.predicates;
using Nmedia.Infrastructure.DependencyInjection;

namespace Anewluv.Services.DependencyResolution.Ninject.Modules
{
	public class AnewLuvContextModule : NinjectModule
	{
		public override void Load()
		{
            //TO DO should be a separate service or something
            //bind the dbset context

            Bind<IDataContextAsync>().ToMethod(c => c.Kernel.Get<AnewluvContext>())
            .WhenAnyAncestorMatches(Predicates.TargetHas<IAnewluvEntitesScope>).InRequestScope();
             
		}
	}
}