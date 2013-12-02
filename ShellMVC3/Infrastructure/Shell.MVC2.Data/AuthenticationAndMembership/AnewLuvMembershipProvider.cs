



    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Server;

    using Dating.Server.Data.Models;
    using Dating.Server.Data.Services;

    using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;

    using System.Data.EntityClient;
    using System.Collections.ObjectModel;
    using System.Security.Principal;

    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using System.ServiceModel.DomainServices.Server.ApplicationServices;
   // using Common;
using Shell.MVC2.Infrastructure;


    using System.Globalization;
using Shell.MVC2.Interfaces;
using LoggingLibrary;
using Nmedia.Infrastructure.Domain.errorlog;


namespace Shell.MVC2.Data.AuthenticationAndMembership
{

    //=======================================================
    //Service provided by Telerik (www.telerik.com)
    //Conversion powered by NRefactory.
    //Twitter: @telerik, @toddanglin
    //Facebook: facebook.com/telerik
    //=======================================================


    // this membership serverice is for MVC
    public class AnewLuvMembershipProvider :  MembershipProvider, IAnewLuvMembershipProvider
    {

        //constant strings for reseting passwords
        const String UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; const String LOWER = "abcdefghijklmnopqrstuvwxyz"; const String NUMBERS = "1234567890"; const String SPECIAL = "*$-+?&=!%/";
        //AnewluvContext context = new AnewluvContext();
       // DatingService datingService = new DatingService();
      
         private IGeoRepository _georepository;
         private IMemberRepository _memberepository;
         private AnewluvContext _datingcontext;
        private IPhotoRepository _photorepository;
        private ErroLogging logger;


         public AnewLuvMembershipProvider(AnewluvContext datingcontext, IGeoRepository georepository,
             IMemberRepository memberepository,IPhotoRepository photorepository)            
        {
            _georepository = georepository;
            _memberepository = memberepository;
             _datingcontext = datingcontext ;
             _photorepository = photorepository ;
        }



    


      
    }
}