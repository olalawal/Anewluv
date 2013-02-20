


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
using Microsoft.AspNet.SignalR.Hosting ;
using Shell.MVC2.Services.Contracts;
namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
	public class ChatModuleModule : NinjectModule
	{
        const string SignalRBaseAddress = "http://localhost:8081/";
        static Owin.IAppBuilder _SignalRServer;

		public override void Load()
		{

          
           
         
            //bind the SignalR stuff as well

            Kernel.Bind<Microsoft.AspNet.SignalR.IDependencyResolver>();
          
           // Microsoft.AspNet.SignalR.IDependencyResolver(new SignalR.Ninject.NinjectDependencyResolver(kernel));

            //now all the services are regiseters reslove the kernal and access it
            var resolver = new SignalR.Ninject.NinjectDependencyResolver(Kernel);

            //TO DO figure out how to start this 
            // Start the sweeper
         //   var repositoryFactory = new Func<IChatRepository>(() => Kernel.Resolve(IChatRepository));
         //   _timer = new Timer(_ => ChatInfrastructure.Sweep(repositoryFactory, resolver), null, _sweepInterval, _sweepInterval);
      }
	}
}