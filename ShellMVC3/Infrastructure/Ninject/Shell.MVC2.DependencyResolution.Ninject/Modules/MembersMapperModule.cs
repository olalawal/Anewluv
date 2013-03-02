


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Data;
using Shell.MVC2.Interfaces;
//using Shell.MVC2.Services.Media;
//using Shell.MVC2.Services.Dating;
using Dating.Server.Data.Services;

//to do do away with this when we go to code first , we would pull this from entities 
//using Dating.Server.Data.Models ;

//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;
using Shell.MVC2.Services.Contracts;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Infrastructure.Interfaces;
using Shell.MVC2.Infrastructure.Entities.ApiKeyModel;
using Dating.Server.Data.Models;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class MembersMapperModule : NinjectModule
	{
		public override void Load()
		{
                
      //TO DO put all the general always needed repos in the same module so they are guarnteed to only be called once as needed.
              //SInce this needs the authentication module we comment all this out   
            //TO DO should be a separate service or something

            // this.Bind<ApiKeyContext>().ToConstructor(x => new ApiKeyContext());
            Kernel.Bind<IAPIkeyRepository>().ToConstructor(ctorArg => new APIkeyRepository(ctorArg.Inject<ApiKeyContext>()));
            if (!Kernel.HasModule("Shell.MVC2.DependencyResolution.Ninject.Modules.MembersModule")) //only load if not already loaded into kernel
           // Kernel.Bind<IMemberRepository>().ToConstructor(ctorArg => new MemberRepository(ctorArg.Inject<AnewluvContext>()));
            //Kernel.Bind<IGeoRepository>().ToConstructor(ctorArg => new GeoRepository(ctorArg.Inject<PostalData2Entities>()));
          //  Kernel.Bind<IPhotoRepository>().ToConstructor(ctorArg => new PhotoRepository(ctorArg.Inject<AnewluvContext>(), ctorArg.Inject<IMemberRepository>()));
            Kernel.Bind<IMailRepository>().ToConstructor(ctorArg => new MailRepository(ctorArg.Inject<AnewluvContext>(), ctorArg.Inject<IMemberRepository>()));
           // Kernel.Bind<IMemberActionsRepository>().ToConstructor(ctorArg => new MemberActionsRepository(ctorArg.Inject<AnewluvContext>(), ctorArg.Inject<IMemberRepository>()));

            Kernel.Bind<IMembersMapperRepository>().ToConstructor(
             ctorArg => new MembersMapperRepository(
                 ctorArg.Inject<IGeoRepository>(),
                 ctorArg.Inject<IPhotoRepository>(),
                 ctorArg.Inject<IMemberRepository>(),                
                  ctorArg.Inject<IMailRepository>(),
                 ctorArg.Inject<AnewluvContext>()));

			//services
            Kernel.Bind<IMembersMapperService>().ToSelf();
         


       
      }
	}
}