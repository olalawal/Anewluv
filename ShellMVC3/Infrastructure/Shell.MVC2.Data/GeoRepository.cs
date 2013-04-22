using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using Shell.MVC2.Domain.Entities.Anewluv;

using Shell.MVC2.Interfaces;
using System.Data.EntityClient;
using System.Data;

using Shell.MVC2.Data.Infrastructure;
using LoggingLibrary;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;
using Shell.MVC2.AppFabric;

namespace Shell.MVC2.Data
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GeoService" in code, svc and config file together.
    public class GeoRepository : GeoRepositoryBase ,  IGeoRepository
    {
       // private PostalData2Entities _postalcontext;

        public GeoRepository(PostalData2Entities postalcontext)
            : base(postalcontext)
        {
           
        }





        public string getcountrynamebycountryid(int countryid)
        {
            try
            {
                return (from p in _postalcontext.Country_PostalCode_List
                        where p.CountryID  == countryid
                        select p.CountryName ).FirstOrDefault();
                //return postaldataservicecontext.GetcountryNameBycountryID(profiledata.countryid);
            }
            catch (Exception ex)
            {

                Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryid.ToString(), "", "", ex.Message, ex.InnerException);
               new ErroLogging(applicationEnum.GeoLocationService ).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }
        }
        public registermodel verifyorupdateregistrationgeodata(ValidateRegistrationGeoDataModel model)
        {
           

             try
            {
               //4-24-2012 fixed code to hanlde if we did not have a postcal code
                gpsdata gpsData = new gpsdata();
            string[] tempcityAndStateProvince = model.GeoRegisterModel.city .Split(',');
            //int countryID;

            //attmept to get postal postalcode if it is empty

            model.GeoRegisterModel.ziporpostalcode = (model.GeoRegisterModel.ziporpostalcode == null) ? 
           this.getgeopostalcodebycountrynameandcity(model.GeoRegisterModel.country, tempcityAndStateProvince[0]).Where(p=>p.postalcodevalue  == model.GeoRegisterModel.ziporpostalcode ).FirstOrDefault().postalcodevalue  :
           model.GeoRegisterModel.ziporpostalcode;
            model.GeoRegisterModel.stateprovince = ((tempcityAndStateProvince.Count() > 1)) ? tempcityAndStateProvince[1] : "NA";
            //countryID = postaldataservicecontext.GetcountryIdBycountryName(model.GeoRegisterModel.country);

            //check if the  city and country match
            if (model.GeoRegisterModel.country == model.GeoMembersModel.myquicksearch.myselectedcountryname && 
                tempcityAndStateProvince[0] == model.GeoMembersModel.myquicksearch.myselectedcity)
            {
                if (model.GeoRegisterModel.lattitude  != null | model.GeoRegisterModel.lattitude  == 0)
                    return model.GeoRegisterModel;

            }

            //get GPS data here
            //conver the unquiqe coountry Name to an ID
            //store country ID for use later
            //get the longidtue and latttude 
            //1-11-2011 postal code and city are flipped by the way not this function should be renamed
            //TO DO rename this function.                  
            gpsData = this.getgpsdatasinglebycitycountryandpostalcode(model.GeoRegisterModel.country, model.GeoRegisterModel.ziporpostalcode, tempcityAndStateProvince[0]);


            model.GeoRegisterModel.lattitude  = (gpsData != null) ? gpsData.lattitude   : 0;
            model.GeoRegisterModel.longitude = (gpsData != null) ? gpsData.longitude   : 0;

            return model.GeoRegisterModel ;
            }
            catch (Exception ex)
            {

              Exception convertedexcption = new CustomExceptionTypes.GeoLocationException ("","","",ex.Message , ex.InnerException);
               new ErroLogging(applicationEnum.GeoLocationService ).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }

        }
        //gets the country list and orders it
        //added sorting
        public List<countrypostalcode> getcountryandpostalcodestatuslist()
        {



             try
            {
           //    List<Country_PostalCode_List> myQuery = default(List<Country_PostalCode_List>);           
           // myQuery = _postalcontext.Country_PostalCode_List.Where(p => p.countryName != "").OrderBy(p => p.countryName).ToList();

           //return (from s in myQuery select new countrypostalcode {  name   = s.countryName , code = s.country_Code , 
           //    customregionid = s.countryCustomRegionID , region = s.country_Region , haspostalcode  = Convert.ToBoolean(s.PostalCodes)   }).ToList();

           // return myQuery;

                return CachingFactory.SharedObjectHelper.getcountryandpostalcodestatuslist(_postalcontext);

            }
            catch (Exception ex)
            {

              Exception convertedexcption = new CustomExceptionTypes.GeoLocationException ("","","",ex.Message , ex.InnerException);
               new ErroLogging(applicationEnum.GeoLocationService ).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }
        }      
        /// <summary>
        /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
        /// </summary>
        /// 
        public bool getpostalcodestatusbycountryname(string countryname)
        {
          

             try
            {
                List<Country_PostalCode_List> myQuery = default(List<Country_PostalCode_List>);
            //Dim ctx As New Entities()
            myQuery = _postalcontext.Country_PostalCode_List.Where(p => p.CountryName  == countryname).ToList();

            return (myQuery.Count > 0 ? true : false);
          //  return myQuery.FirstOrDefault().PostalCodes.Value;
            }
            catch (Exception ex)
            {

              Exception convertedexcption = new CustomExceptionTypes.GeoLocationException (countryname,"","",ex.Message , ex.InnerException);
              new ErroLogging(applicationEnum.GeoLocationService ).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
              throw convertedexcption;
            }
        }            
        public int getcountryidbycountryname(string countryname)
        {
          

             try
            {
                List<Country_PostalCode_List> countryCodeQuery = default(List<Country_PostalCode_List>);
                 //3-18-2013 olawal added code to remove the the spaces when we test
            countryCodeQuery = _postalcontext.Country_PostalCode_List.Where(p => p.CountryName .Replace(" ","") == countryname).ToList();

            if (countryCodeQuery.Count() > 0)
            {
                return countryCodeQuery.FirstOrDefault().CountryID ;

            }
            else
            {
                return 0;
            }
            }
            catch (Exception ex)
            {

              Exception convertedexcption = new CustomExceptionTypes.GeoLocationException (countryname,"","",ex.Message , ex.InnerException);
              new ErroLogging(applicationEnum.GeoLocationService ).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
              throw convertedexcption;
            }
        }
        //Dynamic LINQ to Entites quries 
        //*****************************************************************************************************************************************
        public List<citystateprovince> getcitylistdynamic(string countryname, string postalcode, string prefixtext)
        {
            try{

                var citylist = _postalcontext.GetCityListByCountryPostalCode(countryname, postalcode,prefixtext ,false );

                return ((from s in citylist.ToList() select new citystateprovince {  city = s.City ,stateprovince = s.State_Province }).ToList());
         
            }
           catch (Exception ex)
            {

              Exception convertedexcption = new CustomExceptionTypes.GeoLocationException (countryname ,"","",ex.Message , ex.InnerException);
               new ErroLogging(applicationEnum.GeoLocationService ).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }




        }
        public List<gpsdata> getgpsdatabycountrypostalcodeandcity(string countryname, string postalcode, string city)
        {
            

             try
            {

                var gpsdatalist = _postalcontext.GetGpsDataByCityCountryAndPostalCode(countryname, postalcode,city, false);
                return ((from s in gpsdatalist.ToList() select new gpsdata { lattitude =s.Latitude , longitude = s.Longitude ,stateprovince = s.State_Province  }).ToList());
            
            }
            catch (Exception ex)
            {

              Exception convertedexcption = new CustomExceptionTypes.GeoLocationException (countryname ,postalcode,"",ex.Message , ex.InnerException);
              new ErroLogging(applicationEnum.GeoLocationService ).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }

        }
        public List<gpsdata> getgpsdatabycountryandcity(string countryname, string city)
        {
           

             try
            {
                var gpsdatalist = _postalcontext.GetGpsDataByCountryNameAndCity(countryname,  city, false);
                return ((from s in gpsdatalist.ToList() select new gpsdata { lattitude = s.Latitude, longitude = s.Longitude, stateprovince = s.State_Province }).ToList());
            
            }
            catch (Exception ex)
            {

              Exception convertedexcption = new CustomExceptionTypes.GeoLocationException (countryname,"","",ex.Message , ex.InnerException);
              new ErroLogging(applicationEnum.GeoLocationService  ).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }

        }
        public gpsdata getgpsdatasinglebycitycountryandpostalcode(string countryname, string postalcode, string city)
        {
         
             gpsdata gpsdata = new gpsdata();
             try
            {
                var s = _postalcontext.GetGpsDataByCountryNameAndCity(countryname, city, false).FirstOrDefault();
                if (s != null)
                {
                    return new gpsdata { lattitude = s.Latitude, longitude = s.Longitude, stateprovince = s.State_Province };
                }
                return gpsdata;
            
            }
            catch (Exception ex)
            {

              Exception convertedexcption = new CustomExceptionTypes.GeoLocationException (countryname ,postalcode,"",ex.Message , ex.InnerException);
               new ErroLogging(applicationEnum.GeoLocationService ).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }


        }
        public List<postalcode> getpostalcodesbycountryandcityprefixdynamic(string countryname, string city, string prefixtext)
        {
      

             try
            {


                var gpsdatalist = _postalcontext.GetPostalCodesByCountryAndCityPrefixDynamic(countryname, city, prefixtext, false);
                return ((from s in gpsdatalist.ToList() select new postalcode { postalcodevalue = s.PostalCode }).ToList());
            
            }
            catch (Exception ex)
            {

              Exception convertedexcption = new CustomExceptionTypes.GeoLocationException (countryname ,city ,"",ex.Message , ex.InnerException);
               new ErroLogging(applicationEnum.GeoLocationService ).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }
        }
        //gets the single geo code as string
        public List<postalcode> getgeopostalcodebycountrynameandcity(string countryname, string city)
        {

             try
            {
                                      
               // var dd = _postalcontext.GetGeoPostalCodebyCountryNameAndCity(countryname, city);
                var gpsdatalist = _postalcontext.GetGeoPostalCodebyCountryNameAndCity(countryname, city);
                return ((from s in gpsdatalist.ToList() select new postalcode { postalcodevalue = s.PostalCode }).ToList());
            }
              
            
            catch (Exception ex)
            {

              Exception convertedexcption = new CustomExceptionTypes.GeoLocationException (countryname ,city ,"",ex.Message , ex.InnerException);
               new ErroLogging(applicationEnum.GeoLocationService ).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }

        }
        public bool validatepostalcodebycountryandcity(string countryname, string city, string postalcode)
        {

            try
            {

                var foundpostalcodes =  _postalcontext.ValidatePostalCodeByCOuntryandCity(countryname, city,postalcode ,false );
                if (foundpostalcodes.Count() > 0) return true;

            }
              
            catch (Exception ex)
            {

                Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryname, city, postalcode, ex.Message, ex.InnerException);
                new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }
              return false ;
        }
        public List<postalcode> getpostalcodesbycountryandlatlongdynamic(string countryname, string lattitude, string longitude)
        {
           
             try
            {


                var geopostalcodes = _postalcontext.GetPostalCodesByCountryAndLatLong(countryname,lattitude ,longitude, false);
                return ((from s in geopostalcodes.ToList() select new postalcode { postalcodevalue = s.PostalCode }).ToList());
            }
            catch (Exception ex)
            {

              Exception convertedexcption = new CustomExceptionTypes.GeoLocationException (countryname,lattitude, longitude,ex.Message , ex.InnerException);
               new ErroLogging(applicationEnum.GeoLocationService ).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }
        }
        public List<postalcode> getpostalcodesbycountrynamecityandstateprovincedynamic(string countryname, string city, string stateprovince)
        {
           

             try
            {
                var geopostalcodes = _postalcontext.GetPostaCodesByCountryNameCityAndStateProvince(countryname, city, stateprovince,false);
                return ((from s in geopostalcodes.ToList() select new postalcode { postalcodevalue = s.PostalCode }).ToList());
            }
            catch (Exception ex)
            {

                Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryname, city, stateprovince, ex.Message, ex.InnerException);
               new ErroLogging(applicationEnum.GeoLocationService ).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }
        }
        public List<country> getcountrylist()
        {

             try
            {
#if DISCONECTED
                       
                        List<country> countrylist = new List<country>();
                        countrylist.Add(new country { countryvalue = "United", countryindex = "44", selected = false });
                        countrylist.Add(new country { countryvalue = "Canada", countryindex = "43", selected = false });
                        return countrylist;

#else
                //List<country> tmplist = new List<country>();
                //// Loop over the int List and modify it.
                ////insert the first one as ANY
                //tmplist.Add(new country { id = "0", name  = "Any" });
                //foreach (countrypostalcode item in this.getcountry_postalcode_listandorderbycountry())
                //{
                //    var currentcountry = new country { id = item.id .ToString(),  name = item.name   };
                //    tmplist.Add(currentcountry);
                //}
                //return tmplist;
                return CachingFactory.SharedObjectHelper.getcountrylist(_postalcontext);
               
#endif
            }
            catch (Exception ex)
            {

              Exception convertedexcption = new CustomExceptionTypes.GeoLocationException ("","","",ex.Message , ex.InnerException);
               new ErroLogging(applicationEnum.GeoLocationService ).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }

        }
        public List<citystateprovince> getfilteredcitiesold(string filter, string country, int offset)
        {


             try
            {

                var customers = this.getcitylistdynamic(country, filter, "");
                return ((from s in customers.Take(50).ToList() select new citystateprovince { city = s.city   + "," + s.stateprovince  }).ToList());

            }
            catch (Exception ex)
            {

                Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(country, "", filter, ex.Message, ex.InnerException); 
                new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }
        }
        public List<citystateprovince> getfilteredcities(string filter, string country, int offset)
        {

            List<citystateprovince> temp;
            try
            {
                var customers = this.getcitylistdynamic(country, filter, "").Take(50);

                temp = (from s in customers.ToList() select new citystateprovince { stateprovince = s.city  + "," + s.stateprovince }).ToList();
                return temp;

            }
               catch (Exception ex)
            {

                Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(country, "", filter, ex.Message, ex.InnerException); 
               new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }

        }
        public List<postalcode> getfilteredpostalcodes(string filter, string country, string city, int offset)
        {

             try
            {

                var customers = this.getpostalcodesbycountryandcityprefixdynamic(country, city, filter);

                return ((from s in customers.Skip(offset).Take(25).ToList() select new postalcode { postalcodevalue = s.postalcodevalue }).ToList());

            }
            catch (Exception ex)
            {

              Exception convertedexcption = new CustomExceptionTypes.GeoLocationException (country,city,filter,ex.Message , ex.InnerException);
               new ErroLogging(applicationEnum.GeoLocationService ).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }
        }

        #region "Spatial Functions"





        // probbaly won't use this, lets add lat long and other stuff to model
        //public Nullable<double> DistBTWMembersByLatLon(string profileid1, string profileid2)
        //{
        //    DatingService datingService = new DatingService();
        //    //confusion here
        //    PostalDataService postalService = new PostalDataService();




        //    profiledata[] tmpprofile = new profiledata[2];

        //    //create and array of GPS data
        //   IQueryable<GpsData>[] _GpsData =  new IQueryable<GpsData>[2];


        //   //first get the postal codes and countries
        //  tmpprofile[0]= datingService.GetProfileDataByProfileID(profileid1);
        //  tmpprofile[1] = datingService.GetProfileDataByProfileID(profileid2);



        //   // string StrSQL = "Select PostalCode from profilesData Where profileID=";
        //    //Data_Access.OpenDatingDB();
        //   // System.Data.SqlClient.SqlDataReader ConnectionReader = null;


        //    //get profile Data to get country and postal code then we can use 
        //    //    List<GpsData> _GpsData = new List<GpsData>()

        //    try
        //    {
        //        //get dps data now
        //       // _GpsData[0] = postalService.GetGpsDataBycountryPostalCodeandcity (GetcountryNameBycountryID(tmpprofile[0].countryID),tmpprofile[0].PostalCode,"");
        //      //  _GpsData[1] = postalService.GetGpsDataBycountryPostalCodeandcity(GetcountryNameBycountryID(tmpprofile[1].countryID), tmpprofile[1].PostalCode, ""); 


        //        //now use gps data to get the values

        //            return GetdistanceBetweenMembers(_GpsData[0].FirstOrDefault().Latitude, 
        //                                             _GpsData[0].FirstOrDefault().Longitude,
        //                                             _GpsData[1].FirstOrDefault().Latitude,
        //                                             _GpsData[1].FirstOrDefault().Longitude,"M");

        //    }
        //    catch
        //    {
        //        return null;
        //    }

        //}


        // use this function to get distance at the same time, add it to the model
        public double? getdistancebetweenmembers(double lat1, double lon1, double lat2, double lon2, string unit)
        {

            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == "K")
            {
                dist = dist * 1.609344;
            }
            else if (unit == "N" | unit =="")
            {
                dist = dist * 0.8684;
            }
            return dist;
        }



        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private static double rad2deg(double rad)
        {
            return rad / Math.PI * 180.0;
        }

        #endregion


    }
}
