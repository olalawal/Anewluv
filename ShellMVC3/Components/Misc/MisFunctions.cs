using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Migrations;

//using DatingModel;


using Dating.Server.Data.Models;
using Shell.MVC2.Domain.Entities;
//using Shell.MVC2.Infrastructure.Entities;
//using LoggingLibrary;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using Shell.MVC2.Domain.Entities.Anewluv;

namespace Misc
{
   public static class MisFunctions
    {


        //synch up anew luv database with the new database 
        //add the old database model
        //once this is tested and working we want to move this code into migrations ins Domain.Entities
        public static void ConvertDatabase()
       {
           var olddb = new AnewluvFtsEntities();
           var context = new AnewluvContext();

            //code for simple type data swap
           //olddb.profiles.ToList().ForEach(p => context.profiles.AddOrUpdate(new Shell.MVC2.Domain.Entities.Anewluv.profile()
           //{
           //})          
           //);



            //convert profileData and profile first 
           //convet abusers data
         
            int newprofileid = 1;
            foreach (Dating.Server.Data.Models.profile item in olddb.profiles )
            {
             var myobject = new Shell.MVC2.Domain.Entities.Anewluv.profile();


             try
             {
                 myobject.id = newprofileid;
                 myobject.username = item.UserName;
                 myobject.emailaddress = item.ProfileID;
                 myobject.screenname = item.ScreenName;
                 myobject.activationcode = item.ActivationCode;
                 myobject.dailsentmessagequota = item.DailSentMessageQuota;
                 myobject.dailysentemailquota = item.DailySentEmailQuota;
                 myobject.forwardmessages = item.ForwardMessages;
                 myobject.logindate = item.LoginDate;
                 myobject.modificationdate = item.ModificationDate;
                 myobject.creationdate = item.CreationDate;
                 myobject.failedpasswordchangedate = null;
                 myobject.passwordChangeddate = item.PasswordChangedDate;
                 myobject.readprivacystatement = item.ReadPrivacyStatement;
                 myobject.readtemsofuse = item.ReadTemsOfUse;
                 myobject.password = item.Password;
                 myobject.passwordchangecount = item.PasswordChangedCount;
                 myobject.failedpasswordchangeattemptcount = item.PasswordChangeAttempts;
                 myobject.salt = item.salt;
                 myobject.status = context.lu_profilestatus.Where(z => z.id == item.ProfileStatusID).FirstOrDefault();
                 myobject.securityquestion = context.lu_securityquestion.Where(z => z.id == item.SecurityQuestionID).FirstOrDefault();
                 myobject.securityanswer = item.SecurityAnswer;
                 myobject.sentemailquotahitcount = item.SentEmailQuotaHitCount;
                 myobject.sentmessagequotahitcount = item.SentMessageQuotaHitCount;
             
                 //build related profiledata object
                 var myprofiledata = new Shell.MVC2.Domain.Entities.Anewluv.profiledata();

                 //query the profile data
                 var matchedprofiledata = olddb.ProfileDatas.Where(p => p.ProfileID == item.ProfileID);
                 // Metadata classes are not meant to be instantiated.
                 myprofiledata.id = newprofileid;
                 myprofiledata.age = matchedprofiledata.FirstOrDefault().Age;
                 myprofiledata.birthdate = matchedprofiledata.FirstOrDefault().Birthdate;
                 myprofiledata.city = matchedprofiledata.FirstOrDefault().City;
                 myprofiledata.countryregion = matchedprofiledata.FirstOrDefault().Country_Region;
                 myprofiledata.stateprovince = matchedprofiledata.FirstOrDefault().State_Province;
                 myprofiledata.countryid = matchedprofiledata.FirstOrDefault().CountryID;
                 myprofiledata.longitude = matchedprofiledata.FirstOrDefault().Longitude;
                 myprofiledata.latitude = matchedprofiledata.FirstOrDefault().Latitude;
                 myprofiledata.aboutme = matchedprofiledata.FirstOrDefault().AboutMe;
                 myprofiledata.height = (long)matchedprofiledata.FirstOrDefault().Height.GetValueOrDefault();
                 myprofiledata.mycatchyintroLine = matchedprofiledata.FirstOrDefault().MyCatchyIntroLine;
                 myprofiledata.phone = matchedprofiledata.FirstOrDefault().Phone;
                 myprofiledata.postalcode = matchedprofiledata.FirstOrDefault().PostalCode;
                 //myprofiledata.profile = context.profiles.Where(z => z.id == myprofiledata.id).FirstOrDefault();
                

                 //do the metadata noew
                 var myprofilemetadata = new Shell.MVC2.Domain.Entities.Anewluv.profilemetadata();
                 myprofilemetadata.id = newprofileid;


                 //add the two new objects to profile
                 myobject.profiledata = myprofiledata;
                 myobject.profilemetadata = myprofilemetadata;

                 context.profiles.Add(myobject);
                 context.SaveChanges();
                 //iccrement new ID
                 newprofileid = +newprofileid;

             }
            catch ( Exception ex)
                {

                    var dd = ex.ToString();
                }

            }


            
           
            foreach (Dating.Server.Data.Models.ProfileData  item in olddb.ProfileDatas  )
            {
             var myobject = new Shell.MVC2.Domain.Entities.Anewluv.profiledata();

                //query the profile data
            var matchedprofile = context.profiles.Where(p => p.emailaddress   == item.ProfileID );
            // Metadata classes are not meant to be instantiated.
             myobject.id = matchedprofile.First().id ;
             myobject.age = item.Age ;
             myobject.birthdate = item.Birthdate ;
             myobject.city = item.City ;
             myobject.countryregion = item.Country_Region ;
             myobject.stateprovince = item.State_Province ;
             myobject.countryid = item.CountryID ;
             myobject.longitude = item.Longitude ;
             myobject.latitude = item.Latitude ;
             myobject.aboutme = item.AboutMe ;
             myobject.height = (long)item.Height ;
             myobject.mycatchyintroLine = item.MyCatchyIntroLine ;
             myobject.phone = item.Phone ;
             myobject.postalcode = item.PostalCode ;
             myobject.profile = context.profiles.Where(z => z.id == myobject.id).FirstOrDefault();
             //
             context.profiledata.Add(myobject);
             //iccrement new ID
             newprofileid = +newprofileid;

            }

           







           //convet abusers data
           //olddb.abusereports.ToList().ForEach(p => context.abusereports.AddOrUpdate (new Shell.MVC2.Domain.Entities.Anewluv.abusereport()
           //{
           //    abuser_id =  p.AbuserID,
           //    abusereporter_id  = p.ProfileID 


           //}));

       }

