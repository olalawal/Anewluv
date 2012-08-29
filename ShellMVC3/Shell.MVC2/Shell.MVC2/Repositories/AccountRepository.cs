using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using Dating.Server.Data;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;

//using RiaServicesContrib.Mvc;
//using RiaServicesContrib.Mvc.Services;

using System.Text;

using System.Web.Mvc;

using Common;


namespace Shell.MVC2.Models
{
    public partial  class AccountRepository
    {

        //TO DO
        //Get these initalized
        DatingService datingservicecontext = new DatingService().Initialize();
        PostalDataService postaldataservicecontext = new PostalDataService().Initialize();
        AnewLuvFTSEntities db = new AnewLuvFTSEntities();
        PostalData2Entities postaldb = new PostalData2Entities();



        



    }
}