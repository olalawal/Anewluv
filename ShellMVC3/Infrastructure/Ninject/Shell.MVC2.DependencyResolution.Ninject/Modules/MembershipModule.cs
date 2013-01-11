


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Data;
using Shell.MVC2.Interfaces;
//using Shell.MVC2.Services.Authentication; 
using Dating.Server.Data.Services;

//to do do away with this when we go to code first , we would pull this from entities 
using Dating.Server.Data.Models ;
using Shell.MVC2.Data.AuthenticationAndMembership;
using Shell.MVC2.Services.Contracts;

//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Infrastructure.Interfaces;
using Shell.MVC2.Infrastructure.Entities.ApiKeyModel;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class MembershipModule : NinjectModule
	{
		public override void Load()
		{
             //AnewLuvFTSEntities datingcontext, IGeoRepository georepository, IMemberRepository memberepository
            Kernel.Bind<IAPIkeyRepository>().ToConstructor(ctorArg => new APIkeyRepository(ctorArg.Inject<ApiKeyContext>()));
            Kernel.Bind<IGeoRepository>().ToConstructor(ctorArg => new GeoRepository(ctorArg.Inject<PostalData2Entities>()));
            Kernel.Bind<IMemberRepository>().ToConstructor(ctorArg => new MemberRepository(ctorArg.Inject<AnewluvContext>()));
            Kernel.Bind<IPhotoRepository>().ToConstructor(ctorArg => new PhotoRepository(ctorArg.Inject<AnewluvContext>(), ctorArg.Inject<IMemberRepository>()));
            Kernel.Bind<IAnewLuvMembershipProvider>().ToConstructor(
            ctorArg => new AnewLuvMembershipProvider(ctorArg.Inject<AnewluvContext>(), ctorArg.Inject<IGeoRepository>(), ctorArg.Inject<IMemberRepository>(), ctorArg.Inject<IPhotoRepository>()));

            //services
           Kernel.Bind<IAuthenticationService>().ToSelf();
         
      }
	}
}