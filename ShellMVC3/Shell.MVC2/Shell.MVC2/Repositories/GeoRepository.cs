using System;
using System.Linq;
using System.Web;
//using Dating.Server.Data;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;


using Ninject;



namespace Shell.MVC2.Models
{

    public partial class GeoRepository
    {


       // private  DatingService datingservicecontext;  //= new DatingService().Initialize();        
        private  PostalDataService postaldataservicecontext;  //= new PostalDataService().Initialize();
    //    private  AnewLuvFTSEntities db; // = new AnewLuvFTSEntities();
        private  PostalData2Entities postaldb; //= new PostalData2Entities();
        private LoggerService loggerservicecontext;
        private AnewLuvLogEntities loggerdb;
        private DatingService datingservicecontext;  //= new DatingService().Initialize();  
        private AnewLuvFTSEntities db; // = new AnewLuvFTSEntities();

        //TO DO


        public GeoRepository()
       {
           IKernel kernel = new StandardKernel();
          //Get these initalized
           //datingservicecontext = kernel.Get<DatingService>(); 
           postaldataservicecontext = kernel.Get<PostalDataService>();
           // db =  kernel.Get<AnewLuvFTSEntities>();
            postaldb = kernel.Get<PostalData2Entities>();
            loggerservicecontext = kernel.Get<LoggerService>();
            loggerdb = kernel.Get<AnewLuvLogEntities>();
            datingservicecontext = kernel.Get<DatingService>();  //= new DatingService().Initialize();  
            db = kernel.Get<AnewLuvFTSEntities>(); ; // = new AnewLuvFTSEntities();
        }

        
        


        public string GetMyCountry(ProfileData userProfileData)
        {

            return (from p in postaldb.Country_PostalCode_List 
                    where p.CountryID == userProfileData.CountryID
                    select p.CountryName).FirstOrDefault();
            //return postaldataservicecontext.GetCountryNameByCountryID(ProfileData.CountryID);
        }


        public MembersViewModel  PopulateQuickSearchWithGeoDemoData(MembersViewModel jsonmodel,MembersViewModel model)
        {
            Boolean countryexistsinDB = (postaldataservicecontext.GetCountryIdByCountryName(jsonmodel.MyCountryName) != null) ? true : false;

           
           // QuickSearchModel quicksearchmodel = new QuickSearchModel();
           // model.MyQuickSearch = quicksearchmodel;

            if (countryexistsinDB)
            {
                //model.MyCountryName = json.MyCountryName.Replace(" ", "");
                //model.MyRegionName = json.MyRegionName;
                //model.MyCity = json.MyCity;
                //model.MyCountryName = json.MyCountryName;
                //model.MyLongitude = json.MyLongitude;
                //model.MyLatitude = json.MyLatitude;

                //get the full province and city string
                var citystateprovince = (jsonmodel.MyRegionName != "") ? jsonmodel.MyCity + ',' + jsonmodel.MyRegionName : jsonmodel.MyCity;

                //load data for registration on registration phere maybe

                //for registration and to let us know we populated data
                model.MyQuickSearch.geocodeddata = countryexistsinDB;

                //grab whatever is in the last search if its empty

                model.MyQuickSearch.MySelectedCountryName = (model.MyQuickSearch.MySelectedCountryName != null) ? model.MyQuickSearch.MySelectedCountryName : jsonmodel.MyCountryName;
                if (jsonmodel.MyLongitude != "")
                model.MyQuickSearch.MySelectedLongitude = Convert.ToDouble(jsonmodel.MyLongitude); //verify these too maybe
                if (jsonmodel.MyLatitude != "")
                model.MyQuickSearch.MySelectedLatitude = Convert.ToDouble(jsonmodel.MyLatitude);
                model.MyQuickSearch.MySelectedCityStateProvince = (model.MyQuickSearch.MySelectedCityStateProvince != null) ? model.MyQuickSearch.MySelectedCityStateProvince : citystateprovince;
                model.MyQuickSearch.MySelectedCity = (model.MyQuickSearch.MySelectedCity != null) ? model.MyQuickSearch.MySelectedCity : jsonmodel.MyCity;
                //get postal code status
                model.MyQuickSearch.MySelectedPostalCodeStatus = (model.MyQuickSearch.MySelectedPostalCode != null) ? (model.MyQuickSearch.MySelectedPostalCode == "True") ? true : false : 
                (postaldataservicecontext.GetCountry_PostalCodeStatusByCountryName(jsonmodel.MyCountryName) == 1) ? true : false;
                //add demographic data from other provider , for now use defaults
                //set gener and ages if it was empty from last search
                model.MyQuickSearch.MySelectedIamGenderID = (model.MyQuickSearch.MySelectedIamGenderID != 0) ? model.MyQuickSearch.MySelectedIamGenderID : 1;
                model.MyQuickSearch.MySelectedSeekingGenderID = (model.MyQuickSearch.MySelectedSeekingGenderID != 0) ? model.MyQuickSearch.MySelectedSeekingGenderID : 2;
                model.MyQuickSearch.MySelectedFromAge = (model.MyQuickSearch.MySelectedFromAge != 0) ? model.MyQuickSearch.MySelectedFromAge : 18;
                model.MyQuickSearch.MyselectedToAge = (model.MyQuickSearch.MyselectedToAge != 0) ? model.MyQuickSearch.MyselectedToAge : 99;

                //populate the rest of the data
                //add the postalcode regardless , done care about the old model
              
                model.MyPostalCode = (jsonmodel.MyRegionName != null)?  postaldataservicecontext.GetPostalCodesByCountryNameCityandStateProvinceDynamic(jsonmodel.MyCountryName,
                    jsonmodel.MyCity, jsonmodel.MyRegionName).First().PostalCode : postaldataservicecontext.GetGeoPostalCodebyCountryNameAndCity(jsonmodel.MyCountryName ,jsonmodel.MyCity );
                //5/24/2012 store the non quick search data
                model.MyCountryName = jsonmodel.MyCountryName;  
                model.MyIpAddress = jsonmodel.MyIpAddress;
                model.MyUserAgent = jsonmodel.MyUserAgent;
                model.MyContinentCode = jsonmodel.MyContinentCode;
                model.MyRegionName = jsonmodel.MyRegionName;
                model.MyLatitude = jsonmodel.MyLatitude;
                model.MyLongitude = jsonmodel.MyLongitude;
                model.MyCityStateProvince  = citystateprovince;
                model.MyPostalCodeStatus = model.MyQuickSearch.MySelectedPostalCodeStatus;

            }

            return model;

        }

