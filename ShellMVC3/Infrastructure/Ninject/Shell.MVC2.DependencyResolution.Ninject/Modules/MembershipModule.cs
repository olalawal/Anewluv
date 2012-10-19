


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

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class MembershipModule : NinjectModule
	{
		public override void Load()
		{
                //AnewLuvFTSEntities datingcontext, IGeoRepository georepository, IMemberRepository memberepository
           
           Kernel.Bind<IAnewLuvMembershipProvider>().ToConstructor(
             ctorArg => new AnewLuvMembershipProvider(ctorArg.Inject<AnewLuvFTSEntities>(),ctorArg.Inject<IGeoRepository>(),ctorArg.Inject<IMemberRepository>()));

            //services
           Kernel.Bind<IAuthenticationService>().ToSelf();
         
      }
	}
}