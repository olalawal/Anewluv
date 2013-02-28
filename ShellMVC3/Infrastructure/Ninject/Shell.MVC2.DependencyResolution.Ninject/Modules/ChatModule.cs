


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
using Shell.MVC2.Infrastructure.Interfaces;
using Shell.MVC2.Infrastructure.Entities.ApiKeyModel;
using Shell.MVC2.Domain.Entities.Anewluv.Chat;

using Shell.MVC2.Services.Contracts;
using Owin;
using Shell.MVC2.Services.Chat;
using Shell.MVC2.DependencyResolution.Ninject.Infrastructure;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.AspNet.SignalR;
using Shell.MVC2.SignalR.Hubs;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class ChatModule : NinjectModule
	{
        const string SignalRBaseAddress = "http://localhost:8081/";
      

		public override void Load()
		{
            

            Kernel.Bind<IAPIkeyRepository>().ToConstructor(ctorArg => new APIkeyRepository(ctorArg.Inject<ApiKeyContext>()));           
            Kernel.Bind<IChatRepository>().ToConstructor(
             ctorArg => new ChatRepository(ctorArg.Inject<ChatContext>(), ctorArg.Inject<AnewluvContext>()));
            Kernel.Bind<IMemberRepository>().ToConstructor(ctorArg => new MemberRepository(ctorArg.Inject<AnewluvContext>()));
            Kernel.Bind<IChatService>().ToConstructor(ctorArg => new ChatService(ctorArg.Inject<IChatRepository>(), ctorArg.Inject<IMemberRepository>()));

            //services
            //this allows the service to bind for WCF
            // Kernel.Bind<IChatService>().ToSelf();
         
            //Other binding option
            //TO DO move chat hub to separate assembly so we do not have to import the chaervice into this
           // Kernel.Bind<ChatHub>().ToConstructor(ctorArg => new ChatHub(ctorArg.Inject<IChatService>(),ctorArg.Inject<IChatRepository >())).InSingletonScope();

           

            
      }
	}
}