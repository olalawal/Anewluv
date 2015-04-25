


using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;

//using Shell.MVC2.Services.Media;
//using Shell.MVC2.Services.Dating;


//to do do away with this when we go to code first , we would pull this from entities 
//using Dating.Server.Data.Models ;

//using CommonInstanceFactory.Sample.Interfaces;
//using CommonInstanceFactory.Sample.Services;
//using Anewluv.Services.Contracts;



using Anewluv.Domain;


using Ninject.Web.Common;
using Ninject.Activation;
using Ninject;
//using Nmedia.DataAccess.Interfaces;
using System.Data.Entity;
using GeoData.Domain.Models;
using Nmedia.Infrastructure.DependencyInjection;
//using Anewluv.Services.DependencyResolution.Ninject.predicates;
using Repository.Pattern.DataContext;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Ef6;

namespace Anewluv.Services.DependencyResolution.Ninject.Modules
{
	public class MessagingModule : NinjectModule
	{
		public override void Load()
		{
            
            this.Bind<IDataContextAsync>().To<AnewluvContext>().InRequestScope();
            this.Bind<IUnitOfWorkAsync>().To<UnitOfWork>().InRequestScope();
         
        }

       
      }
	
}