


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Data;
using Shell.MVC2.Infrastructure.Interfaces;
using Shell.MVC2.Data.Infrastructure;
//using Shell.MVC2.Services.Logging;
using Nmedia.Infrastructure.Domain.Errorlog;
using Shell.MVC2.Services.Contracts;
using System.ServiceModel;




//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
    public class ErrorLoggingModule : NinjectModule
    {
        public override void Load()
        {

       

            Kernel.Bind<IErrorLoggingRepository>().ToConstructor(
             ctorArg => new ErrorLoggingRepository(ctorArg.Inject<ErrorlogContext>()));

            Kernel.Bind<IErrorLoggingService>().ToSelf().InScope(c => OperationContext.Current); 


        }
    }
}