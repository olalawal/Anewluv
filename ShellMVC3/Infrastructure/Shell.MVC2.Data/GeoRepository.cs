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
       // private PostalDataService _postalcontext;

        public GeoRepository(PostalDataService  postalcontext)
            : base(postalcontext)
        {
           
        }





        public string getcountrynamebycountryid(int countryid)
        {
            try
            {
                return (from p in _postalcontext.GetCountry_PostalCode_List()
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
           this.getpostalcodesbycountrynamecity(model.GeoRegisterModel.country, tempcityAndStateProvince[0]).Where(p=>p.postalcodevalue  == model.GeoRegisterModel.ziporpostalcode ).FirstOrDefault().postalcodevalue  :
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
            gpsData = this.getgpsdatabycitycountrypostalcode(model.GeoRegisterModel.country, model.GeoRegisterModel.ziporpostalcode, tempcityAndStateProvince[0]);


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

                Exception convertedexcption = new CustomExceptionTypes.GeoLocationException("", "", "", ex.Message, ex.InnerException);
                new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
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
            myQuery = _postalcontext.GetCountry_PostalCode_List().ToList().Where(p => p.CountryName  == countryname).ToList();

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
            countryCodeQuery = _postalcontext.GetCountry_PostalCode_List().ToList().Where(p => p.CountryName .Replace(" ","") == countryname).ToList();

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
        public List<citystateprovince> getcitystateprovincelistbycountrynamepostalcodefilter(string countryname, string postalcode, string filter)
        {
            try{

                var citylist = _postalcontext.GetCityListDynamic (countryname, postalcode,filter );
                int index = 0;
                return ((from s in citylist.ToList() select new citystateprovince { citystateprovincevalue = s.City + "," + s.State_Province }).ToList());
         
            }
           catch (Exception ex)
            {

              Exception convertedexcption = new CustomExceptionTypes.GeoLocationException (countryname ,"","",ex.Message , ex.InnerException);
               new ErroLogging(applicationEnum.GeoLocationService ).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }




        }
        public List<gpsdata> getgpsdatalistbycountrycitypostalcode(string countryname, string city, string postalcode)
        {
            

             try
            {

                var gpsdatalist = _postalcontext.GetGpsDataByCountryPostalCodeandCity (countryname, postalcode,city);
                return ((from s in gpsdatalist.ToList() select new gpsdata { lattitude =s.Latitude , longitude = s.Longitude ,stateprovince = s.State_Province  }).ToList());
            
            }
            catch (Exception ex)
            {

              Exception convertedexcption = new CustomExceptionTypes.GeoLocationException (countryname ,postalcode,"",ex.Message , ex.InnerException);
              new ErroLogging(applicationEnum.GeoLocationService ).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }

        }
        public List<gpsdata> getgpsdatalistbycountrycity(string countryname, string city)
        {
           

             try
            {
                var gpsdatalist = _postalcontext.GetGpsDataByCountryAndCity (countryname,  city);
                return ((from s in gpsdatalist.ToList() select new gpsdata { lattitude = s.Latitude, longitude = s.Longitude, stateprovince = s.State_Province }).ToList());
            
            }
            catch (Exception ex)
            {

              Exception convertedexcption = new CustomExceptionTypes.GeoLocationException (countryname,"","",ex.Message , ex.InnerException);
              new ErroLogging(applicationEnum.GeoLocationService  ).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }

        }
        public gpsdata getgpsdatabycitycountrypostalcode(string countryname, string city, string postalcode)
        {
         
             gpsdata gpsdata = new gpsdata();
             try
            {
                var s = _postalcontext.GetGpsDataSingleByCityCountryAndPostalCode(countryname,postalcode , city);
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
        public List<postalcode> getpostalcodesbycountrycityfilter(string countryname, string city, string filter)
        {      

             try
            {


                var gpsdatalist = _postalcontext.GetPostalCodesByCountryAndCityPrefixDynamic(countryname, city, filter);
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
        public List<postalcode> getpostalcodesbycountrynamecity(string countryname, string city)
        {

             try
            {
                 
                var postalcodelist = _postalcontext.getpostalcodesbycountrynamecity(countryname, city);
                return ((from s in postalcodelist.ToList() select new postalcode { postalcodevalue = s.PostalCode }).ToList());
                
            }
              
            
            catch (Exception ex)
            {

              Exception convertedexcption = new CustomExceptionTypes.GeoLocationException (countryname ,city ,"",ex.Message , ex.InnerException);
               new ErroLogging(applicationEnum.GeoLocationService ).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }

        }
        public bool validatepostalcodebycountrycitypostalcode(string countryname, string city, string postalcode)
        {

            try
            {

                var foundpostalcodes =  _postalcontext.ValidatePostalCodeByCOuntryandCity(countryname, city,postalcode  );
                return foundpostalcodes;
                //if (foundpostalcodes Count() > 0) return true;

            }
              
            catch (Exception ex)
            {

                Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryname, city, postalcode, ex.Message, ex.InnerException);
                new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }
              //return false ;
        }
        public List<postalcode> getpostalcodesbycountrylatlong(string countryname, string lattitude, string longitude)
        {
           
             try
            {


                var geopostalcodes = _postalcontext.GetPostalCodesByCountryAndLatLongDynamic (countryname,lattitude ,longitude);
                return ((from s in geopostalcodes.ToList() select new postalcode { postalcodevalue = s.PostalCode }).ToList());
            }
            catch (Exception ex)
            {

              Exception convertedexcption = new CustomExceptionTypes.GeoLocationException (countryname,lattitude, longitude,ex.Message , ex.InnerException);
               new ErroLogging(applicationEnum.GeoLocationService ).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }
        }
        public List<postalcode> getpostalcodesbycountrynamecitystateprovince(string countryname, string city, string stateprovince)
        {
           

             try
            {
                var geopostalcodes = _postalcontext.GetPostalCodesByCountryNameCityandStateProvinceDynamic(countryname, city, stateprovince);
                return ((from s in geopostalcodes.ToList() select new postalcode { postalcodevalue  = s.PostalCode }).ToList());
            }
            catch (Exception ex)
            {

                Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryname, city, stateprovince, ex.Message, ex.InnerException);
               new ErroLogging(applicationEnum.GeoLocationService ).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }
        }

       
        public List<citystateprovince> getfilteredcitiesbycountryfilter(string country, string filter)
        {

            List<citystateprovince> temp;
            try
            {
                var customers = _postalcontext.GetCityListDynamic  (country, "", filter).Take(50);
                
                temp = (from s in customers.ToList() select new citystateprovince { citystateprovincevalue = s.City + "," + s.State_Province }).ToList();
                return temp;

            }
               catch (Exception ex)
            {

                Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(country, "", filter, ex.Message, ex.InnerException); 
               new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                throw convertedexcption;
            }

        }
        public List<postalcode> getfilteredpostalcodesbycountrycityfilter(string country, string city, string filter)
        {

             try
            {

                var customers = _postalcontext.GetPostalCodesByCountryAndCityPrefixDynamic (country, city, filter);

                return ((from s in customers.Take(25).ToList() select new postalcode { postalcodevalue = s.PostalCode  }).ToList());

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
