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

using Misc.PhotoService;

namespace Misc
{
   public static class MisFunctions
    {

          //synch up anew luv database with the new database 
        //add the old database model
        //once this is tested and working we want to move this code into migrations ins Domain.Entities
       public static void StartDebuggingTest()
       {
           var olddb = new AnewluvFtsEntities();
           var postaldb = new PostalData2Entities();
           var context = new AnewluvContext();


           Console.WriteLine("The debugger is ready");
           Console.WriteLine("Press <Enter> to stop the debugging");
           Console.ReadLine();

       }

        //synch up anew luv database with the new database 
        //add the old database model
        //once this is tested and working we want to move this code into migrations ins Domain.Entities
        public static void ConvertFlatProfileandprofiledata()
       {
           var olddb = new AnewluvFtsEntities();
            var postaldb = new PostalData2Entities();
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
             var myprofile = new Shell.MVC2.Domain.Entities.Anewluv.profile();
             var myprofiledata = new Shell.MVC2.Domain.Entities.Anewluv.profiledata ();
             //build the related  profilemetadata noew
             var myprofilemetadata = new Shell.MVC2.Domain.Entities.Anewluv.profilemetadata();

             try
             {
                 Console.WriteLine("attempting to assign old profile with email  :" + item.ProfileID + "to the new database with id: " + newprofileid );

                 myprofile.id = newprofileid;
                 myprofile.username = item.UserName;
                 myprofile.emailaddress = item.ProfileID;
                 myprofile.screenname = item.ScreenName;
                 myprofile.activationcode = item.ActivationCode;
                 myprofile.dailsentmessagequota = item.DailSentMessageQuota;
                 myprofile.dailysentemailquota = item.DailySentEmailQuota;
                 myprofile.forwardmessages = item.ForwardMessages;
                 myprofile.logindate = item.LoginDate;
                 myprofile.modificationdate = item.ModificationDate;
                 myprofile.creationdate = item.CreationDate;
                 myprofile.failedpasswordchangedate = null;
                 myprofile.passwordChangeddate = item.PasswordChangedDate;
                 myprofile.readprivacystatement = item.ReadPrivacyStatement;
                 myprofile.readtemsofuse = item.ReadTemsOfUse;
                 myprofile.password = item.Password;
                 myprofile.passwordchangecount = item.PasswordChangedCount;
                 myprofile.failedpasswordchangeattemptcount = item.PasswordChangeAttempts;
                 myprofile.salt = item.salt;
                 myprofile.status = context.lu_profilestatus.Where(z => z.id == item.ProfileStatusID).FirstOrDefault();
                 myprofile.securityquestion = context.lu_securityquestion.Where(z => z.id == item.SecurityQuestionID).FirstOrDefault();
                 myprofile.securityanswer = item.SecurityAnswer;
                 myprofile.sentemailquotahitcount = item.SentEmailQuotaHitCount;
                 myprofile.sentmessagequotahitcount = item.SentMessageQuotaHitCount;


                 context.profiles.AddOrUpdate(myprofile);
                 context.SaveChanges();
                 var newprofilecreated = context.profiles.Where(p => p.emailaddress == item.ProfileID).First();


                 //query the profile data
                 var matchedprofiledata = olddb.ProfileDatas.Where(p => p.ProfileID == item.ProfileID);
                 // Metadata classes are not meant to be instantiated.
                 myprofiledata.profile_id = newprofilecreated.id; //newprofileid;
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
                //add in lookup feilds 
                    //lookups for personal profile details 
                   myprofiledata.gender  = context.lu_gender.Where(p => p.id   == item.ProfileData.GenderID).FirstOrDefault();  
                 myprofiledata.bodytype=  context.lu_bodytype.Where(p => p.id   == item.ProfileData.BodyTypeID).FirstOrDefault(); 
                  myprofiledata.eyecolor = context.lu_eyecolor .Where(p => p.id   == item.ProfileData.EyeColorID ).FirstOrDefault(); 
                   myprofiledata.haircolor=  context.lu_haircolor.Where(p => p.id   == item.ProfileData.HairColorID ).FirstOrDefault(); 
                  myprofiledata.diet = context.lu_diet .Where(p => p.id   == item.ProfileData.DietID ).FirstOrDefault(); 
                  myprofiledata.drinking = context.lu_drinks.Where(p => p.id   == item.ProfileData.DrinksID ).FirstOrDefault(); 
                 myprofiledata.exercise = context.lu_exercise.Where(p => p.id   == item.ProfileData.ExerciseID).FirstOrDefault(); 
                  myprofiledata.humor = context.lu_humor.Where(p => p.id   == item.ProfileData.HumorID).FirstOrDefault(); 
                   myprofiledata.politicalview = context.lu_politicalview.Where(p => p.id   == item.ProfileData.PoliticalViewID).FirstOrDefault(); 
                   myprofiledata.religion = context.lu_religion.Where(p => p.id   == item.ProfileData.ReligionID ).FirstOrDefault(); 
                  myprofiledata.religiousattendance = context.lu_religiousattendance.Where(p => p.id   == item.ProfileData.ReligiousAttendanceID ).FirstOrDefault(); 
                   myprofiledata.sign = context.lu_sign.Where(p => p.id   == item.ProfileData.SignID).FirstOrDefault(); 
                   myprofiledata.smoking=  context.lu_smokes .Where(p => p.id   == item.ProfileData.SmokesID ).FirstOrDefault(); 
                   myprofiledata.educationlevel=  context.lu_educationlevel.Where(p => p.id   == item.ProfileData.EducationLevelID ).FirstOrDefault(); 
                 myprofiledata. employmentstatus = context.lu_employmentstatus.Where(p => p.id   == item.ProfileData.EmploymentSatusID).FirstOrDefault(); 
                  myprofiledata.kidstatus = context.lu_havekids.Where(p => p.id   == item.ProfileData.HaveKidsId).FirstOrDefault(); 
                  myprofiledata. incomelevel=  context.lu_incomelevel.Where(p => p.id   == item.ProfileData.IncomeLevelID ).FirstOrDefault(); 
                  myprofiledata. livingsituation=  context.lu_livingsituation .Where(p => p.id   == item.ProfileData.LivingSituationID).FirstOrDefault(); 
                  myprofiledata.maritalstatus=  context.lu_maritalstatus.Where(p => p.id   == item.ProfileData.MaritalStatusID).FirstOrDefault(); 
                   myprofiledata.profession=  context.lu_profession.Where(p => p.id   == item.ProfileData.ProfessionID ).FirstOrDefault(); 
                  myprofiledata. wantsKidstatus=  context.lu_wantskids.Where(p => p.id   == item.ProfileData.WantsKidsID).FirstOrDefault(); 
                 //visiblity settings was never implemented anyways.
             // myprofiledata.visibilitysettings=  context.visibilitysettings.Where(p => p.id   == item.Prof).FirstOrDefault();     



                  myprofilemetadata.profile_id = newprofilecreated.id;
                  
                  //add the two new objects to profile
                  //********************************
                  myprofile.profiledata = myprofiledata;
                  myprofile.profilemetadata = myprofilemetadata;

                  context.profiles.AddOrUpdate (myprofile);
               
                 //iccrement new ID
                 newprofileid = newprofileid +1;
             }


            
             
             catch ( Exception ex)
                {

                    var dd = ex.ToString();
                }

           
            }


