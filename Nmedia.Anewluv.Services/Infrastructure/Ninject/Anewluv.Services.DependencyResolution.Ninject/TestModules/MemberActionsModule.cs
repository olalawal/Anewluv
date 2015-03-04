


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;


//using Shell.MVC2.Services.Media;
//using Shell.MVC2.Services.Dating;
//using Dating.Server.Data.Services;
//using Anewluv.Services.Contracts;



//using Nmedia.DataAccess.Interfaces;
using Ninject.Activation;
using Ninject.Web.Common;
using Ninject;
using System.Data.Entity;
using Anewluv.Domain;
using GeoData.Domain.Models;
using Nmedia.Infrastructure.DependencyInjection;
using Anewluv.Services.DependencyResolution.Ninject.predicates;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;

namespace Anewluv.Services.DependencyResolution.Ninject.Modules
{


    public class MemberActionsModule : NinjectModule
    {
        /// <summary>
        /// 5-12-2014 olawal
        /// couple of neat tricks in this code.
        /// we added an extention method to the ninject activation interface to type match the DbContext to the calling service.
        /// IsInjectingToRepositoryDataSourceOfNamespace  allows up to match a single abstract IConext object to the correct Ef db context.
        /// We also added the line  Kernel.Unbind(typeof(IUnitOfWorkAsync)); since out IunitOfWork object is meant to be disposed anyways after it is used
        /// this kills two birds with one stone since there will alsways only one UnitOfwork and unbinding it before re-binding to the calling service's
        /// IContext ensures that each service call will have the correct version of the Icontext object.
        /// see below link 
        /// http://stackoverflow.com/questions/11864279/ninject-activationexception-error-activating-ialertmanagement
        /// </summary>

        public override void Load()
        {


            Bind<IDataContextAsync>().ToMethod(c => c.Kernel.Get<AnewluvContext>())
            .WhenAnyAncestorMatches(Predicates.TargetHas<IAnewluvEntitesScope>).InRequestScope();
            Bind<IDataContextAsync>().ToMethod(c => c.Kernel.Get<PostalData2Context>())
            .WhenAnyAncestorMatches(Predicates.TargetHas<InSpatialEntitesScope>).InRequestScope();

            // this.Bind<IDataContextAsync>().To<AnewluvContext>().WhenTargetHas<IAnewluvEntitesScope>().InRequestScope();
            // this.Bind<IDataContextAsync>().To<PostalData2Context>().WhenTargetHas<InSpatialEntitesScope>().InRequestScope();
            this.Bind<IUnitOfWorkAsync>().To<UnitOfWork>().InRequestScope();
            this.Bind<IGeoDataStoredProcedures>().To<PostalData2Context>().WhenTargetHas<InSpatialEntitesScope>().InRequestScope(); ;


        }
    }
}