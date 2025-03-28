﻿


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Data;
using Shell.MVC2.Interfaces;
//using Shell.MVC2.Services.Media;
//using Shell.MVC2.Services.Dating;
using Dating.Server.Data.Services;
using Shell.MVC2.Services.Contracts;

//to do do away with this when we go to code first , we would pull this from entities 
using Dating.Server.Data.Models ;

//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;
using Shell.MVC2.Domain.Entities.Anewluv;
using Ninject.Web.Common;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class MemberActionsModule : NinjectModule
	{
		public override void Load()
		{
            //we assume IMEMBER repo is already loaded from the Membership modul;e
           //  if (!Kernel.HasModule("Shell.MVC2.DependencyResolution.Ninject.Modules.MembersModule")) //only load if not already loaded into kernel
           //     Kernel.Bind<IMemberRepository>().ToConstructor(ctorArg => new MemberRepository(ctorArg.Inject<AnewluvContext>()));
            Kernel.Bind<IMemberActionsRepository>().ToConstructor(
             ctorArg => new MemberActionsRepository(ctorArg.Inject<AnewluvContext>(),
                 ctorArg.Inject<IMemberRepository>(), ctorArg.Inject<IMembersMapperRepository>())).InRequestScope();


			//services
            Kernel.Bind<IMemberActionsService>().ToSelf().InRequestScope();
         
      }
	}
}