            //attempt bulk save
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                var dd = ex.ToString();
            }
        }

        public static void ConvertProfileCollections()
        {
          var olddb = new AnewluvFtsEntities();
            var postaldb = new PostalData2Entities();
           var context = new AnewluvContext();


            //global try for the rest of objects that are tied to profile
           try
           {

               //populate collections tied to profile and profiledata


               //build  members in role data if it exists
               foreach (Dating.Server.Data.Models.MembersInRole membersinroleitem in olddb.MembersInRoles)
               {
                   var membersinroleobject = new Shell.MVC2.Domain.Entities.Anewluv.membersinrole();

                   Console.WriteLine("attempting to assign a roleld for old profileid of    :" + membersinroleitem.ProfileID);
                   //query the profile data
                   //var matchedprofile = context.profiles.Where(p => p.emailaddress == membersinroleitem.ProfileID).FirstOrDefault();
                   // Metadata classes are not meant to be instantiated.
                   // myprofile.id = matchedprofile.First().id ;
                   var matchedprofile = context.profiles.Where(p => p.emailaddress == membersinroleitem.ProfileID).FirstOrDefault();
                      if (matchedprofile != null)
                      {


                          membersinroleobject.active = true;
                          //membersinroleobject.profile_id = matchedprofile.id;
                          membersinroleobject.role = context.lu_role.Where(z => z.id == membersinroleitem.Role.RoleID).FirstOrDefault();
                          membersinroleobject.roleexpiredate = null;
                          membersinroleobject.rolestartdate = DateTime.Now;
                          //add the related
                          membersinroleobject.profile = matchedprofile; //context.profiles.Where(p => p.emailaddress == membersinroleitem.ProfileID).FirstOrDefault();

                          //add the object to profile object
                          context.membersinroles.Add(membersinroleobject);

                          Console.WriteLine( "role added for old profileid of   :" + membersinroleitem.ProfileID);
                          //save data one per row
                      }

               }



               //build  activitylog if it exists
               foreach (Dating.Server.Data.Models.ProfileGeoDataLogger activitylogitem in olddb.ProfileGeoDataLoggers)
               {
                   var activitylogobject = new Shell.MVC2.Domain.Entities.Anewluv.profileactivity();

                   Console.WriteLine("attempting to assign a geo activity log for old profileid of    :" + activitylogitem.ProfileID);
                   // Metadata classes are not meant to be instantiated.
                   // myprofile.id = matchedprofile.First().id ;
                   var matchedprofile = context.profiles.Where(p => p.emailaddress == activitylogitem.ProfileID).FirstOrDefault();
                  if (matchedprofile != null)
                  {

                      activitylogobject.creationdate = activitylogitem.CreationDate;
                      activitylogobject.ipaddress = activitylogitem.IPaddress;
                      activitylogobject.actionname = "";
                      activitylogobject.sessionid = activitylogitem.SessionID;
                      activitylogobject.useragent = activitylogitem.UserAgent;
                      activitylogobject.routeurl = ""; //new
                      activitylogobject.useragent = activitylogitem.UserAgent;
                      //add the activity type  as pulled from old database since we did not have those then
                      activitylogobject.activitytype = context.lu_activitytype.Where(p => p.id == (int)activitytypeEnum.fromolddatabasestructure).FirstOrDefault();
                      //add the profile
                      activitylogobject.profile = matchedprofile; //context.profiles.Where(p => p.emailaddress == activitylogitem.ProfileID).FirstOrDefault();

                      //build related geodata  object
                      var myprofileactivitygeodata = new Shell.MVC2.Domain.Entities.Anewluv.profileactivitygeodata();

                      myprofileactivitygeodata.city = activitylogitem.City;
                      myprofileactivitygeodata.regionname = activitylogitem.RegionName;
                      myprofileactivitygeodata.continent = activitylogitem.Continent;
                      myprofileactivitygeodata.countryId = postaldb.CountryCodes.Where(p => p.CountryName == activitylogitem.CountryName).FirstOrDefault().CountryID;
                      myprofileactivitygeodata.countryname = activitylogitem.CountryName;
                      myprofileactivitygeodata.creationdate = activitylogitem.CreationDate;
                      myprofileactivitygeodata.lattitude = activitylogitem.Lattitude;
                      myprofileactivitygeodata.longitude = activitylogitem.Longitude;

                      //add the geodata value for this object to activitylog
                      activitylogobject.profileactivitygeodata = myprofileactivitygeodata;

                      //add the object to profile object
                      context.profileactivity.Add(activitylogobject);
                      //save data one per row
                      Console.WriteLine("geo activity log value added for old profileid of   :" + activitylogitem.ProfileID);
                  }
               }


               //add openID data

               foreach (Dating.Server.Data.Models.profileOpenIDStore openiditem in olddb.profileOpenIDStores)
               {
                   var openidobject = new Shell.MVC2.Domain.Entities.Anewluv.openid();
                   Console.WriteLine("attempting to assign  a profile open id store value  for old profileid of    :" + openiditem.ProfileID);
                   //query the profile data
                   var matchedprofile = context.profiles.Where(p => p.emailaddress == openiditem.ProfileID).FirstOrDefault();
                    if (matchedprofile != null)
                    {
                        openidobject.active = true;
                        //openidobject.profile_id = matchedprofile.id;
                        openidobject.creationdate = openiditem.creationDate;
                        openidobject.openididentifier = openiditem.openidIdentifier;
                        openidobject.openidprovidername = openiditem.openidProviderName;
                        //add the related
                        openidobject.profile = matchedprofile;
                        //add the object to profile object
                        context.opendIds.Add(openidobject);
                        //save data one per row

                        Console.WriteLine(" profile open id store added for old profileid of   :" + openiditem.ProfileID);
                    }

               }


               //add userlogtime

               foreach (Dating.Server.Data.Models.User_Logtime userlogtimeitem in olddb.User_Logtime)
               {
                   var userlogtimeobject = new Shell.MVC2.Domain.Entities.Anewluv.userlogtime();
                   Console.WriteLine("attempting assign a user logtime for old profileid of    :" + userlogtimeitem.ProfileID);
                   //query the profile data
                   var matchedprofile = context.profiles.Where(p => p.emailaddress == userlogtimeitem.ProfileID).FirstOrDefault();
                   if (matchedprofile != null)
                   {
                       // Metadata classes are not meant to be instantiated.
                       // myprofile.id = matchedprofile.First().id ;
                       userlogtimeobject.logintime = userlogtimeitem.LoginTime;
                       userlogtimeobject.logouttime = userlogtimeitem.LogoutTime;
                       userlogtimeobject.offline = Convert.ToBoolean(userlogtimeitem.Offline);
                       userlogtimeobject.sessionid = userlogtimeitem.SessionID;
                       //add the related


                       userlogtimeobject.profile_id = matchedprofile.id;
                       //add the object to profile object
                       context.userlogtimes.Add(userlogtimeobject);
                       //save data one per row
                       Console.WriteLine("user logtime added for old profileid of    :" + userlogtimeitem.ProfileID);
                   }

               }

           }
           catch (Exception ex)
           {

               var dd = ex.ToString();
           }

           context.SaveChanges();

       }

        public static void ConvertProfileMetaDataBasicCollections()
        {
            var olddb = new AnewluvFtsEntities();
            var postaldb = new PostalData2Entities();
            var context = new AnewluvContext();


            //global try for the rest of objects that are tied to profile
            try
            {

                //populate collections tied to profilemetadata


                //handle favorites
                foreach (Dating.Server.Data.Models.favorite  favoritesitem in olddb.favorites)
                {
                    var favoritesobject = new Shell.MVC2.Domain.Entities.Anewluv.favorite();
                    Console.WriteLine("attempting to assign friend request for the old profileid of    :" + favoritesitem.ProfileID);
                   //add the realted proflemetadatas 
                    var matchedprofilemetatdata = context.profilemetadata.Where(p => p.profile.emailaddress  == favoritesitem.ProfileID).FirstOrDefault();
                    var matchedfavoriteprofilemetadata = context.profilemetadata.Where(p => p.profile.emailaddress == favoritesitem.FavoriteID ).FirstOrDefault();
                    if (matchedprofilemetatdata != null && matchedfavoriteprofilemetadata != null)
                     {
                         favoritesobject.profilemetadata = matchedprofilemetatdata; //context.profilemetadata.Where(p => p.profile.emailaddress == favoritesitem.ProfileID).FirstOrDefault();
                         favoritesobject.favoriteprofilemetadata = matchedfavoriteprofilemetadata; //context.profilemetadata.Where(p => p.profile.emailaddress == favoritesitem.FavoriteID).FirstOrDefault();

                         favoritesobject.creationdate = favoritesitem.FavoriteDate.GetValueOrDefault();
                         favoritesobject.modificationdate = null;
                         favoritesobject.viewdate = favoritesitem.FavoriteViewedDate;
                         favoritesobject.deletedbymemberdate = null;
                         favoritesobject.deletedbyfavoritedate = null;

                         //add the object to profile object
                         context.favorites.AddOrUpdate(favoritesobject);
                         //save data one per row
                         context.SaveChanges();
                         Console.WriteLine("friend request added for old profileid of    :" + favoritesitem.ProfileID);
                     }
                }



                //handle friends
                foreach (Dating.Server.Data.Models.Friend  friendsitem in olddb.Friends )
                {
                    var friendsobject = new Shell.MVC2.Domain.Entities.Anewluv.friend();
                    Console.WriteLine("attempting to assign friend request for the old profileid of    :" + friendsitem.ProfileID);
                    //add the realted proflemetadatas 
                    var matchedprofilemetatdata = context.profilemetadata.Where(p => p.profile.emailaddress == friendsitem.ProfileID).FirstOrDefault();
                    var matchedfriendprofilemetadata = context.profilemetadata.Where(p => p.profile.emailaddress == friendsitem.FriendID).FirstOrDefault();
                    if (matchedprofilemetatdata != null && matchedfriendprofilemetadata != null)
                    {
                        friendsobject.profilemetadata = matchedprofilemetatdata; //context.profilemetadata.Where(p => p.profile.emailaddress == friendsitem.ProfileID).FirstOrDefault();
                        friendsobject.friendprofilemetadata = matchedfriendprofilemetadata; //context.profilemetadata.Where(p => p.profile.emailaddress == friendsitem.friendID).FirstOrDefault();

                        friendsobject.creationdate = friendsitem.FriendDate.GetValueOrDefault();
                        friendsobject.modificationdate = null;
                        friendsobject.viewdate = friendsitem.FriendViewedDate.GetValueOrDefault();
                        friendsobject.deletedbymemberdate = null;
                        friendsobject.deletedbyfrienddate = null;

                        //add the object to profile object
                        context.friends.AddOrUpdate(friendsobject);
                        //save data one per row
                        context.SaveChanges();
                        Console.WriteLine("friend request added for old profileid of    :" + friendsitem.ProfileID);
                    }
                }

                //handle interests
                foreach (Dating.Server.Data.Models.Interest Interestsitem in olddb.Interests)
                {
                    var Interestsobject = new Shell.MVC2.Domain.Entities.Anewluv.interest();
                    Console.WriteLine("attempting to assign Interest request for the old profileid of    :" + Interestsitem.ProfileID);
                    //add the realted proflemetadatas 
                    var matchedprofilemetatdata = context.profilemetadata.Where(p => p.profile.emailaddress == Interestsitem.ProfileID).FirstOrDefault();
                    var matchedInterestprofilemetadata = context.profilemetadata.Where(p => p.profile.emailaddress == Interestsitem.InterestID).FirstOrDefault();
                    if (matchedprofilemetatdata != null && matchedInterestprofilemetadata != null)
                    {
                        Interestsobject.profilemetadata = matchedprofilemetatdata; //context.profilemetadata.Where(p => p.profile.emailaddress == Interestsitem.ProfileID).FirstOrDefault();
                        Interestsobject.interestprofilemetadata  = matchedInterestprofilemetadata; //context.profilemetadata.Where(p => p.profile.emailaddress == Interestsitem.InterestID).FirstOrDefault();

                        Interestsobject.creationdate = Interestsitem.InterestDate.GetValueOrDefault();
                        Interestsobject.modificationdate = null;
                        Interestsobject.viewdate = Interestsitem.IntrestViewedDate.GetValueOrDefault() ;
                        Interestsobject.deletedbymemberdate = null;
                        Interestsobject.deletedbyinterestdate  = null;

                        //add the object to profile object
                        context.interests.AddOrUpdate(Interestsobject);
                        //save data one per row
                       // context.SaveChanges();
                        Console.WriteLine("Interest request added for old profileid of    :" + Interestsitem.ProfileID);
                    }
                }
                context.SaveChanges();


                //handle likes
                foreach (Dating.Server.Data.Models.Like  likesitem in olddb.Likes )
                {
                    var likesobject = new Shell.MVC2.Domain.Entities.Anewluv.like();
                    Console.WriteLine("attempting to assign like request for the old profileid of    :" + likesitem.ProfileID);
                    //add the realted proflemetadatas 
                    var matchedprofilemetatdata = context.profilemetadata.Where(p => p.profile.emailaddress == likesitem.ProfileID).FirstOrDefault();
                    var matchedlikeprofilemetadata = context.profilemetadata.Where(p => p.profile.emailaddress == likesitem.LikeID ).FirstOrDefault();
                    if (matchedprofilemetatdata != null && matchedlikeprofilemetadata != null)
                    {
                        likesobject.profilemetadata = matchedprofilemetatdata; //context.profilemetadata.Where(p => p.profile.emailaddress == likesitem.ProfileID).FirstOrDefault();
                        likesobject.likeprofilemetadata = matchedlikeprofilemetadata; //context.profilemetadata.Where(p => p.profile.emailaddress == likesitem.likeID).FirstOrDefault();

                        likesobject.creationdate = likesitem.LikeDate .GetValueOrDefault();
                        likesobject.modificationdate = null;
                        likesobject.deletedbylikedate = likesitem.DeletedByLikeIDDate.GetValueOrDefault();
                        likesobject.deletedbymemberdate = likesitem.DeletedByProfileIDDate.GetValueOrDefault();
                        likesobject.viewdate = likesitem.LikeViewedDate.GetValueOrDefault();                      
                       
                        likesobject.deletedbymemberdate = null;
                        likesobject.deletedbylikedate = null;

                        //add the object to profile object
                        context.likes.AddOrUpdate(likesobject);
                        //save data one per row
                    
                        Console.WriteLine("like request added for old profileid of    :" + likesitem.ProfileID);
                    }
                }
                context.SaveChanges();

                //handle peeks
                //Peeks is invierse with profileviewer being the opposit of profile
                foreach (Dating.Server.Data.Models.ProfileView  peeksitem in olddb.ProfileViews )
                {
                    var peeksobject = new Shell.MVC2.Domain.Entities.Anewluv.peek();
                    Console.WriteLine("attempting to assign peek request for the old profileid of    :" + peeksitem.ProfileID);
                    //add the realted proflemetadatas 
                    var matchedprofilemetatdata = context.profilemetadata.Where(p => p.profile.emailaddress == peeksitem.ProfileViewerID ).FirstOrDefault();
                    var matchedpeekprofilemetadata = context.profilemetadata.Where(p => p.profile.emailaddress == peeksitem.ProfileID    ).FirstOrDefault();
                    if (matchedprofilemetatdata != null && matchedpeekprofilemetadata != null)
                    {
                        peeksobject.profilemetadata = matchedprofilemetatdata; //context.profilemetadata.Where(p => p.profile.emailaddress == peeksitem.ProfileID).FirstOrDefault();
                        peeksobject.peekprofilemetadata = matchedpeekprofilemetadata; //context.profilemetadata.Where(p => p.profile.emailaddress == peeksitem.peekID).FirstOrDefault();

                        peeksobject.creationdate = peeksitem.ProfileViewDate .GetValueOrDefault();
                        peeksobject.modificationdate = null;
                        peeksobject.viewdate = peeksitem.ProfileViewViewedDate.GetValueOrDefault() ;
                        peeksobject.deletedbymemberdate = null;
                        peeksobject.deletedbypeekdate = null;

                        //add the object to profile object
                        context.peeks.AddOrUpdate(peeksobject);
                        //save data one per row
                     //   context.SaveChanges();
                        Console.WriteLine("peek request added for old profileid of    :" + peeksitem.ProfileID);
                    }
                }


                //handle hotlists
                foreach (Dating.Server.Data.Models.Hotlist  hotlistsitem in olddb.Hotlists )
                {
                    var hotlistsobject = new Shell.MVC2.Domain.Entities.Anewluv.hotlist();
                    Console.WriteLine("attempting to assign hotlist request for the old profileid of    :" + hotlistsitem.ProfileID);
                    //add the realted proflemetadatas 
                    var matchedprofilemetatdata = context.profilemetadata.Where(p => p.profile.emailaddress == hotlistsitem.ProfileID).FirstOrDefault();
                    var matchedhotlistprofilemetadata = context.profilemetadata.Where(p => p.profile.emailaddress == hotlistsitem.HotlistID  ).FirstOrDefault();
                    if (matchedprofilemetatdata != null && matchedhotlistprofilemetadata != null)
                    {
                        hotlistsobject.profilemetadata = matchedprofilemetatdata; //context.profilemetadata.Where(p => p.profile.emailaddress == hotlistsitem.ProfileID).FirstOrDefault();
                        hotlistsobject.hotlistprofilemetadata = matchedhotlistprofilemetadata; //context.profilemetadata.Where(p => p.profile.emailaddress == hotlistsitem.hotlistID).FirstOrDefault();

                        hotlistsobject.creationdate = hotlistsitem.HotlistDate .GetValueOrDefault();
                        hotlistsobject.modificationdate = null;
                        hotlistsobject.viewdate = hotlistsitem.HotlistViewedDate.GetValueOrDefault() ;
                        hotlistsobject.deletedbymemberdate = null;
                        hotlistsobject.deletedbyhotlistdate = null;

                        //add the object to profile object
                        context.hotlists.AddOrUpdate(hotlistsobject);
                        //save data one per row
                     //   context.SaveChanges();
                        Console.WriteLine("hotlist request added for old profileid of    :" + hotlistsitem.ProfileID);
                    }
                }


                //custom work for blocks which was mailbox blocks
                //no block notes for now since thye are optional
                foreach (Dating.Server.Data.Models.Mailboxblock blocksitem in olddb.Mailboxblocks)
                {
                    var blocksobject = new Shell.MVC2.Domain.Entities.Anewluv.block();

                    Console.WriteLine("attempting to add a block for the old profileid of    :" + blocksitem.ProfileID);          
                    var matchedprofilemetatdata = context.profilemetadata.Where(p => p.profile.emailaddress == blocksitem.ProfileID).FirstOrDefault();
                    var matchedInterestprofilemetadata = context.profilemetadata.Where(p => p.profile.emailaddress == blocksitem.BlockID ).FirstOrDefault();
                    if (matchedprofilemetatdata != null && matchedInterestprofilemetadata != null)
                    {
                        //add the realted proflemetadatas 
                        blocksobject.profilemetadata = matchedprofilemetatdata; //context.profilemetadata.Where(p => p.profile.emailaddress == blocksitem.ProfileID).FirstOrDefault();
                        blocksobject.blockedprofilemetadata = matchedInterestprofilemetadata; //context.profilemetadata.Where(p => p.profile.emailaddress == blocksitem.BlockID).FirstOrDefault();

                        blocksobject.creationdate = blocksitem.MailboxBlockDate.GetValueOrDefault();
                        blocksobject.modificationdate = null; ;
                        blocksobject.removedate = blocksitem.BlockRemovedDate.GetValueOrDefault();
                        //No need to do anyting with notes since we had no notes in the the past

                        //add the object to profile object
                        context.blocks.Add(blocksobject);
                        Console.WriteLine("block  added for old profileid of    :" + blocksitem.ProfileID);
                        //save data one per row
                    }
                }

                //now handle the collections for other profiledatavalues
                 
                
                //handle ProfileData_Ethnicity
                foreach (Dating.Server.Data.Models.ProfileData_Ethnicity profiledataethnicityitem in olddb.ProfileData_Ethnicity  )
                {
                    var profiledataethnicityobject = new Shell.MVC2.Domain.Entities.Anewluv.profiledata_ethnicity();
                    //add the realted proflemetadatas 
                    Console.WriteLine("attempting assign ethnicity for old profileid of    :" + profiledataethnicityitem.ProfileID);
                   //query the profile data
                    var matchedprofilemetatdata = context.profilemetadata.Where(p => p.profile.emailaddress  == profiledataethnicityitem.ProfileID).FirstOrDefault();
                    if (matchedprofilemetatdata != null)
                   {
                       profiledataethnicityobject.profilemetadata = matchedprofilemetatdata;
                        //context.profilemetadata.Where(p => p.profile.emailaddress == profiledataethnicityitem.ProfileID).FirstOrDefault();
                       profiledataethnicityobject.ethnicty = context.lu_ethnicity.Where(p => p.id == profiledataethnicityitem.EthnicityID ).FirstOrDefault();

                       //add the object to profile object
                       context.ethnicities.Add(profiledataethnicityobject);
                       //save data one per row
                       //context.SaveChanges();
                       Console.WriteLine("ethnicity added for old profileid of    :" + profiledataethnicityitem.ProfileID);
                   }
                }

                   //handle ProfileData_hobby
                foreach (Dating.Server.Data.Models.ProfileData_Hobby profiledatahobbyitem in olddb.ProfileData_Hobby)
                {
                    var profiledatahobbyobject = new Shell.MVC2.Domain.Entities.Anewluv.profiledata_hobby();
                    //add the realted proflemetadatas 
                    Console.WriteLine("attempting assign hobby for old profileid of    :" + profiledatahobbyitem.ProfileID);
                    //query the profile data
                    var matchedprofilemetatdata = context.profilemetadata.Where(p => p.profile.emailaddress == profiledatahobbyitem.ProfileID).FirstOrDefault();
                    if (matchedprofilemetatdata != null)
                    {
                        profiledatahobbyobject.profilemetadata = matchedprofilemetatdata;
                        //context.profilemetadata.Where(p => p.profile.emailaddress == profiledatahobbyitem.ProfileID).FirstOrDefault();
                        profiledatahobbyobject.hobby  = context.lu_hobby.Where(p => p.id == profiledatahobbyitem.HobbyID ).FirstOrDefault();

                        //add the object to profile object
                        context.hobbies.Add(profiledatahobbyobject);
                        //save data one per row
                        //context.SaveChanges();
                        Console.WriteLine("hobby added for old profileid of    :" + profiledatahobbyitem.ProfileID);
                    }
                }

                       
                //handle ProfileData_hotfeature
                foreach (Dating.Server.Data.Models.ProfileData_HotFeature profiledatahotfeatureitem in olddb.ProfileData_HotFeature)
                {
                    var profiledatahotfeatureobject = new Shell.MVC2.Domain.Entities.Anewluv.profiledata_hotfeature();
                    //add the realted proflemetadatas 
                    Console.WriteLine("attempting assign hotfeature for old profileid of    :" + profiledatahotfeatureitem.ProfileID);
                    //query the profile data
                    var matchedprofilemetatdata = context.profilemetadata.Where(p => p.profile.emailaddress == profiledatahotfeatureitem.ProfileID).FirstOrDefault();
                    if (matchedprofilemetatdata != null)
                    {
                        profiledatahotfeatureobject.profilemetadata = matchedprofilemetatdata;
                        //context.profilemetadata.Where(p => p.profile.emailaddress == profiledatahotfeatureitem.ProfileID).FirstOrDefault();
                        profiledatahotfeatureobject.hotfeature  = context.lu_hotfeature.Where(p => p.id == profiledatahotfeatureitem.HotFeatureID ).FirstOrDefault();

                        //add the object to profile object
                        context.hotfeatures .Add(profiledatahotfeatureobject);
                        //save data one per row
                        //context.SaveChanges();
                        Console.WriteLine("hotfeature added for old profileid of    :" + profiledatahotfeatureitem.ProfileID);
                    }
                }


                    //handle ProfileData_lookingfor
                foreach (Dating.Server.Data.Models.ProfileData_LookingFor profiledatalookingforitem in olddb.ProfileData_LookingFor)
                {
                    var profiledatalookingforobject = new Shell.MVC2.Domain.Entities.Anewluv.profiledata_lookingfor();
                    //add the realted proflemetadatas 
                    Console.WriteLine("attempting assign lookingfor for old profileid of    :" + profiledatalookingforitem.ProfileID);
                    //query the profile data
                    var matchedprofilemetatdata = context.profilemetadata.Where(p => p.profile.emailaddress == profiledatalookingforitem.ProfileID).FirstOrDefault();
                    if (matchedprofilemetatdata != null)
                    {
                        profiledatalookingforobject.profilemetadata = matchedprofilemetatdata;
                        //context.profilemetadata.Where(p => p.profile.emailaddress == profiledatalookingforitem.ProfileID).FirstOrDefault();
                        profiledatalookingforobject.lookingfor  = context.lu_lookingfor.Where(p => p.id == profiledatalookingforitem.LookingForID ).FirstOrDefault();
                        
                        //add the object to profile object
                        context.lookingfor.Add(profiledatalookingforobject);
                        //save data one per row
                        //context.SaveChanges();
                        Console.WriteLine("lookingfor added for old profileid of    :" + profiledatalookingforitem.ProfileID);
                    }
                }

                
                context.SaveChanges();

            }
            catch (Exception ex)
            {

                var dd = ex.ToString();
            }

           
            //context.SaveChanges();

        }

       //we will asl use the photo service to update the photo collections stuff
        public static void ConvertProfileDataMetadataCollectionsPhoto()
        {
            var olddb = new AnewluvFtsEntities();
            var postaldb = new PostalData2Entities();
            var context = new AnewluvContext();
            


            //global try for the rest of objects that are tied to profile
            try
            {
                //create refereerence to photo service
                var PhotoService = new PhotoServiceClient();


               // populate photo object and create all the photo convenversions
                //handle favorites
                foreach (Dating.Server.Data.Models.photo   photositem in olddb.photos )
                {
                    var photosobject = new Shell.MVC2.Domain.Entities.Anewluv.photo();

                    //get the profileID since that was saved first
                    var newprofile = context.profiles.Where(p => p.emailaddress == photositem.ProfileID).FirstOrDefault();

                    if (newprofile != null)
                    {
                        //get the list of all this user's photos , we are not re-adding duplicate photos
                        var alloldphotos = olddb.photos.Where(p=>p.ProfileID == newprofile.emailaddress );
                        //now  before adding check the size and approved status
                       var test = !alloldphotos.Any(z=>z.PhotoSize == photositem.PhotoSize) ;
                        if (!alloldphotos.Any(z=>z.PhotoSize == photositem.PhotoSize ))
                        {

                        PhotoUploadModel uploadmodel = new PhotoUploadModel();
                        uploadmodel.caption = photositem.ImageCaption;
                        uploadmodel.creationdate = photositem.PhotoDate.GetValueOrDefault() ;
                        uploadmodel.image = photositem.ProfileImage ;
                        uploadmodel.imagetype = context.lu_photoimagetype.Where(z => z.description == photositem.ProfileImageType).FirstOrDefault(); 
                        uploadmodel.size = photositem.PhotoSize ;

                            if( photositem.Aproved == "Yes")
                            {
                                  uploadmodel.approvalstatus =context.lu_photoapprovalstatus.Where(z => z.id  == (int)(photoapprovalstatusEnum.Approved)).FirstOrDefault(); 
                            }
                            else if (photositem.Aproved == "No")
                            {
                                uploadmodel.approvalstatus = context.lu_photoapprovalstatus.Where(z => z.id == (int)(photoapprovalstatusEnum.Rejected)).FirstOrDefault();
                            }
                            else
                            {
                                uploadmodel.approvalstatus = context.lu_photoapprovalstatus.Where(z => z.id == (int)(photoapprovalstatusEnum.NotReviewed )).FirstOrDefault();
                            }
                  
                        PhotoService.addsinglephoto(uploadmodel, newprofile.id.ToString());
                        }
                    }
                    else
                    { 
                    
                    //skip this photo since its not tied to any valid profile
                    }
                  
                    

                       

                   //   public bool addsinglephoto(PhotoUploadModel newphoto,int profileid)

                   ////add the realted proflemetadatas 
                   // photosobject.profilemetadata  = context.profilemetadata.Where(p => p.profile.emailaddress == photositem.ProfileID).FirstOrDefault();
                   // photosobject.id = new Guid();
                   // photosobject.creationdate = photositem.PhotoDate.GetValueOrDefault();
                   // photosobject.imagecaption =  photositem.ImageCaption;
                   // photosobject.imagetype = context.lu_photoimagetype.Where(z => z.description == photositem.ProfileImageType).FirstOrDefault(); 



                   // photosobject.modificationdate = null;
                   // photosobject.viewdate = photositem.photoViewedDate ;                    
                   // photosobject.deletedbymemberdate = null;
                   // photosobject.deletedbyphotodate = null; 

                   // //add the object to profile object
                   // context.photos.Add(photosobject);
                   // //save data one per row
                   // context.SaveChanges();
                }




                

            }
            catch (Exception ex)
            {

                var dd = ex.ToString();
            }



        }

        public static void ConvertProfileSearchSettingsCollections()
        {
            var olddb = new AnewluvFtsEntities();
            var postaldb = new PostalData2Entities();
            var context = new AnewluvContext();


            //global try for the rest of objects that are tied to profile
            try
            {

                //populate collections tied to profilemetadata


                //handle searchsetting
                foreach (Dating.Server.Data.Models.SearchSetting  searchsettingitem in olddb.SearchSettings )
                {
                    var searchsettingobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting ();

                    //add the realted proflemetadatas 
                    searchsettingobject.profilemetadata = context.profilemetadata.Where(p => p.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                    searchsettingobject.agemax = searchsettingitem.AgeMax;
                    searchsettingobject.agemin  = searchsettingitem.AgeMin ;
                    searchsettingobject.creationdate  = searchsettingitem.CreationDate ; 
                    searchsettingobject.distancefromme  = searchsettingitem.DistanceFromMe ;
                    searchsettingobject.heightmax  = searchsettingitem.HeightMin ;
                    searchsettingobject.heightmin  = searchsettingitem.HeightMin ;
                    searchsettingobject.lastupdatedate  = searchsettingitem.LastUpdateDate ;
                    searchsettingobject.myperfectmatch  = searchsettingitem.MyPerfectMatch ;
                    searchsettingobject.savedsearch  = searchsettingitem.SavedSearch ;
                    searchsettingobject.searchname  = searchsettingitem.SearchName ;
                    searchsettingobject.searchrank  = searchsettingitem.SearchRank ;
                    searchsettingobject.systemmatch  = searchsettingitem.SystemMatch ;

                    //add the object to profile object
                    context.searchsetting.Add(searchsettingobject);
                    //save data one per row
                    context.SaveChanges();

                    //now populate the rest of the date for each item

                    //body type
                    foreach (Dating.Server.Data.Models.SearchSettings_BodyTypes  searchsettingbodytypeitem in olddb.SearchSettings_BodyTypes )
                    {
                        var searchsettingbodytypeobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_bodytype ();

                        searchsettingbodytypeobject.searchsetting   = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                        searchsettingbodytypeobject.bodytype = context.lu_bodytype.Where(p => p.id == searchsettingbodytypeitem.BodyTypesID).FirstOrDefault();
                        context.searchsetting_bodytype.Add(searchsettingbodytypeobject);
                        //save data one per row
                        context.SaveChanges();
                    }

                    //diet
                    foreach (Dating.Server.Data.Models.SearchSettings_Diet  searchsettingdietitem in olddb.SearchSettings_Diet)
                    {
                        var searchsettingdietobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_diet();

                        searchsettingdietobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                        searchsettingdietobject.diet = context.lu_diet.Where(p => p.id == searchsettingdietitem.DietID ).FirstOrDefault();
                        context.searchsetting_diet.Add(searchsettingdietobject);
                        //save data one per row
                        context.SaveChanges();
                    }

                    //drink
                    foreach (Dating.Server.Data.Models.SearchSettings_Drinks searchsettingdrinkitem in olddb.SearchSettings_Drinks)
                    {
                        var searchsettingdrinkobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_drink();

                        searchsettingdrinkobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                        searchsettingdrinkobject.drink = context.lu_drinks.Where(p => p.id == searchsettingdrinkitem.DrinksID).FirstOrDefault();
                        context.searchsetting_drink.Add(searchsettingdrinkobject);
                        //save data one per row
                        context.SaveChanges();
                    }

                    //educationlevel
                    foreach (Dating.Server.Data.Models.SearchSettings_EducationLevel searchsettingeducationlevelitem in olddb.SearchSettings_EducationLevel)
                    {
                        var searchsettingeducationlevelobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_educationlevel();

                        searchsettingeducationlevelobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                        searchsettingeducationlevelobject.educationlevel = context.lu_educationlevel.Where(p => p.id == searchsettingeducationlevelitem.EducationLevelID ).FirstOrDefault();
                        context.searchsetting_educationlevel.Add(searchsettingeducationlevelobject);
                        //save data one per row
                        context.SaveChanges();
                    }


                    ////employmentstatus
                    //foreach (Dating.Server.Data.Models.SearchSettings_employmentstatuss searchsettingemploymentstatusitem in olddb.SearchSettings_employmentstatuss)
                    //{
                    //    var searchsettingemploymentstatusobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_employmentstatus();

                    //    searchsettingemploymentstatusobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                    //    searchsettingemploymentstatusobject.employmentstatus = context.lu_employmentstatus.Where(p => p.id == searchsettingemploymentstatusitem.employmentstatussID).FirstOrDefault();
                    //    context.searchsetting_employmentstatus.Add(searchsettingemploymentstatusobject);
                    //    //save data one per row
                    //    context.SaveChanges();
                    //}


                    ////ethnicity
                    //foreach (Dating.Server.Data.Models.SearchSettings_ethnicitys searchsettingethnicityitem in olddb.SearchSettings_ethnicitys)
                    //{
                    //    var searchsettingethnicityobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_ethnicity();

                    //    searchsettingethnicityobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                    //    searchsettingethnicityobject.ethnicity = context.lu_ethnicity.Where(p => p.id == searchsettingethnicityitem.ethnicitysID).FirstOrDefault();
                    //    context.searchsetting_ethnicity.Add(searchsettingethnicityobject);
                    //    //save data one per row
                    //    context.SaveChanges();
                    //}


                    ////exercise
                    //foreach (Dating.Server.Data.Models.SearchSettings_exercises searchsettingexerciseitem in olddb.SearchSettings_exercises)
                    //{
                    //    var searchsettingexerciseobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_exercise();

                    //    searchsettingexerciseobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                    //    searchsettingexerciseobject.exercise = context.lu_exercise.Where(p => p.id == searchsettingexerciseitem.exercisesID).FirstOrDefault();
                    //    context.searchsetting_exercise.Add(searchsettingexerciseobject);
                    //    //save data one per row
                    //    context.SaveChanges();
                    //}


                    ////eyecolor
                    //foreach (Dating.Server.Data.Models.SearchSettings_eyecolors searchsettingeyecoloritem in olddb.SearchSettings_eyecolors)
                    //{
                    //    var searchsettingeyecolorobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_eyecolor();

                    //    searchsettingeyecolorobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                    //    searchsettingeyecolorobject.eyecolor = context.lu_eyecolor.Where(p => p.id == searchsettingeyecoloritem.eyecolorsID).FirstOrDefault();
                    //    context.searchsetting_eyecolor.Add(searchsettingeyecolorobject);
                    //    //save data one per row
                    //    context.SaveChanges();
                    //}


                    ////gender
                    //foreach (Dating.Server.Data.Models.SearchSettings_genders searchsettinggenderitem in olddb.SearchSettings_genders)
                    //{
                    //    var searchsettinggenderobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_gender();

                    //    searchsettinggenderobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                    //    searchsettinggenderobject.gender = context.lu_gender.Where(p => p.id == searchsettinggenderitem.gendersID).FirstOrDefault();
                    //    context.searchsetting_gender.Add(searchsettinggenderobject);
                    //    //save data one per row
                    //    context.SaveChanges();
                    //}


                    ////haircolor
                    //foreach (Dating.Server.Data.Models.SearchSettings_haircolors searchsettinghaircoloritem in olddb.SearchSettings_haircolors)
                    //{
                    //    var searchsettinghaircolorobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_haircolor();

                    //    searchsettinghaircolorobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                    //    searchsettinghaircolorobject.haircolor = context.lu_haircolor.Where(p => p.id == searchsettinghaircoloritem.haircolorsID).FirstOrDefault();
                    //    context.searchsetting_haircolor.Add(searchsettinghaircolorobject);
                    //    //save data one per row
                    //    context.SaveChanges();
                    //}


                    ////hotfeature
                    //foreach (Dating.Server.Data.Models.SearchSettings_hotfeatures searchsettinghotfeatureitem in olddb.SearchSettings_hotfeatures)
                    //{
                    //    var searchsettinghotfeatureobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_hotfeature();

                    //    searchsettinghotfeatureobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                    //    searchsettinghotfeatureobject.hotfeature = context.lu_hotfeature.Where(p => p.id == searchsettinghotfeatureitem.hotfeaturesID).FirstOrDefault();
                    //    context.searchsetting_hotfeature.Add(searchsettinghotfeatureobject);
                    //    //save data one per row
                    //    context.SaveChanges();
                    //}


                    ////havekids
                    //foreach (Dating.Server.Data.Models.SearchSettings_havekidss searchsettinghavekidsitem in olddb.SearchSettings_havekidss)
                    //{
                    //    var searchsettinghavekidsobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_havekids();

                    //    searchsettinghavekidsobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                    //    searchsettinghavekidsobject.havekids = context.lu_havekids.Where(p => p.id == searchsettinghavekidsitem.havekidssID).FirstOrDefault();
                    //    context.searchsetting_havekids.Add(searchsettinghavekidsobject);
                    //    //save data one per row
                    //    context.SaveChanges();
                    //}


                    ////hobby
                    //foreach (Dating.Server.Data.Models.SearchSettings_hobbys searchsettinghobbyitem in olddb.SearchSettings_hobbys)
                    //{
                    //    var searchsettinghobbyobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_hobby();

                    //    searchsettinghobbyobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                    //    searchsettinghobbyobject.hobby = context.lu_hobby.Where(p => p.id == searchsettinghobbyitem.hobbysID).FirstOrDefault();
                    //    context.searchsetting_hobby.Add(searchsettinghobbyobject);
                    //    //save data one per row
                    //    context.SaveChanges();
                    //}


                    ////humor
                    //foreach (Dating.Server.Data.Models.SearchSettings_humors searchsettinghumoritem in olddb.SearchSettings_humors)
                    //{
                    //    var searchsettinghumorobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_humor();

                    //    searchsettinghumorobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                    //    searchsettinghumorobject.humor = context.lu_humor.Where(p => p.id == searchsettinghumoritem.humorsID).FirstOrDefault();
                    //    context.searchsetting_humor.Add(searchsettinghumorobject);
                    //    //save data one per row
                    //    context.SaveChanges();
                    //}

                    ////incomelevel
                    //foreach (Dating.Server.Data.Models.SearchSettings_incomelevels searchsettingincomelevelitem in olddb.SearchSettings_incomelevels)
                    //{
                    //    var searchsettingincomelevelobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_incomelevel();

                    //    searchsettingincomelevelobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                    //    searchsettingincomelevelobject.incomelevel = context.lu_incomelevel.Where(p => p.id == searchsettingincomelevelitem.incomelevelsID).FirstOrDefault();
                    //    context.searchsetting_incomelevel.Add(searchsettingincomelevelobject);
                    //    //save data one per row
                    //    context.SaveChanges();
                    //}


                    ////livingstituation
                    //foreach (Dating.Server.Data.Models.SearchSettings_livingstituations searchsettinglivingstituationitem in olddb.SearchSettings_livingstituations)
                    //{
                    //    var searchsettinglivingstituationobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_livingstituation();

                    //    searchsettinglivingstituationobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                    //    searchsettinglivingstituationobject.livingstituation = context.lu_livingstituation.Where(p => p.id == searchsettinglivingstituationitem.livingstituationsID).FirstOrDefault();
                    //    context.searchsetting_livingstituation.Add(searchsettinglivingstituationobject);
                    //    //save data one per row
                    //    context.SaveChanges();
                    //}


                    ////location
                    //foreach (Dating.Server.Data.Models.SearchSettings_locations searchsettinglocationitem in olddb.SearchSettings_locations)
                    //{
                    //    var searchsettinglocationobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_location();

                    //    searchsettinglocationobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                    //    searchsettinglocationobject.location = context.lu_location.Where(p => p.id == searchsettinglocationitem.locationsID).FirstOrDefault();
                    //    context.searchsetting_location.Add(searchsettinglocationobject);
                    //    //save data one per row
                    //    context.SaveChanges();
                    //}


                    ////lookingfor
                    //foreach (Dating.Server.Data.Models.SearchSettings_lookingfors searchsettinglookingforitem in olddb.SearchSettings_lookingfors)
                    //{
                    //    var searchsettinglookingforobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_lookingfor();

                    //    searchsettinglookingforobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                    //    searchsettinglookingforobject.lookingfor = context.lu_lookingfor.Where(p => p.id == searchsettinglookingforitem.lookingforsID).FirstOrDefault();
                    //    context.searchsetting_lookingfor.Add(searchsettinglookingforobject);
                    //    //save data one per row
                    //    context.SaveChanges();
                    //}

                    ////maritalstatus
                    //foreach (Dating.Server.Data.Models.SearchSettings_maritalstatuss searchsettingmaritalstatusitem in olddb.SearchSettings_maritalstatuss)
                    //{
                    //    var searchsettingmaritalstatusobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_maritalstatus();

                    //    searchsettingmaritalstatusobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                    //    searchsettingmaritalstatusobject.maritalstatus = context.lu_maritalstatus.Where(p => p.id == searchsettingmaritalstatusitem.maritalstatussID).FirstOrDefault();
                    //    context.searchsetting_maritalstatus.Add(searchsettingmaritalstatusobject);
                    //    //save data one per row
                    //    context.SaveChanges();
                    //}

                    ////bodytype
                    //foreach (Dating.Server.Data.Models.SearchSettings_BodyTypes searchsettingbodytypeitem in olddb.SearchSettings_BodyTypes)
                    //{
                    //    var searchsettingbodytypeobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_bodytype();

                    //    searchsettingbodytypeobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                    //    searchsettingbodytypeobject.bodytype = context.lu_bodytype.Where(p => p.id == searchsettingbodytypeitem.BodyTypesID).FirstOrDefault();
                    //    context.searchsetting_bodytype.Add(searchsettingbodytypeobject);
                    //    //save data one per row
                    //    context.SaveChanges();
                    //}

                    ////bodytype
                    //foreach (Dating.Server.Data.Models.SearchSettings_BodyTypes searchsettingbodytypeitem in olddb.SearchSettings_BodyTypes)
                    //{
                    //    var searchsettingbodytypeobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_bodytype();

                    //    searchsettingbodytypeobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                    //    searchsettingbodytypeobject.bodytype = context.lu_bodytype.Where(p => p.id == searchsettingbodytypeitem.BodyTypesID).FirstOrDefault();
                    //    context.searchsetting_bodytype.Add(searchsettingbodytypeobject);
                    //    //save data one per row
                    //    context.SaveChanges();
                    //}

                    ////bodytype
                    //foreach (Dating.Server.Data.Models.SearchSettings_BodyTypes searchsettingbodytypeitem in olddb.SearchSettings_BodyTypes)
                    //{
                    //    var searchsettingbodytypeobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_bodytype();

                    //    searchsettingbodytypeobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                    //    searchsettingbodytypeobject.bodytype = context.lu_bodytype.Where(p => p.id == searchsettingbodytypeitem.BodyTypesID).FirstOrDefault();
                    //    context.searchsetting_bodytype.Add(searchsettingbodytypeobject);
                    //    //save data one per row
                    //    context.SaveChanges();
                    //}

                    ////bodytype
                    //foreach (Dating.Server.Data.Models.SearchSettings_BodyTypes searchsettingbodytypeitem in olddb.SearchSettings_BodyTypes)
                    //{
                    //    var searchsettingbodytypeobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_bodytype();

                    //    searchsettingbodytypeobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                    //    searchsettingbodytypeobject.bodytype = context.lu_bodytype.Where(p => p.id == searchsettingbodytypeitem.BodyTypesID).FirstOrDefault();
                    //    context.searchsetting_bodytype.Add(searchsettingbodytypeobject);
                    //    //save data one per row
                    //    context.SaveChanges();
                    //}

                    ////bodytype
                    //foreach (Dating.Server.Data.Models.SearchSettings_BodyTypes searchsettingbodytypeitem in olddb.SearchSettings_BodyTypes)
                    //{
                    //    var searchsettingbodytypeobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_bodytype();

                    //    searchsettingbodytypeobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                    //    searchsettingbodytypeobject.bodytype = context.lu_bodytype.Where(p => p.id == searchsettingbodytypeitem.BodyTypesID).FirstOrDefault();
                    //    context.searchsetting_bodytype.Add(searchsettingbodytypeobject);
                    //    //save data one per row
                    //    context.SaveChanges();
                    //}

                    ////bodytype
                    //foreach (Dating.Server.Data.Models.SearchSettings_BodyTypes searchsettingbodytypeitem in olddb.SearchSettings_BodyTypes)
                    //{
                    //    var searchsettingbodytypeobject = new Shell.MVC2.Domain.Entities.Anewluv.searchsetting_bodytype();

                    //    searchsettingbodytypeobject.searchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();
                    //    searchsettingbodytypeobject.bodytype = context.lu_bodytype.Where(p => p.id == searchsettingbodytypeitem.BodyTypesID).FirstOrDefault();
                    //    context.searchsetting_bodytype.Add(searchsettingbodytypeobject);
                    //    //save data one per row
                    //    context.SaveChanges();
                    //}







                }






            }
            catch (Exception ex)
            {

                var dd = ex.ToString();
            }



        }



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
