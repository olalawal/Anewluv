using GeoData.Domain.Models;
using GeoData.Domain.Models.ViewModels;
using GeoData.Domain.ViewModels;
using Repository.Pattern.UnitOfWork;
//using Nmedia.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anewluv.DataExtentionMethods
{


    public static class spatialextentions
    {

        #region "Spatial Functions"


        /// 
        /// Convert meters to miles
        /// 
        /// 
        /// 
        public static double MetersToMiles(double? meters)
        {
            if (meters == null)
                return 0F;

            return meters.Value * 0.000621371192;
        }

        /// 
        /// Convert miles to meters
        /// 
        /// 
        /// 
        public static double MilesToMeters(double? miles)
        {
            if (miles == null)
                return 0;

            return miles.Value * 1609.344;
        }

        public static DbGeography CreatePoint(double latitude, double longitude)
        {
            var text = string.Format(CultureInfo.InvariantCulture.NumberFormat,
                                        "POINT({0} {1})", longitude, latitude);
            // 4326 is most common coordinate system used by GPS/Maps
            return DbGeography.PointFromText(text, 4326);
        }
        


        public static List<countrypostalcode> getcountryandpostalcodestatuslist(IGeoDataStoredProcedures _storedProcedures)
        {

           // string countryname = "";
            //geodb.DisableProxyCreation = true;

            try
            {
                //  List<Country_PostalCode_List> myQuery = default(List<Country_PostalCode_List>);
                //Dim ctx As New Entities()
                 var results =  _storedProcedures.GetCountryPostalCodeList().ToList();


                 return (from s in results
                         select new countrypostalcode
                         {
                             name = s.CountryName,
                             id = s.CountryID.ToString(),
                             code = s.Country_Code,
                             customregionid = s.CountryCustomRegionID,
                             region = s.Country_Region,
                             haspostalcode = Convert.ToBoolean(s.PostalCodes)
                         }).ToList();

                //object params                      
                // countryname = geodb.ExecuteStoredProcedure<string>(query + " @CountryID ", parameters).Select().FirstOrDefault();
//if (countryname != null) return countryname;


            }
            catch (Exception ex)
            {

                throw ex;
                //throw convertedexcption;
            }

           
        }


        public static  async Task<int?> getcountrycountryidbycountryname(GeoModel model, IGeoDataStoredProcedures _storedProcedures)
        {

            int? countryid = null;
            //geodb.DisableProxyCreation = true;

            try
            {
                //  List<Country_PostalCode_List> myQuery = default(List<Country_PostalCode_List>);
                //Dim ctx As New Entities()
                 var result = await _storedProcedures.GetCountryCountryIDByCountryName(model.country);

                 countryid = result;
                //object params                      
                // countryname = geodb.ExecuteStoredProcedure<string>(query + " @CountryID ", parameters).Select().FirstOrDefault();
                 return countryid;

            }
            catch (Exception ex)
            {

                throw ex;
                //throw convertedexcption;
            }

           // return countryname;
        }

           public static string getcountrynamebycountryid(GeoModel model,IGeoDataStoredProcedures _storedProcedures)
        {

            string countryname = "";
            //geodb.DisableProxyCreation = true;
           
                try
                {
                    //  List<Country_PostalCode_List> myQuery = default(List<Country_PostalCode_List>);
                    //Dim ctx As New Entities()
                     countryname = _storedProcedures.GetCountryNameByCountryID(model.countryid);

                    //object params                      
                   // countryname = geodb.ExecuteStoredProcedure<string>(query + " @CountryID ", parameters).Select().FirstOrDefault();
                    if (countryname != null) return countryname;


                }
                catch (Exception ex)
                {

                  throw ex;
                    //throw convertedexcption;
                }
              
            return countryname;
            }


           public static bool getpostalcodestatusbycountryname(GeoModel model, IGeoDataStoredProcedures geodb)
           {

               if (model.country == null) return false;

                  // geodb.DisableProxyCreation = true;             
                   try
                   {
                       //  List<Country_PostalCode_List> myQuery = default(List<Country_PostalCode_List>);
                       //Dim ctx As New Entities()
                       var result = geodb.GetPostalCodeStatusBycountryName(model.country);
                       return result;


                    //   return (myQuery.Count > 0 ? true : false);
                       //  return myQuery.FirstOrDefault().PostalCodes.Value

                   }
                   catch (Exception ex)
                   {
                       throw ex;
                       //throw convertedexcption;
                   }

               


           }
         

          public static gpsdata getgpsdatabycitycountrypostalcode(GeoModel model, IGeoDataStoredProcedures _storedProcedures)
           {

               //_unitOfWorkAsync.DisableProxyCreation = true;
               //  //using (var db = _unitOfWorkAsync)
               //  {
               try
               {


                   if (model.country == null | model.city == null) return null;

                   //   IQueryable<GpsData> functionReturnValue = default(IQueryable<GpsData>);

                   List<gpsdata> _GpsData = new List<gpsdata>();
                   model.country = string.Format(model.country.Replace(" ", ""));
                   // fix country names if theres a space
                   // strCity = String.Format("{0}%", strCity) '11/13/2009 addded wild ca


                   var gpsdata = _storedProcedures.GetGPSDatasByCountryIdPostalCode(model.country, model.postalcode);
                   return gpsdata.ToList().FirstOrDefault();
                   //var s = _postalcontext.GetGpsDataSingleByCityCountryAndPostalCode(countryname, postalcode, city);
                   //if (gpsdata != null)
                   //{
                   //    return new gpsdata { lattitude = s.Latitude, longitude = s.Longitude, stateprovince = s.State_Province };
                   //}
                   //return gpsdata;

               }
               catch (Exception ex)
               {

                   throw ex;

                   //throw convertedexcption;
               }
           
           }


          public static async Task<CityList> getcitystateprovincebycountrylatlong(GeoModel model, IGeoDataStoredProcedures _storedProcedures)
           {

               //_unitOfWorkAsync.DisableProxyCreation = true;
               //  //using (var db = _unitOfWorkAsync)
               //  {
               try
               {


                   if (model.country == null | model.lattitude == null | model.longitude ==null ) return null;

                   //IQueryable<GpsData> functionReturnValue = default(IQueryable<GpsData>);

                   List<citystateprovince> _citystateprovince = new List<citystateprovince>();
                   model.country = string.Format(model.country.Replace(" ", ""));
                   // fix country names if theres a space
                   // strCity = String.Format("{0}%", strCity) '11/13/2009 addded wild ca


                   var cityinfo = await _storedProcedures.GetCityStateprovinceBycountryandlatlong(model.country, model.lattitude, model.longitude);
                   return cityinfo;
                   //var s = _postalcontext.GetGpsDataSingleByCityCountryAndPostalCode(countryname, postalcode, city);
                   //if (gpsdata != null)
                   //{
                   //    return new gpsdata { lattitude = s.Latitude, longitude = s.Longitude, stateprovince = s.State_Province };
                   //}
                   //return gpsdata;

               }
               catch (Exception ex)
               {

                   throw ex;

                   //throw convertedexcption;
               }

           }

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
        public static double? getdistancebetweenmembers(double lat1, double lon1, double lat2, double lon2, string unit)
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
            else if (unit == "N" | unit == "")
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