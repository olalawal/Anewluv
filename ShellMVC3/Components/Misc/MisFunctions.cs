using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatingModel;
using Dating.Server.Data.Models;
using Dating.Server.Data.Services;
using LoggingLibrary;

namespace Misc
{
   public static class MisFunctions
    {


        public static void FixBadUserGeoData()
        {
            var db = new Dating.Server.Data.Services.DatingService();
            var datingentities = new AnewLuvFTSEntities();
            var postaldataentities = new PostalData2Entities();
            var postaldb= new PostalDataService();
            List<MembersViewModel> membersmodels = new List<MembersViewModel>();
            List<profiledata> profiledatas =  new List<profiledata>();
            //  Dim testSignatureSample As String = New LargeImageTestdata().LargeImageTest
            //get all the profile Data's with errors 

            profiledatas = datingentities.ProfileDatas.Where(p => p.Longitude ==0  && p.Latitude == 0 ).ToList();





            foreach (profiledata  tcd in profiledatas)
            {
               

               //get country and city to get lattitude and longitude
                var countryname =  postaldb.GetCountryNameByCountryID(tcd.CountryID);
                int haspostalcodes = postaldb.GetCountry_PostalCodeStatusByCountryName(countryname);
                string postalcodegpsdata = (haspostalcodes == 1)? postaldb.GetGeoPostalCodebyCountryNameAndCity(countryname,tcd.City):"";
                IQueryable<GpsData> dd = postaldb.GetGpsDataByCountryAndCity(countryname,tcd.City);
                
                tcd.PostalCode = postalcodegpsdata;
                tcd.Latitude = dd.Count() > 0?  dd.FirstOrDefault().Latitude :0.0;
                tcd.Longitude = dd.Count() > 0? dd.FirstOrDefault().Longitude:0.0;
                tcd.State_Province = dd.Count() > 0 ? dd.FirstOrDefault().State_Province : null; 
                           
                        
               
                    
                 


                try
                {


                    if (tcd.Latitude != 0.0 && tcd.Longitude != 0.0)
                    {

                        datingentities.SaveChanges();

                        UserRepairLogging logger = new UserRepairLogging();
                       // LoggingLibrary.ServiceReference2.UserRepairLog log = new LoggingLibrary.ServiceReference2.UserRepairLog();
                        logger.oLogEntry.CountryName = countryname;
                        logger.oLogEntry.ProfileID = tcd.ProfileID;
                        logger.oLogEntry.DateFixed = DateTime.Now;
                        logger.oLogEntry.RepairReason = "Fixed users with empty lat long";
                        logger.WriteSingleEntry(logger.oLogEntry);
                    }

                    Console.WriteLine();
                    Console.WriteLine("TestCase/name:  = " + "Geodata fix");
                    Console.WriteLine("UserName    = " + tcd.ProfileID);
                    // Console.WriteLine("Password    = " + tcd.Password);
                    Console.WriteLine("Country =" + countryname );
                    Console.WriteLine("This Script Updated the following values :");
                    Console.WriteLine("Lattutude : {0}",tcd.Latitude);
                    Console.WriteLine("LongiTude : {0}",tcd.Longitude);
                    Console.WriteLine("State Province :{0}",tcd.State_Province);

                    Console.WriteLine();
                }
                catch (Exception  ex)
                {
                    Console.WriteLine("The service operation timed out. " +ex.Message);
                    Console.ReadLine();
                    //     Client.Abort()
                    //these are expected errors here i.e handled 
                }
               
                


            }

        }

        //=======================================================
        //Service provided by Telerik (www.telerik.com)
        //Conversion powered by NRefactory.
        //Twitter: @telerik, @toddanglin
        //Facebook: facebook.com/telerik
        //=======================================================


    }
}