        public QuickSearchModel PopulateQuickSearchWithGeoDemoDataDisconnected(MembersViewModel jsonmodel, QuickSearchModel oldmodel)
        {
            Boolean countryexistsinDB = true;
            QuickSearchModel model = new QuickSearchModel();

            if (countryexistsinDB)
            {
                //model.MyCountryName = json.MyCountryName.Replace(" ", "");
                //model.MyRegionName = json.MyRegionName;
                //model.MyCity = json.MyCity;
                //model.MyCountryName = json.MyCountryName;
                //model.MyLongitude = json.MyLongitude;
                //model.MyLatitude = json.MyLatitude;

                //get the full province and city string
                var citystateprovince = (jsonmodel.MyRegionName != "") ? jsonmodel.MyCity + ',' + jsonmodel.MyRegionName : jsonmodel.MyCity;

                //load data for registration on registration phere maybe

                //for registration and to let us know we populated data
                model.geocodeddata = countryexistsinDB;

                //grab whatever is in the last search if its empty

                model.MySelectedCountryName =  jsonmodel.MyCountryName;
                 if (jsonmodel.MyLongitude !="")
                model.MySelectedLongitude = Convert.ToDouble(jsonmodel.MyLongitude); //verify these too maybe
                 if (jsonmodel.MyLatitude != "")
                model.MySelectedLatitude = Convert.ToDouble(jsonmodel.MyLatitude) ;
                model.MySelectedCityStateProvince = citystateprovince;
                model.MySelectedCity = jsonmodel.MyCity;
                //get postal code status
                model.MySelectedPostalCodeStatus = true; //(oldmodel.MySelectedPostalCode != null) ? (model.MySelectedPostalCode == "True") ? true : false :
               //  (postaldataservicecontext.GetCountry_PostalCodeStatusByCountryName(jsonmodel.MyCountryName) == 1) ? true : false;
                //add default search city


                //add demographic data from other provider , for now use defaults
                //set gener and ages if it was empty from last search
                model.MySelectedIamGenderID = 1;
                model.MySelectedSeekingGenderID = 2;
                model.MySelectedFromAge =  18;
                model.MyselectedToAge =  99;


            }

            return model;

        }


        public bool  LogGuestGeoData(MembersViewModel jsonmodel)
        {

            var sessionID = HttpContext.Current.Session.SessionID;
              //check to make sure that this session has not been logged
            if (loggerdb.GeoDataLogs.Where(p => p.SessionID == sessionID).FirstOrDefault() != null) return false;
            

                //grab whatever is in the last search if its empty
            var data = new GeoDataLog
               {
               SessionID = HttpContext.Current.Session.SessionID ,
               CountryName = jsonmodel.MyCountryName,
               
               Continent = jsonmodel.MyContinentCode ,
               RegionName = jsonmodel.MyRegionName ,
               City = jsonmodel.MyCity,
               Longitude = Convert.ToDouble(jsonmodel.MyLongitude) ,
               Lattitude =  Convert.ToDouble(jsonmodel.MyLatitude),
               UserAgent = jsonmodel.MyUserAgent ,
               IPaddress = jsonmodel.MyIpAddress ,
               CreationDate = DateTime.UtcNow 

               };

               loggerdb.GeoDataLogs.AddObject(data);

               loggerdb.SaveChanges();
               
                return true;
                


            }
                
        public bool LogMemberLoginGeoData(MembersViewModel jsonmodel)
        {

            var sessionID = HttpContext.Current.Session.SessionID;
            //check to make sure that this session has not been logged
            if (db.ProfileGeoDataLoggers.Where(p => p.SessionID  == sessionID).FirstOrDefault() != null) return false;


            //grab whatever is in the last search if its empty
            var data = new ProfileGeoDataLogger 
            {
                ProfileID =  jsonmodel.Profile.ProfileID ,
                CountryName = jsonmodel.MyCountryName,
                Continent = jsonmodel.MyContinentCode,
                RegionName = jsonmodel.MyRegionName,
                City = jsonmodel.MyCity,
                Longitude = Convert.ToDouble(jsonmodel.MyLongitude),
                Lattitude =  Convert.ToDouble(jsonmodel.MyLatitude),
                UserAgent = jsonmodel.MyUserAgent,
                IPaddress = jsonmodel.MyIpAddress,
                  CreationDate = DateTime.UtcNow
            };

            db.ProfileGeoDataLoggers.AddObject(data);

            db.SaveChanges();

            return true;



        }

         public bool countryInWatchList(string geoCountryName)
         {
         
            //var dd = from p in  postaldb.
                //

             return false;
         }
         }
}