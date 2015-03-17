using Anewluv.Domain;
using Anewluv.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using DatingModel;
//using Shell.MVC2.Tests.Common;


namespace Misc
{
    class Program
    {
        static void Main(string[] args)
        {

           // var context = new AnewluvContext();
          //  Utils.SeedDebug(context);

          //var ddd =   ImageUtils.SmallImageTestdata();
            MisFunctions.StartDebuggingTest();
            MisFunctions.ConvertFlatProfileandprofiledata();
            MisFunctions.ConvertProfileMails();
            MisFunctions.ConvertProfileCollections();
        MisFunctions.ConvertProfileMetaDataBasicCollections();
        MisFunctions.ConvertProfileDataMetadataCollectionsPhoto();
            MisFunctions.ConvertProfileSearchSettingsCollections();
       //  MisFunctions.FixBadUserGeoData();
         //  FixBadUserGeoData();
        }
    }
}
