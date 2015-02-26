


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;

//to do do away with this when we go to code first , we would pull this from entities 
using GeoData.Domain.Context;
using GeoData.Domain.Models;
using Ninject.Web.Common;

namespace Anewluv.Services.DependencyResolution.Ninject.Modules
{
    public class PostalDataContextModule : NinjectModule
	{
		public override void Load()
		{

            this.Bind<PostalData2Context>().ToConstructor(x => new PostalData2Context()).InRequestScope();;
                      
		}
	}
}