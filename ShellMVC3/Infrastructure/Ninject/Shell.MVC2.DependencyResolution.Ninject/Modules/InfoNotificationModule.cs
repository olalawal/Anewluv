


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Data;
using Shell.MVC2.Infrastructure.Interfaces;
//using Shell.MVC2.Services.Notification;
using Shell.MVC2.Infrastructure.Entities.NotificationModel;
using Shell.MVC2.Services.Contracts;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Interfaces;



 

//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
    public class InfoNotificationModule : NinjectModule
    {
        public override void Load()
        {
                    

            Kernel.Bind<IInfoNotificationRepository>().ToConstructor(
             ctorArg => new InfoNotificationRepository(ctorArg.Inject<NotificationContext>(),
                 ctorArg.Inject<AnewluvContext>(),
                 ctorArg.Inject<IMemberRepository>(), ctorArg.Inject<IMembersMapperRepository>() 
                 ));
          
            Kernel.Bind<IInfoNotificationService>().ToSelf();


        }
    }
}