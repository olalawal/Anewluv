


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Data;
using Shell.MVC2.Infrastructure.Interfaces;


//to do do away with this when we go to code first , we would pull this from entities 
using Dating.Server.Data.Models ;
using Shell.MVC2.Data.AuthenticationAndMembership;
using Shell.MVC2.Services.Contracts;
using Shell.MVC2.Infrastructure.Entities.ApiKeyModel;

using Shell.MVC2.Domain.Entities.Anewluv;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
    public class APIKeyAuthorizationModule : NinjectModule
	{
		public override void Load()
		{

          //  this.Bind<ApiKeyContext>().ToConstructor(x => new ApiKeyContext());

           // Kernel.Bind<IAPIkeyRepository>().ToConstructor(ctorArg => new APIkeyRepository(ctorArg.Inject<ApiKeyContext>()));

           //  Kernel.Bind<APIKeyAuthorization>().ToConstructor(
          //   ctorArg => new APIKeyAuthorization(ctorArg.Inject<IAPIkeyRepository>()));

            //services
            //Kernel.Bind<IAuthenticationService>().ToSelf();
         
      }
	}
}