        //public static void FixBadUserGeoData()
        //{
        //    //var db =  //new Dating.Server.Data.Services.DatingService();
        //    var datingentities = new AnewLuvFTSEntities();
        //    var postaldataentities = new PostalData2Entities();
        //    var postaldb= new PostalDataService();
        //    List<MembersViewModel> membersmodels = new List<MembersViewModel>();
        //    List<profiledata> profiledatas =  new List<profiledata>();
        //    //  Dim testSignatureSample As String = New LargeImageTestdata().LargeImageTest
        //    //get all the profile Data's with errors 

        //    profiledatas = datingentities.ProfileDatas.Where(p => p.Longitude ==0  && p.Latitude == 0 ).ToList();





        //    foreach (profiledata  tcd in profiledatas)
        //    {
               

        //       //get country and city to get lattitude and longitude
        //        var countryname =  postaldb.GetCountryNameByCountryID(tcd.countryid );
        //        int haspostalcodes = postaldb.GetCountry_PostalCodeStatusByCountryName(countryname);
        //        string postalcodegpsdata = (haspostalcodes == 1)? postaldb.GetGeoPostalCodebyCountryNameAndCity(countryname,tcd.city):"";
        //        IQueryable<GpsData> dd = postaldb.GetGpsDataByCountryAndCity(countryname,tcd.city );
                
        //        tcd.postalcode  = postalcodegpsdata;
        //        tcd.latitude  = dd.Count() > 0?  dd.FirstOrDefault().Latitude :0.0;
        //        tcd.longitude  = dd.Count() > 0? dd.FirstOrDefault().Longitude:0.0;
        //        tcd.stateprovince  = dd.Count() > 0 ? dd.FirstOrDefault().State_Province : null; 
                           
                        
               
                    
                 


        //        try
        //        {


        //            if (tcd.Latitude != 0.0 && tcd.Longitude != 0.0)
        //            {

        //                datingentities.SaveChanges();

        //                UserRepairLogging logger = new UserRepairLogging();
        //               // LoggingLibrary.ServiceReference2.UserRepairLog log = new LoggingLibrary.ServiceReference2.UserRepairLog();
        //                logger.oLogEntry.CountryName = countryname;
        //                logger.oLogEntry.ProfileID = tcd.ProfileID;
        //                logger.oLogEntry.DateFixed = DateTime.Now;
        //                logger.oLogEntry.RepairReason = "Fixed users with empty lat long";
        //                logger.WriteSingleEntry(logger.oLogEntry);
        //            }

        //            Console.WriteLine();
        //            Console.WriteLine("TestCase/name:  = " + "Geodata fix");
        //            Console.WriteLine("UserName    = " + tcd.ProfileID);
        //            // Console.WriteLine("Password    = " + tcd.Password);
        //            Console.WriteLine("Country =" + countryname );
        //            Console.WriteLine("This Script Updated the following values :");
        //            Console.WriteLine("Lattutude : {0}",tcd.Latitude);
        //            Console.WriteLine("LongiTude : {0}",tcd.Longitude);
        //            Console.WriteLine("State Province :{0}",tcd.State_Province);

        //            Console.WriteLine();
        //        }
        //        catch (Exception  ex)
        //        {
        //            Console.WriteLine("The service operation timed out. " +ex.Message);
        //            Console.ReadLine();
        //            //     Client.Abort()
        //            //these are expected errors here i.e handled 
        //        }
               
                


        //    }

        //}


     


        //=======================================================
        //Service provided by Telerik (www.telerik.com)
        //Conversion powered by NRefactory.
        //Twitter: @telerik, @toddanglin
        //Facebook: facebook.com/telerik
        //=======================================================


    }
}
