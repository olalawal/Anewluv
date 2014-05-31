


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Shell.MVC2.Data;

//using Shell.MVC2.Services.Logging;


using Anewluv.Services.Contracts;
using Shell.MVC2.Interfaces;





//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;

namespace Shell.MVC2.DependencyResolution.Ninject.Modules
{
    public class CommonModule : NinjectModule
    {
        public override void Load()
        {

            //TO DO maybe we can remove this maybe ?
           // Kernel.Bind<IAPIkeyRepository>().ToConstructor(ctorArg => new APIkeyRepository(ctorArg.Inject<ApiKeyContext>()));                    
            Kernel.Bind<ICommonRepository>().ToConstructor(
                ctorArg => new CommonRepository());

            Kernel.Bind<ICommonService>().ToSelf();


        }
    }
}