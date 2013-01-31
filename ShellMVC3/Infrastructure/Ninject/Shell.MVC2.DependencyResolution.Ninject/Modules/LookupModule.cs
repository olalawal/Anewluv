


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Data;
using Shell.MVC2.Infrastructure.Interfaces;
//using Shell.MVC2.Services.Logging;
using Shell.MVC2.Infrastructure.Entities.UserRepairModel;

using Shell.MVC2.Services.Contracts;
using Shell.MVC2.Infrastructure.Entities.ApiKeyModel;
using Shell.MVC2.Interfaces;
using Shell.MVC2.Domain.Entities.Anewluv;



//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
    public class LookupModule : NinjectModule
    {
        public override void Load()
        {

            //TO DO maybe we can remove this maybe ?
            Kernel.Bind<IAPIkeyRepository>().ToConstructor(ctorArg => new APIkeyRepository(ctorArg.Inject<ApiKeyContext>()));
            if (!Kernel.HasModule("Shell.MVC2.DependencyResolution.Ninject.Modules.MembersModule")) //only load if not already loaded into kernel
            Kernel.Bind<IMemberRepository>().ToConstructor(ctorArg => new MemberRepository(ctorArg.Inject<AnewluvContext>()));
          
            Kernel.Bind<ILookupRepository>().ToConstructor(
                ctorArg => new LookupRepository(ctorArg.Inject<AnewluvContext>(), ctorArg.Inject<IMemberRepository>()));

            Kernel.Bind<ILookupService>().ToSelf();


        }
    }
}