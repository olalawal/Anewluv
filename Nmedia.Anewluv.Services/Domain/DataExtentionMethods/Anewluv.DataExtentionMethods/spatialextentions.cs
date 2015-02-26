using GeoData.Domain.Models;
using GeoData.Domain.ViewModels;
//using Nmedia.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Anewluv.DataExtentionMethods
{


    public static class spatialextentions
    {

        #region "Spatial Functions"


           public static string getcountrynamebycountryid(GeoModel model,IUnitOfWorkAsync geodb)
        {

            string countryname = "";
            geodb.DisableProxyCreation = true;
           
                try
                {
                    //return (from p in _postalcontext.GetCountry_PostalCode_List()
                    //where p.CountryID  == countryid
                    //select p.CountryName ).Select().FirstOrDefault();
                    //return postaldataservicecontext.GetcountryNameBycountryID(profiledata.countryid);      
                    geodb.SetIsolationToDefault = true;
                    //TDocRecon loandetail2 = new TDocRecon();
                    string query = "sp_GetCountryNameByCountryID";
                    SqlParameter parameter = new SqlParameter("@CountryID", model.countryid);
                    parameter.ParameterName = "@CountryID";
                    parameter.SqlDbType = System.Data.SqlDbType.VarChar;
                    parameter.Size = 40;
                    //Procedure or function 'usp_GetHudFeeReviewLoanDetails' expects parameter '@LoanNbr', which was not supplied.
                    //parameter.TypeName 
                    var parameters = new object[] { parameter };

                    //object params                      
                    countryname = geodb.ExecuteStoredProcedure<string>(query + " @CountryID ", parameters).Select().FirstOrDefault();
                    if (countryname != null) return countryname;


                }
                catch (Exception ex)
                {

                  throw ex;
                    //throw convertedexcption;
                }
              
            return countryname;
            }


           public static bool getpostalcodestatusbycountryname(GeoModel model, IUnitOfWorkAsync geodb)
           {

               if (model.country == null) return false;

                   geodb.DisableProxyCreation = true;             
                   try
                   {
                       //  List<Country_PostalCode_List> myQuery = default(List<Country_PostalCode_List>);
                       //Dim ctx As New Entities()
                       var myQuery = geodb.Repository<Country_PostalCode_List>().Find().ToList().ToList().Where(p => p.CountryName == model.country).ToList();

                       return (myQuery.Count > 0 ? true : false);
                       //  return myQuery.FirstOrDefault().PostalCodes.Value

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