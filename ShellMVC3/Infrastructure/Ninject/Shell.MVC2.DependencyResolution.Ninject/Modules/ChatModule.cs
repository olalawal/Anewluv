


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

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class ChatModule : NinjectModule
	{
        const string SignalRBaseAddress = "http://localhost:8081/";
      

		public override void Load()
		{
            //stuff for SIGNAL R
            var resolver = new NinjectSignalRDependencyResolver(Kernel);
            var connectionManager = resolver.Resolve<IConnectionManager>();

            Kernel.Bind<IConnectionManager>()
               .ToConstant(connectionManager);

            Kernel.Bind<IAPIkeyRepository>().ToConstructor(ctorArg => new APIkeyRepository(ctorArg.Inject<ApiKeyContext>()));           
            Kernel.Bind<IChatRepository>().ToConstructor(
             ctorArg => new ChatRepository(ctorArg.Inject<ChatContext>(), ctorArg.Inject<AnewluvContext>()));
            Kernel.Bind<IMemberRepository>().ToConstructor(ctorArg => new MemberRepository(ctorArg.Inject<AnewluvContext>()));
            //services
            Kernel.Bind<IChatService>().ToSelf();
         
            //bind the SignalR stuff as well

           // Kernel.Bind<Microsoft.AspNet.SignalR.IDependencyResolver>();
          


           // Microsoft.AspNet.SignalR.IDependencyResolver(new SignalR.Ninject.NinjectDependencyResolver(kernel));

            //now all the services are regiseters reslove the kernal and access it
           // var resolver = new SignalR.Ninject.NinjectDependencyResolver(Kernel);


            //Other binding option
            //TO DO move chat hub to separate assembly so we do not have to import the chaervice into this
            Kernel.Bind<Chat>().ToConstructor(ctorArg => new Chat(ctorArg.Inject<IChatService>(),ctorArg.Inject<IChatRepository >())).InSingletonScope();

            //TO DO move chat hub to separate assembly so we do not have to import the chaervice into this
            // We're doing this manually since we want the chat repository to be shared
            // between the chat service and the chat hub itself
            //Kernel.Bind<Chat>()
            //      .ToMethod(context =>
            //      {
            //          var chatrepository = Kernel.<IChatRepository>();
            //          var memberrepository = context.Kernel.Get<IJabbrRepository>();
            //          var cache = context.Kernel.Get<ICache>();

            //          var service = new ChatService(cache, repository);

            //          return new Chat(resourceProcessor,
            //                          service,
            //                          repository,
            //                          cache);
            //      });


            
            //TO DO figure out how to start this 
            // Start the sweeper
         //   var repositoryFactory = new Func<IChatRepository>(() => Kernel.Resolve(IChatRepository));
         //   _timer = new Timer(_ => ChatInfrastructure.Sweep(repositoryFactory, resolver), null, _sweepInterval, _sweepInterval);
      }
	}
}