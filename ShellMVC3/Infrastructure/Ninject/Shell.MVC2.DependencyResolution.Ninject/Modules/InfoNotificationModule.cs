


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
using Dating.Server.Data.Models;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;
using Shell.MVC2.Infrastructure.Entities.ApiKeyModel ;



 

//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
    public class InfoNotificationModule : NinjectModule
    {
        public override void Load()
        {

            
           // Kernel.Bind<IAPIkeyRepository>().ToConstructor(ctorArg => new APIkeyRepository(ctorArg.Inject<ApiKeyContext>()));
            //this.Bind<CustomErrorLogContext>().ToConstructor(x => new CustomErrorLogContext());
            Kernel.Bind<IMemberRepository>().ToConstructor(ctorArg => new MemberRepository(ctorArg.Inject<AnewluvContext>()));
            Kernel.Bind<IGeoRepository>().ToConstructor(ctorArg => new GeoRepository(ctorArg.Inject<PostalData2Entities>()));
            Kernel.Bind<IPhotoRepository>().ToConstructor(
            ctorArg => new PhotoRepository(ctorArg.Inject<AnewluvContext>(), ctorArg.Inject<IMemberRepository>()));
            Kernel.Bind<IMailRepository>().ToConstructor(
            ctorArg => new MailRepository(ctorArg.Inject<AnewluvContext>(), ctorArg.Inject<IMemberRepository>()));
            Kernel.Bind<IMemberActionsRepository>().ToConstructor(
          ctorArg => new MemberActionsRepository(ctorArg.Inject<AnewluvContext>(), ctorArg.Inject<IMemberRepository>()));

            Kernel.Bind<IMembersMapperRepository>().ToConstructor(
             ctorArg => new MembersMapperRepository(
                 ctorArg.Inject<IGeoRepository>(),
                 ctorArg.Inject<IPhotoRepository>(),
                 ctorArg.Inject<IMemberRepository>(),
                 ctorArg.Inject<IMemberActionsRepository>(),
                  ctorArg.Inject<IMailRepository>(),
                 ctorArg.Inject<AnewluvContext>()));
            

            Kernel.Bind<IInfoNotificationRepository>().ToConstructor(
             ctorArg => new InfoNotificationRepository(ctorArg.Inject<NotificationContext>(),
                 ctorArg.Inject<AnewluvContext>(),
                 ctorArg.Inject<IMemberRepository>(), ctorArg.Inject<IMembersMapperRepository>() 
                 ));
          
            Kernel.Bind<IInfoNotificationService>().ToSelf();


        }
    }
}