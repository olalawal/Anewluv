


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Data;
using Shell.MVC2.Infrastructure.Interfaces;
//using Shell.MVC2.Services.Logging;
using Shell.MVC2.Infrastructure.Entities.UserRepairModel;

using Shell.MVC2.Services.Contracts;



//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
    public class UserRepairLogModule : NinjectModule
    {
        public override void Load()
        {

           

            Kernel.Bind<IUserRepairRepository>().ToConstructor(
             ctorArg => new UserRepairRepository(ctorArg.Inject<UserRepairLogContext>()));

            Kernel.Bind<IUserRepairService >().ToSelf();


        }
    }
}