using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using DatingModel;



namespace Misc
{
    class Program
    {
        static void Main(string[] args)
        {

           //MisFunctions.FixBadUserGeoData();
          //FixBadUserGeoData();
            MisFunctions.ConvertFlatProfileandprofiledata();
            MisFunctions.ConvertProfileCollections();
            MisFunctions.ConvertProfileMetaDataBasicCollections();
            MisFunctions.ConvertProfileDataMetadataCollectionsPhoto();
            MisFunctions.ConvertProfileSearchSettingsCollections();
        }
    }
}
