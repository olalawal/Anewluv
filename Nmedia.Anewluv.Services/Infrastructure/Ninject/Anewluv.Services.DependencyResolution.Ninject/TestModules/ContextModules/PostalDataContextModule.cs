


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;

//to do do away with this when we go to code first , we would pull this from entities 
using GeoData.Domain.Context;
using GeoData.Domain.Models;
using Ninject.Web.Common;



using Ninject.Activation;
using Ninject;
using Repository.Pattern.DataContext;
//using Anewluv.Services.DependencyResolution.Ninject.predicates;
using Nmedia.Infrastructure.DependencyInjection;

namespace Anewluv.Services.DependencyResolution.Ninject.Modules
{
    public class PostalDataContextModule : NinjectModule
	{
		public override void Load()
		{

            Bind<IDataContextAsync>().ToMethod(c => c.Kernel.Get<PostalData2Context>())
          .WhenAnyAncestorMatches(Predicates.TargetHas<ISpatialEntitesScope>).InRequestScope();
            this.Bind<IGeoDataStoredProcedures>().To<PostalData2Context>().WhenTargetHas<ISpatialEntitesScope>().InRequestScope(); ;
         
		}
	}
}