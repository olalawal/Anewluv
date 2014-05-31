using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Migrations;

//using DatingModel;



//using Shell.MVC2.Infrastructure.Entities;
//using LoggingLibrary;


using Shell.MVC2.Services.Contracts;
using System.ServiceModel.Web;
using Dating.Server.Data.Models;
using Anewluv.Domain;
using Anewluv.Domain.Data;
using Anewluv.Domain.Data.ViewModels;

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
            //olddb.profiles.ToList().ForEach(p => context.profiles.AddOrUpdate(new Anewluv.Domain.Data.profile()
            //{
            //})          
            //);



            //convert profileData and profile first 
            //convet abusers data

            int newprofileid = 1;
            foreach (Dating.Server.Data.Models.profile item in olddb.profiles)
            {
                var myprofile = new Anewluv.Domain.Data.profile();
                var myprofiledata = new Anewluv.Domain.Data.profiledata();
                //build the related  profilemetadata noew
                var myprofilemetadata = new Anewluv.Domain.Data.profilemetadata();

                if (context.profiles.Any(p => p.emailaddress == item.ProfileID))
                {
                    Console.WriteLine("skipping profile with email  :" + item.ProfileID +"it alaready exists   ");
                }
                else
                {
                    try
                    {
                        Console.WriteLine("attempting to assign old profile with email  :" + item.ProfileID + "to the new database with id: " + newprofileid);

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
                        myprofiledata.gender = context.lu_gender.Where(p => p.id == item.ProfileData.GenderID).FirstOrDefault();
                        myprofiledata.bodytype = context.lu_bodytype.Where(p => p.id == item.ProfileData.BodyTypeID).FirstOrDefault();
                        myprofiledata.eyecolor = context.lu_eyecolor.Where(p => p.id == item.ProfileData.EyeColorID).FirstOrDefault();
                        myprofiledata.haircolor = context.lu_haircolor.Where(p => p.id == item.ProfileData.HairColorID).FirstOrDefault();
                        myprofiledata.diet = context.lu_diet.Where(p => p.id == item.ProfileData.DietID).FirstOrDefault();
                        myprofiledata.drinking = context.lu_drinks.Where(p => p.id == item.ProfileData.DrinksID).FirstOrDefault();
                        myprofiledata.exercise = context.lu_exercise.Where(p => p.id == item.ProfileData.ExerciseID).FirstOrDefault();
                        myprofiledata.humor = context.lu_humor.Where(p => p.id == item.ProfileData.HumorID).FirstOrDefault();
                        myprofiledata.politicalview = context.lu_politicalview.Where(p => p.id == item.ProfileData.PoliticalViewID).FirstOrDefault();
                        myprofiledata.religion = context.lu_religion.Where(p => p.id == item.ProfileData.ReligionID).FirstOrDefault();
                        myprofiledata.religiousattendance = context.lu_religiousattendance.Where(p => p.id == item.ProfileData.ReligiousAttendanceID).FirstOrDefault();
                        myprofiledata.sign = context.lu_sign.Where(p => p.id == item.ProfileData.SignID).FirstOrDefault();
                        myprofiledata.smoking = context.lu_smokes.Where(p => p.id == item.ProfileData.SmokesID).FirstOrDefault();
                        myprofiledata.educationlevel = context.lu_educationlevel.Where(p => p.id == item.ProfileData.EducationLevelID).FirstOrDefault();
                        myprofiledata.employmentstatus = context.lu_employmentstatus.Where(p => p.id == item.ProfileData.EmploymentSatusID).FirstOrDefault();
                        myprofiledata.kidstatus = context.lu_havekids.Where(p => p.id == item.ProfileData.HaveKidsId).FirstOrDefault();
                        myprofiledata.incomelevel = context.lu_incomelevel.Where(p => p.id == item.ProfileData.IncomeLevelID).FirstOrDefault();
                        myprofiledata.livingsituation = context.lu_livingsituation.Where(p => p.id == item.ProfileData.LivingSituationID).FirstOrDefault();
                        myprofiledata.maritalstatus = context.lu_maritalstatus.Where(p => p.id == item.ProfileData.MaritalStatusID).FirstOrDefault();
                        myprofiledata.profession = context.lu_profession.Where(p => p.id == item.ProfileData.ProfessionID).FirstOrDefault();
                        myprofiledata.wantsKidstatus = context.lu_wantskids.Where(p => p.id == item.ProfileData.WantsKidsID).FirstOrDefault();
                        //visiblity settings was never implemented anyways.
                        // myprofiledata.visibilitysettings=  context.visibilitysettings.Where(p => p.id   == item.Prof).FirstOrDefault();     



                        myprofilemetadata.profile_id = newprofilecreated.id;

                        //add the two new objects to profile
                        //********************************
                        myprofile.profiledata = myprofiledata;
                        myprofile.profilemetadata = myprofilemetadata;

                        context.profiles.AddOrUpdate(myprofile);

                        //iccrement new ID
                        newprofileid = newprofileid + 1;
                    }




                    catch (Exception ex)
                    {

                        var dd = ex.ToString();
                    }
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
                    var membersinroleobject = new Anewluv.Domain.Data.membersinrole();

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

                        Console.WriteLine("role added for old profileid of   :" + membersinroleitem.ProfileID);
                        //save data one per row
                    }

                }



                //build  activitylog if it exists
                foreach (Dating.Server.Data.Models.ProfileGeoDataLogger activitylogitem in olddb.ProfileGeoDataLoggers)
                {
                    var activitylogobject = new Anewluv.Domain.Data.profileactivity();

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
                        var myprofileactivitygeodata = new Anewluv.Domain.Data.profileactivitygeodata();

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
                    var openidobject = new Anewluv.Domain.Data.openid();
                    Console.WriteLine("attempting to assign  a profile open id store value  for old profileid of    :" + openiditem.ProfileID);
                    //query the profile data
                    var matchedprofile = context.profiles.Where(p => p.emailaddress == openiditem.ProfileID).FirstOrDefault();
                    if (matchedprofile != null)
                    {
                        openidobject.active = true;
                        //openidobject.profile_id = matchedprofile.id;
                        openidobject.creationdate = openiditem.creationDate;
                        openidobject.openididentifier = openiditem.openidIdentifier;
                        openidobject.openidprovider = context.lu_openidprovider.Where(p => p.description  == openiditem.openidIdentifier).FirstOrDefault(); //DbConte openiditem.openidProviderName;
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
                    var userlogtimeobject = new Anewluv.Domain.Data.userlogtime();
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
                foreach (Dating.Server.Data.Models.favorite favoritesitem in olddb.favorites)
                {
                    var favoritesobject = new Anewluv.Domain.Data.favorite();
                    Console.WriteLine("attempting to add a favorite for the old profileid of    :" + favoritesitem.ProfileID);
                    //add the realted proflemetadatas 
                    var matchedprofilemetatdata = context.profilemetadata.Where(p => p.profile.emailaddress == favoritesitem.ProfileID).FirstOrDefault();
                    var matchedfavoriteprofilemetadata = context.profilemetadata.Where(p => p.profile.emailaddress == favoritesitem.FavoriteID).FirstOrDefault();
                    if (matchedprofilemetatdata != null && matchedfavoriteprofilemetadata != null)
                    {
                        favoritesobject.profilemetadata = matchedprofilemetatdata; //context.profilemetadata.Where(p => p.profile.emailaddress == favoritesitem.ProfileID).FirstOrDefault();
                        favoritesobject.favoriteprofilemetadata = matchedfavoriteprofilemetadata; //context.profilemetadata.Where(p => p.profile.emailaddress == favoritesitem.FavoriteID).FirstOrDefault();

                        favoritesobject.creationdate = (favoritesitem.FavoriteDate != null) ? favoritesitem.FavoriteDate : null;
                        favoritesobject.modificationdate = null;
                        favoritesobject.viewdate = (favoritesitem.FavoriteViewedDate != null) ? favoritesitem.FavoriteViewedDate : null;
                        favoritesobject.deletedbymemberdate = null;
                        favoritesobject.deletedbyfavoritedate = null;

                        //add the object to profile object
                        context.favorites.AddOrUpdate(favoritesobject);
                        //save data one per row
                        // context.SaveChanges();
                        Console.WriteLine("favorite  added for old profileid of    :" + favoritesitem.ProfileID);
                    }
                }

                Console.WriteLine("saving  favorites "); context.SaveChanges();

                //handle friends
                foreach (Dating.Server.Data.Models.Friend friendsitem in olddb.Friends)
                {
                    var friendsobject = new Anewluv.Domain.Data.friend();
                    Console.WriteLine("attempting to assign friend request for the old profileid of    :" + friendsitem.ProfileID);
                    //add the realted proflemetadatas 
                    var matchedprofilemetatdata = context.profilemetadata.Where(p => p.profile.emailaddress == friendsitem.ProfileID).FirstOrDefault();
                    var matchedfriendprofilemetadata = context.profilemetadata.Where(p => p.profile.emailaddress == friendsitem.FriendID).FirstOrDefault();
                    if (matchedprofilemetatdata != null && matchedfriendprofilemetadata != null)
                    {
                        friendsobject.profilemetadata = matchedprofilemetatdata; //context.profilemetadata.Where(p => p.profile.emailaddress == friendsitem.ProfileID).FirstOrDefault();
                        friendsobject.friendprofilemetadata = matchedfriendprofilemetadata; //context.profilemetadata.Where(p => p.profile.emailaddress == friendsitem.friendID).FirstOrDefault();

                        friendsobject.creationdate = (friendsitem.FriendDate != null) ? friendsitem.FriendDate : null;
                        friendsobject.modificationdate = null;
                        friendsobject.viewdate = (friendsitem.FriendViewedDate != null) ? friendsitem.FriendViewedDate : null;
                        friendsobject.deletedbymemberdate = null;
                        friendsobject.deletedbyfrienddate = null;

                        //add the object to profile object
                        context.friends.AddOrUpdate(friendsobject);
                        //save data one per row
                        //  context.SaveChanges();
                        Console.WriteLine("friend request added for old profileid of    :" + friendsitem.ProfileID);
                    }
                }

                Console.WriteLine("saving  friends "); context.SaveChanges();

                //handle interests
                foreach (Dating.Server.Data.Models.Interest Interestsitem in olddb.Interests)
                {
                    var Interestsobject = new Anewluv.Domain.Data.interest();
                    Console.WriteLine("attempting to assign Interest request for the old profileid of    :" + Interestsitem.ProfileID);
                    //add the realted proflemetadatas 
                    var matchedprofilemetatdata = context.profilemetadata.Where(p => p.profile.emailaddress == Interestsitem.ProfileID).FirstOrDefault();
                    var matchedInterestprofilemetadata = context.profilemetadata.Where(p => p.profile.emailaddress == Interestsitem.InterestID).FirstOrDefault();
                    if (matchedprofilemetatdata != null && matchedInterestprofilemetadata != null)
                    {
                        Interestsobject.profilemetadata = matchedprofilemetatdata; //context.profilemetadata.Where(p => p.profile.emailaddress == Interestsitem.ProfileID).FirstOrDefault();
                        Interestsobject.interestprofilemetadata = matchedInterestprofilemetadata; //context.profilemetadata.Where(p => p.profile.emailaddress == Interestsitem.InterestID).FirstOrDefault();
                        Interestsobject.creationdate = (Interestsitem.InterestDate != null) ? Interestsitem.InterestDate : null;
                        Interestsobject.modificationdate = null;
                        Interestsobject.viewdate = (Interestsitem.IntrestViewedDate != null) ? Interestsitem.IntrestViewedDate : null;
                        Interestsobject.deletedbymemberdate = null;
                        Interestsobject.deletedbyinterestdate = null;

                        //add the object to profile object
                        context.interests.AddOrUpdate(Interestsobject);
                        //save data one per row
                        // context.SaveChanges();
                        Console.WriteLine("Interest request added for old profileid of    :" + Interestsitem.ProfileID);

                    }
                }

                Console.WriteLine("saving  Interests "); context.SaveChanges();

                //handle likes
                foreach (Dating.Server.Data.Models.Like likesitem in olddb.Likes)
                {
                    var likesobject = new Anewluv.Domain.Data.like();
                    Console.WriteLine("attempting to assign like request for the old profileid of    :" + likesitem.ProfileID);
                    //add the realted proflemetadatas 
                    var matchedprofilemetatdata = context.profilemetadata.Where(p => p.profile.emailaddress == likesitem.ProfileID).FirstOrDefault();
                    var matchedlikeprofilemetadata = context.profilemetadata.Where(p => p.profile.emailaddress == likesitem.LikeID).FirstOrDefault();
                    if (matchedprofilemetatdata != null && matchedlikeprofilemetadata != null)
                    {
                        likesobject.profilemetadata = matchedprofilemetatdata; //context.profilemetadata.Where(p => p.profile.emailaddress == likesitem.ProfileID).FirstOrDefault();
                        likesobject.likeprofilemetadata = matchedlikeprofilemetadata; //context.profilemetadata.Where(p => p.profile.emailaddress == likesitem.likeID).FirstOrDefault();

                        likesobject.creationdate = (likesitem.LikeDate != null) ? likesitem.LikeDate : null;
                        likesobject.modificationdate = null;
                        likesobject.viewdate = (likesitem.LikeViewedDate != null) ? likesitem.LikeViewedDate : null;
                        likesobject.deletedbymemberdate = null;
                        likesobject.deletedbylikedate = null;

                        //add the object to profile object
                        context.likes.AddOrUpdate(likesobject);
                        //save data one per row
                        //   context.SaveChanges();
                        Console.WriteLine("like request added for old profileid of    :" + likesitem.ProfileID);
                    }
                }
                Console.WriteLine("saving  likes "); context.SaveChanges();

                //handle peeks
                //Peeks is invierse with profileviewer being the opposit of profile
                foreach (Dating.Server.Data.Models.ProfileView peeksitem in olddb.ProfileViews)
                {
                    var peeksobject = new Anewluv.Domain.Data.peek();
                    Console.WriteLine("attempting to assign peek request for the old profileid of    :" + peeksitem.ProfileID);
                    //add the realted proflemetadatas 
                    var matchedprofilemetatdata = context.profilemetadata.Where(p => p.profile.emailaddress == peeksitem.ProfileViewerID).FirstOrDefault();
                    var matchedpeekprofilemetadata = context.profilemetadata.Where(p => p.profile.emailaddress == peeksitem.ProfileID).FirstOrDefault();
                    if (matchedprofilemetatdata != null && matchedpeekprofilemetadata != null)
                    {
                        peeksobject.profilemetadata = matchedprofilemetatdata; //context.profilemetadata.Where(p => p.profile.emailaddress == peeksitem.ProfileID).FirstOrDefault();
                        peeksobject.peekprofilemetadata = matchedpeekprofilemetadata; //context.profilemetadata.Where(p => p.profile.emailaddress == peeksitem.peekID).FirstOrDefault();

                        peeksobject.creationdate = (peeksitem.ProfileViewDate != null) ? peeksitem.ProfileViewDate : null;
                        peeksobject.modificationdate = null;
                        peeksobject.viewdate = (peeksitem.ProfileViewViewedDate != null) ? peeksitem.ProfileViewViewedDate : null;
                        peeksobject.deletedbymemberdate = null;
                        peeksobject.deletedbypeekdate = null;

                        //add the object to profile object
                        context.peeks.AddOrUpdate(peeksobject);
                        //save data one per row
                        //   context.SaveChanges();
                        Console.WriteLine("peek request added for old profileid of    :" + peeksitem.ProfileID);
                    }
                }

                Console.WriteLine("saving  peeks "); context.SaveChanges();


                //handle hotlists
                foreach (Dating.Server.Data.Models.Hotlist hotlistsitem in olddb.Hotlists)
                {
                    var hotlistsobject = new Anewluv.Domain.Data.hotlist();
                    Console.WriteLine("attempting to assign hotlist request for the old profileid of    :" + hotlistsitem.ProfileID);
                    //add the realted proflemetadatas 
                    var matchedprofilemetatdata = context.profilemetadata.Where(p => p.profile.emailaddress == hotlistsitem.ProfileID).FirstOrDefault();
                    var matchedhotlistprofilemetadata = context.profilemetadata.Where(p => p.profile.emailaddress == hotlistsitem.HotlistID).FirstOrDefault();
                    if (matchedprofilemetatdata != null && matchedhotlistprofilemetadata != null)
                    {
                        hotlistsobject.profilemetadata = matchedprofilemetatdata; //context.profilemetadata.Where(p => p.profile.emailaddress == hotlistsitem.ProfileID).FirstOrDefault();
                        hotlistsobject.hotlistprofilemetadata = matchedhotlistprofilemetadata; //context.profilemetadata.Where(p => p.profile.emailaddress == hotlistsitem.hotlistID).FirstOrDefault();

                        hotlistsobject.creationdate = (hotlistsitem.HotlistDate != null) ? hotlistsitem.HotlistDate : null;
                        hotlistsobject.modificationdate = null;
                        hotlistsobject.viewdate = (hotlistsitem.HotlistViewedDate != null) ? hotlistsitem.HotlistViewedDate : null;
                        hotlistsobject.deletedbymemberdate = null;
                        hotlistsobject.deletedbyhotlistdate = null;

                        //add the object to profile object
                        context.hotlists.AddOrUpdate(hotlistsobject);
                        //save data one per row
                        //   context.SaveChanges();
                        Console.WriteLine("hotlist request added for old profileid of    :" + hotlistsitem.ProfileID);
                    }
                }

                Console.WriteLine("saving  hotlists "); context.SaveChanges();

                //custom work for blocks which was mailbox blocks
                //no block notes for now since thye are optional
                foreach (Dating.Server.Data.Models.Mailboxblock blocksitem in olddb.Mailboxblocks)
                {
                    var blocksobject = new Anewluv.Domain.Data.block();

                    Console.WriteLine("attempting to add a block for the old profileid of    :" + blocksitem.ProfileID);
                    var matchedprofilemetatdata = context.profilemetadata.Where(p => p.profile.emailaddress == blocksitem.ProfileID).FirstOrDefault();
                    var matchedInterestprofilemetadata = context.profilemetadata.Where(p => p.profile.emailaddress == blocksitem.BlockID).FirstOrDefault();
                    if (matchedprofilemetatdata != null && matchedInterestprofilemetadata != null)
                    {
                        //add the realted proflemetadatas 
                        blocksobject.profilemetadata = matchedprofilemetatdata; //context.profilemetadata.Where(p => p.profile.emailaddress == blocksitem.ProfileID).FirstOrDefault();
                        blocksobject.blockedprofilemetadata = matchedInterestprofilemetadata; //context.profilemetadata.Where(p => p.profile.emailaddress == blocksitem.BlockID).FirstOrDefault();

                        blocksobject.creationdate = (blocksitem.MailboxBlockDate != null) ? blocksitem.MailboxBlockDate : null;
                        blocksobject.modificationdate = null;
                        blocksobject.removedate = (blocksitem.BlockRemovedDate != null) ? blocksitem.BlockRemovedDate : null;
                        //No need to do anyting with notes since we had no notes in the the past

                        //add the object to profile object
                        context.blocks.Add(blocksobject);
                        Console.WriteLine("block  added for old profileid of    :" + blocksitem.ProfileID);
                        //save data one per row
                    }
                }

                Console.WriteLine("saving  blocks "); context.SaveChanges();

                //now handle the collections for other profiledatavalues


                //handle ProfileData_Ethnicity
                foreach (Dating.Server.Data.Models.ProfileData_Ethnicity profiledataethnicityitem in olddb.ProfileData_Ethnicity)
                {
                    var profiledataethnicityobject = new Anewluv.Domain.Data.profiledata_ethnicity();

                    if (profiledataethnicityitem != null)
                    {
                        //add the realted proflemetadatas 
                        Console.WriteLine("attempting assign ethnicity for old profileid of    :" + profiledataethnicityitem.ProfileID);
                        //query the profile data
                        var matchedprofilemetatdata = context.profilemetadata.Where(p => p.profile.emailaddress == profiledataethnicityitem.ProfileID).FirstOrDefault();
                        if (matchedprofilemetatdata != null)
                        {
                            profiledataethnicityobject.profilemetadata = matchedprofilemetatdata;
                            //context.profilemetadata.Where(p => p.profile.emailaddress == profiledataethnicityitem.ProfileID).FirstOrDefault();
                            profiledataethnicityobject.ethnicty = context.lu_ethnicity.Where(p => p.id == profiledataethnicityitem.EthnicityID).FirstOrDefault();

                            //add the object to profile object
                            context.ethnicities.Add(profiledataethnicityobject);
                            //save data one per row
                            //context.SaveChanges();
                            Console.WriteLine("ethnicity added for old profileid of    :" + profiledataethnicityitem.ProfileID);
                        }
                    }
                }

                Console.WriteLine("saving  profiledata ethnicities"); context.SaveChanges();

                //handle ProfileData_hobby
                foreach (Dating.Server.Data.Models.ProfileData_Hobby profiledatahobbyitem in olddb.ProfileData_Hobby)
                {
                    var profiledatahobbyobject = new Anewluv.Domain.Data.profiledata_hobby();
                    if (profiledatahobbyitem != null)
                    {
                        //add the realted proflemetadatas 
                        Console.WriteLine("attempting assign hobby for old profileid of    :" + profiledatahobbyitem.ProfileID);
                        //query the profile data
                        var matchedprofilemetatdata = context.profilemetadata.Where(p => p.profile.emailaddress == profiledatahobbyitem.ProfileID).FirstOrDefault();
                        if (matchedprofilemetatdata != null)
                        {
                            profiledatahobbyobject.profilemetadata = matchedprofilemetatdata;
                            //context.profilemetadata.Where(p => p.profile.emailaddress == profiledatahobbyitem.ProfileID).FirstOrDefault();
                            profiledatahobbyobject.hobby = context.lu_hobby.Where(p => p.id == profiledatahobbyitem.HobbyID).FirstOrDefault();

                            //add the object to profile object
                            context.hobbies.Add(profiledatahobbyobject);
                            //save data one per row
                            //context.SaveChanges();
                            Console.WriteLine("hobby added for old profileid of    :" + profiledatahobbyitem.ProfileID);
                        }
                    }
                }

                Console.WriteLine("saving  profiledata hobbies"); context.SaveChanges();

                //handle ProfileData_hotfeature
                foreach (Dating.Server.Data.Models.ProfileData_HotFeature profiledatahotfeatureitem in olddb.ProfileData_HotFeature)
                {
                    var profiledatahotfeatureobject = new Anewluv.Domain.Data.profiledata_hotfeature();
                    if (profiledatahotfeatureitem != null)
                    {
                        //add the realted proflemetadatas 
                        Console.WriteLine("attempting assign hotfeature for old profileid of    :" + profiledatahotfeatureitem.ProfileID);
                        //query the profile data
                        var matchedprofilemetatdata = context.profilemetadata.Where(p => p.profile.emailaddress == profiledatahotfeatureitem.ProfileID).FirstOrDefault();
                        if (matchedprofilemetatdata != null)
                        {
                            profiledatahotfeatureobject.profilemetadata = matchedprofilemetatdata;
                            //context.profilemetadata.Where(p => p.profile.emailaddress == profiledatahotfeatureitem.ProfileID).FirstOrDefault();
                            profiledatahotfeatureobject.hotfeature = context.lu_hotfeature.Where(p => p.id == profiledatahotfeatureitem.HotFeatureID).FirstOrDefault();

                            //add the object to profile object
                            context.hotfeatures.Add(profiledatahotfeatureobject);
                            //save data one per row
                            //context.SaveChanges();
                            Console.WriteLine("hotfeature added for old profileid of    :" + profiledatahotfeatureitem.ProfileID);
                        }
                    }
                }

                Console.WriteLine("saving  profiledata hotfeatures"); context.SaveChanges();

                //handle ProfileData_lookingfor
                foreach (Dating.Server.Data.Models.ProfileData_LookingFor profiledatalookingforitem in olddb.ProfileData_LookingFor)
                {
                    var profiledatalookingforobject = new Anewluv.Domain.Data.profiledata_lookingfor();
                    if (profiledatalookingforitem != null)
                    {


                        //add the realted proflemetadatas 
                        Console.WriteLine("attempting assign lookingfor for old profileid of    :" + profiledatalookingforitem.ProfileID);
                        //query the profile data
                        var matchedprofilemetatdata = context.profilemetadata.Where(p => p.profile.emailaddress == profiledatalookingforitem.ProfileID).FirstOrDefault();
                        if (matchedprofilemetatdata != null)
                        {
                           

                            profiledatalookingforobject.profilemetadata = matchedprofilemetatdata;
                            //context.profilemetadata.Where(p => p.profile.emailaddress == profiledatalookingforitem.ProfileID).FirstOrDefault();
                            profiledatalookingforobject.lookingfor = context.lu_lookingfor.Where(p => p.id == profiledatalookingforitem.LookingForID).FirstOrDefault();

                            //add the object to profile object
                            context.lookingfor.Add(profiledatalookingforobject);
                            //save data one per row
                            //context.SaveChanges();
                            Console.WriteLine("lookingfor added for old profileid of    :" + profiledatalookingforitem.ProfileID);
                        }
                    }
                }

                Console.WriteLine("saving  profiledata lookingfors"); context.SaveChanges();

            }
            catch (Exception ex)
            {

                var dd = ex.ToString();
            }


            context.SaveChanges();

        }

        //we will asl use the photo service to update the photo collections stuff
        public static void ConvertProfileDataMetadataCollectionsPhoto()
        {
            var olddb = new AnewluvFtsEntities();
            var postaldb = new PostalData2Entities();
            var context = new AnewluvContext();

          PhotoUploadModel uploadmodelfailed = new PhotoUploadModel();

            //global try for the rest of objects that are tied to profile
            try
            {
                //create refereerence to photo service
               // var PhotoService = new PhotoServiceClient();
                // PhotoService.ChannelFactory.CreateChannel();


                // populate photo object and create all the photo convenversions
                //handle favorites
                foreach (Dating.Server.Data.Models.photo photositem in olddb.photos)
                {
                    var photosobject = new Anewluv.Domain.Data.photo();

                    Console.WriteLine("attempting add a photo for the old profileid of    :" + photositem.ProfileID);
                    //get the profileID since that was saved first
                    var newprofile = context.profiles.Where(p => p.emailaddress == photositem.ProfileID).FirstOrDefault();
                    //  bool photoadded = false;
                    if (newprofile != null)
                    {
                        //get the list of all this user's photos , we are not re-adding duplicate photos
                        var alloldphotos = olddb.photos.Where(p => p.ProfileID == newprofile.emailaddress);
                        //now  before adding check the size and approved status
                        var test = context.photos.Any(z => z.size == photositem.PhotoSize & z.imagename == photositem.ImageCaption )  ;


                        if (!test)
                        {

                            
  PhotoUploadModel uploadmodel = new PhotoUploadModel();
                            uploadmodel.caption = photositem.ImageCaption;
                            uploadmodel.legacysize = photositem.PhotoSize;
                            uploadmodel.creationdate = photositem.PhotoDate.GetValueOrDefault();
                            uploadmodel.imageb64string = Convert.ToBase64String(photositem.ProfileImage); 

                           // uploadmodel.size = photositem.PhotoSize;
                            uploadmodel.imagename = photositem.ImageCaption; //added name to help compare

                            //approved stuff

                            if (photositem.Aproved == "Yes")
                            {
                                uploadmodel.approvalstatusid = context.lu_photoapprovalstatus.Where(z => z.id == (int)(photoapprovalstatusEnum.Approved)).FirstOrDefault().id;
                            }
                            else if (photositem.Aproved == "No")
                            {
                                uploadmodel.approvalstatusid = context.lu_photoapprovalstatus.Where(z => z.id == (int)(photoapprovalstatusEnum.Rejected)).FirstOrDefault().id;
                            }
                            else
                            {
                                uploadmodel.approvalstatusid = context.lu_photoapprovalstatus.Where(z => z.id == (int)(photoapprovalstatusEnum.NotReviewed)).FirstOrDefault().id;
                            }


                            //if (photositem. != null)
                            //{
                            //    uploadmodel.imagetypeid = context.lu_photoimagetype.Where(z => z.description == photositem.PhotoType.PhotoTypeDescription).FirstOrDefault().id;
                            //}
                            //else
                            //{
                            //    uploadmodel.imagetypeid = null;
                            //}


                            //handle rejection reaseaon

                            if (photositem.PhotoRejectionReason != null)
                            {
                                uploadmodel.rejectionreasonid  = context.lu_photorejectionreason.Where(z => z.id == photositem.PhotoRejectionReasonID).FirstOrDefault().id;
                            }
                            else
                            {
                                uploadmodel.rejectionreasonid  = null;
                            }


                            //handle image type
                                                                
                            if (photositem.ProfileImageType == "Gallery" && photositem.PhotoStatusID != 4)
                            {
                                uploadmodel.photostatusid = context.lu_photostatus.Where(z => z.id == (int)(photostatusEnum.Gallery)).FirstOrDefault().id;
                            }
                            else if (photositem.ProfileImageType == "NoStatus" && photositem.PhotoStatusID != 4)
                            {
                                uploadmodel.photostatusid = context.lu_photostatus.Where(z => z.id == (int)(photostatusEnum.Nostatus)).FirstOrDefault().id;
                            }
                            else if (photositem.PhotoStatusID == 4)
                            {
                                uploadmodel.photostatusid = context.lu_photostatus.Where(z => z.id == (int)(photostatusEnum.deletedbyuser)).FirstOrDefault().id;

                            }
                            else if (photositem.PhotoStatusID == 5)
                            {
                                uploadmodel.photostatusid = context.lu_photostatus.Where(z => z.id == (int)(photostatusEnum.deletedbyadmin)).FirstOrDefault().id;

                            }



                            uploadmodelfailed = uploadmodel; //backup model so we can see which one failed

                            WebChannelFactory<IPhotoService> cf = new WebChannelFactory<IPhotoService>("*");
                            IPhotoService channel = cf.CreateChannel();
                           // suggestions = channel.getactivesurfs();
                            channel.addsinglephoto(uploadmodel, newprofile.id.ToString());
                            // photoadded = true;
                            Console.WriteLine("single photo and its conversions have been added for old profileID   :" + photositem.ProfileID);
                        }
                        else
                        {

                            Console.WriteLine("photo with caption : " + photositem.ImageCaption + " is a duplicate for old profiledID: " + photositem.ProfileID);
                        }
                    }
                    else
                    {

                        //skip this photo since its not tied to any valid profile
                    }


                }






            }
            catch (Exception ex)
            {

                var dd = ex.ToString();
                var brokenmodel = uploadmodelfailed;
            }



        }

        public static void ConvertProfileSearchSettingsCollections()
        {
            var olddb = new AnewluvFtsEntities();
            var postaldb = new PostalData2Entities();
            var context = new AnewluvContext();
            var counter = 0;
            var searchsettinggenderobjecttest = new Anewluv.Domain.Data.searchsetting_gender();
            //global try for the rest of objects that are tied to profile
            try
            {

                //populate collections tied to profilemetadata


                //handle searchsetting
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSetting searchsettingitem in olddb.SearchSettings)
                {
                    var searchsettingobject = new Anewluv.Domain.Data.searchsetting();
                    Console.WriteLine("attempting a search setting for the old profileid of    :" + searchsettingitem.ProfileID);

                    if (context.searchsetting.Any(p => p.profilemetadata.profile.emailaddress == searchsettingitem.ProfileID))
                    {
                        Console.WriteLine("skipping profile with email  :" + searchsettingitem.ProfileID + "it alaready has search settings   ");
                    }
                    else
                    {


                        //add the realted proflemetadatas 
                        searchsettingobject.profilemetadata = context.profilemetadata.Where(p => p.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();




                        if (searchsettingobject.profilemetadata != null)
                        {
                            searchsettingobject.agemax = searchsettingitem.AgeMax;
                            searchsettingobject.agemin = searchsettingitem.AgeMin;
                            searchsettingobject.creationdate = searchsettingitem.CreationDate;
                            searchsettingobject.distancefromme = searchsettingitem.DistanceFromMe;
                            searchsettingobject.heightmax = searchsettingitem.HeightMin;
                            searchsettingobject.heightmin = searchsettingitem.HeightMin;
                            searchsettingobject.lastupdatedate = searchsettingitem.LastUpdateDate;
                            searchsettingobject.myperfectmatch = searchsettingitem.MyPerfectMatch;
                            searchsettingobject.savedsearch = searchsettingitem.SavedSearch;
                            searchsettingobject.searchname = searchsettingitem.SearchName;
                            searchsettingobject.searchrank = searchsettingitem.SearchRank;
                            searchsettingobject.systemmatch = searchsettingitem.SystemMatch;

                            //add the object to profile object
                            context.searchsetting.AddOrUpdate(searchsettingobject);
                            counter = counter + 1;
                            //save data one per row
                            context.SaveChanges();
                            Console.WriteLine("added a search setting for the old profileid of    :" + searchsettingitem.ProfileID);
                        }
                    }
                }
                //now populate the rest of the date for each item

                //bodytype
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_BodyTypes searchsettingbodytypeitem in olddb.SearchSettings_BodyTypes)
                {
                    Console.WriteLine("attempting a search setting bodytype for the old profileid of    :" + searchsettingbodytypeitem.SearchSetting.ProfileID);
                    var searchsettingbodytypeobject = new Anewluv.Domain.Data.searchsetting_bodytype();

                    if (context.searchsetting_bodytype.Any(p => p.bodytype.id == searchsettingbodytypeitem.BodyTypesID))
                    {
                        Console.WriteLine("skipping profile with email  :" + searchsettingbodytypeitem.SearchSetting.ProfileID  + "it alaready has search settings bodytype   ");
                    }
                    else
                    {

                        var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingbodytypeitem.SearchSetting.ProfileID).FirstOrDefault();
                        if (matchedsearchsetting != null)
                        {
                            searchsettingbodytypeobject.searchsetting = matchedsearchsetting;
                            searchsettingbodytypeobject.bodytype = context.lu_bodytype.Where(p => p.id == searchsettingbodytypeitem.BodyTypesID).FirstOrDefault();
                            context.searchsetting_bodytype.Add(searchsettingbodytypeobject);
                            //save data one per row
                            Console.WriteLine("added a search setting bodytype for the old profileid of    :" + searchsettingbodytypeitem.SearchSetting.ProfileID);
                            counter = counter + 1;
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettingbodytypes"); context.SaveChanges();

                //diet
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_Diet searchsettingdietitem in olddb.SearchSettings_Diet)
                {
                    Console.WriteLine("attempting a search setting diet for the old profileid of    :" + searchsettingdietitem.SearchSetting.ProfileID);
                    var searchsettingdietobject = new Anewluv.Domain.Data.searchsetting_diet();

                    if (context.searchsetting_diet.Any(p => p.diet.id == searchsettingdietitem.DietID ))
                    {
                        Console.WriteLine("skipping profile with email  :" + searchsettingdietitem.SearchSetting.ProfileID + "it alaready has search settings diet   ");
                    }
                    else
                    {
                        var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingdietitem.SearchSetting.ProfileID).FirstOrDefault();
                        if (matchedsearchsetting != null)
                        {
                            searchsettingdietobject.searchsetting = matchedsearchsetting;
                            searchsettingdietobject.diet = context.lu_diet.Where(p => p.id == searchsettingdietitem.DietID).FirstOrDefault();
                            context.searchsetting_diet.Add(searchsettingdietobject);
                            //save data one per row
                            Console.WriteLine("added a search setting diet for the old profileid of    :" + searchsettingdietitem.SearchSetting.ProfileID);
                            counter = counter + 1;
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettingdiets"); context.SaveChanges();

                //drink
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_Drinks searchsettingdrinkitem in olddb.SearchSettings_Drinks)
                {
                    Console.WriteLine("attempting a search setting drink for the old profileid of    :" + searchsettingdrinkitem.SearchSetting.ProfileID);
                    var searchsettingdrinkobject = new Anewluv.Domain.Data.searchsetting_drink();

                    if (context.searchsetting_drink.Any(p => p.drink.id == searchsettingdrinkitem.DrinksID ))
                    {
                        Console.WriteLine("skipping profile with email  :" + searchsettingdrinkitem.SearchSetting.ProfileID + "it alaready has search settings drink   ");
                    }
                    else
                    {
                        var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingdrinkitem.SearchSetting.ProfileID).FirstOrDefault();
                        if (matchedsearchsetting != null)
                        {
                            searchsettingdrinkobject.searchsetting = matchedsearchsetting;
                            searchsettingdrinkobject.drink = context.lu_drinks.Where(p => p.id == searchsettingdrinkitem.DrinksID).FirstOrDefault();
                            context.searchsetting_drink.Add(searchsettingdrinkobject);
                            //save data one per row
                            Console.WriteLine("added a search setting drink for the old profileid of    :" + searchsettingdrinkitem.SearchSetting.ProfileID);
                            counter = counter + 1;
                        }
                    }
                }
                    
                Console.WriteLine("saving  a total of : " + counter + " searchsettingdrinks"); context.SaveChanges();


                //educationlevel
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_EducationLevel searchsettingeducationlevelitem in olddb.SearchSettings_EducationLevel)
                {
                    Console.WriteLine("attempting a search setting educationlevel for the old profileid of    :" + searchsettingeducationlevelitem.SearchSetting.ProfileID);
                    var searchsettingeducationlevelobject = new Anewluv.Domain.Data.searchsetting_educationlevel();

                    if (context.searchsetting_educationlevel.Any(p => p.educationlevel.id == searchsettingeducationlevelitem.EducationLevelID ))
                    {
                        Console.WriteLine("skipping profile with email  :" + searchsettingeducationlevelitem.SearchSetting.ProfileID + "it alaready has search settings educationlevel   ");
                    }
                    else
                    {
                        var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingeducationlevelitem.SearchSetting.ProfileID).FirstOrDefault();
                        if (matchedsearchsetting != null)
                        {
                            searchsettingeducationlevelobject.searchsetting = matchedsearchsetting;
                            searchsettingeducationlevelobject.educationlevel = context.lu_educationlevel.Where(p => p.id == searchsettingeducationlevelitem.EducationLevelID).FirstOrDefault();
                            context.searchsetting_educationlevel.Add(searchsettingeducationlevelobject);
                            //save data one per row
                            Console.WriteLine("added a search setting educationlevel for the old profileid of    :" + searchsettingeducationlevelitem.SearchSetting.ProfileID);
                            counter = counter + 1;
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettingeducationlevels"); context.SaveChanges();




                //employmentstatus
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_EmploymentStatus searchsettingemploymentstatusitem in olddb.SearchSettings_EmploymentStatus)
                {
                    Console.WriteLine("attempting a search setting employmentstatus for the old profileid of    :" + searchsettingemploymentstatusitem.SearchSetting.ProfileID);
                    var searchsettingemploymentstatusobject = new Anewluv.Domain.Data.searchsetting_employmentstatus();
                  
                    if (context.searchsetting_employmentstatus.Any(p => p.employmentstatus.id == searchsettingemploymentstatusitem.EmploymentStatusID ))
                    {
                        Console.WriteLine("skipping profile with email  :" + searchsettingemploymentstatusitem.SearchSetting.ProfileID + "it alaready has search settings employmentstatus   ");
                    }
                    else
                    {

                        var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingemploymentstatusitem.SearchSetting.ProfileID).FirstOrDefault();
                        if (matchedsearchsetting != null)
                        {
                            searchsettingemploymentstatusobject.searchsetting = matchedsearchsetting;
                            searchsettingemploymentstatusobject.employmentstatus = context.lu_employmentstatus.Where(p => p.id == searchsettingemploymentstatusitem.EmploymentStatusID).FirstOrDefault();
                            context.searchsetting_employmentstatus.Add(searchsettingemploymentstatusobject);
                            //save data one per row
                            Console.WriteLine("added a search setting employmentstatus for the old profileid of    :" + searchsettingemploymentstatusitem.SearchSetting.ProfileID);
                            counter = counter + 1;
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettingemploymentstatuss"); context.SaveChanges();




                //ethnicity
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_Ethnicity searchsettingethnicityitem in olddb.SearchSettings_Ethnicity)
                {
                    Console.WriteLine("attempting a search setting ethnicity for the old profileid of    :" + searchsettingethnicityitem.SearchSetting.ProfileID);
                    var searchsettingethnicityobject = new Anewluv.Domain.Data.searchsetting_ethnicity();
                    if (context.searchsetting_ethnicity.Any(p => p.ethnicity.id == searchsettingethnicityitem.EthicityID ))
                    {
                        Console.WriteLine("skipping profile with email  :" + searchsettingethnicityitem.SearchSetting.ProfileID + "it alaready has search settings ethnicity   ");
                    }
                    else
                    {
                        var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingethnicityitem.SearchSetting.ProfileID).FirstOrDefault();
                        if (matchedsearchsetting != null)
                        {
                            searchsettingethnicityobject.searchsetting = matchedsearchsetting;
                            searchsettingethnicityobject.ethnicity = context.lu_ethnicity.Where(p => p.id == searchsettingethnicityitem.EthicityID).FirstOrDefault();
                            context.searchsetting_ethnicity.Add(searchsettingethnicityobject);
                            //save data one per row
                            Console.WriteLine("added a search setting ethnicity for the old profileid of    :" + searchsettingethnicityitem.SearchSetting.ProfileID);
                            counter = counter + 1;
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettingethnicitys"); context.SaveChanges();




                //exercise
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_Exercise searchsettingexerciseitem in olddb.SearchSettings_Exercise)
                {
                    Console.WriteLine("attempting a search setting exercise for the old profileid of    :" + searchsettingexerciseitem.SearchSetting.ProfileID);
                    var searchsettingexerciseobject = new Anewluv.Domain.Data.searchsetting_exercise();

                    if (context.searchsetting_exercise.Any(p => p.exercise.id == searchsettingexerciseitem.ExerciseID ))
                    {
                        Console.WriteLine("skipping profile with email  :" + searchsettingexerciseitem.SearchSetting.ProfileID + "it alaready has search settings exercise   ");
                    }
                    else
                    {
                        var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingexerciseitem.SearchSetting.ProfileID).FirstOrDefault();
                        if (matchedsearchsetting != null)
                        {
                            searchsettingexerciseobject.searchsetting = matchedsearchsetting;
                            searchsettingexerciseobject.exercise = context.lu_exercise.Where(p => p.id == searchsettingexerciseitem.ExerciseID).FirstOrDefault();
                            context.searchsetting_exercise.Add(searchsettingexerciseobject);
                            //save data one per row
                            Console.WriteLine("added a search setting exercise for the old profileid of    :" + searchsettingexerciseitem.SearchSetting.ProfileID);
                            counter = counter + 1;
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettingexercises"); context.SaveChanges();




                //eyecolor
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_EyeColor searchsettingeyecoloritem in olddb.SearchSettings_EyeColor)
                {
                    Console.WriteLine("attempting a search setting eyecolor for the old profileid of    :" + searchsettingeyecoloritem.SearchSetting.ProfileID);
                    var searchsettingeyecolorobject = new Anewluv.Domain.Data.searchsetting_eyecolor();
                    if (context.searchsetting_eyecolor.Any(p => p.eyecolor.id == searchsettingeyecoloritem.EyeColorID ))
                    {
                        Console.WriteLine("skipping profile with email  :" + searchsettingeyecoloritem.SearchSetting.ProfileID + "it alaready has search settings eyecolor   ");
                    }
                    else
                    {
                        var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingeyecoloritem.SearchSetting.ProfileID).FirstOrDefault();
                        if (matchedsearchsetting != null)
                        {
                            searchsettingeyecolorobject.searchsetting = matchedsearchsetting;
                            searchsettingeyecolorobject.eyecolor = context.lu_eyecolor.Where(p => p.id == searchsettingeyecoloritem.EyeColorID).FirstOrDefault();
                            context.searchsetting_eyecolor.Add(searchsettingeyecolorobject);
                            //save data one per row
                            Console.WriteLine("added a search setting eyecolor for the old profileid of    :" + searchsettingeyecoloritem.SearchSetting.ProfileID);
                            counter = counter + 1;
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettingeyecolors"); context.SaveChanges();




                //gender
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_Genders searchsettinggenderitem in olddb.SearchSettings_Genders)
                {
                    //if (counter == 127)
                    //{
                    //    var here =0;
                    //}
                    if (searchsettinggenderitem.SearchSetting != null)
                    {
                        Console.WriteLine("attempting a search setting gender for the old profileid of    :" + searchsettinggenderitem.SearchSetting.ProfileID);
                        var searchsettinggenderobject = new Anewluv.Domain.Data.searchsetting_gender();
                        if (context.searchsetting_gender.Any(p => p.gender.id == searchsettinggenderitem.GenderID ))
                        {
                            Console.WriteLine("skipping profile with email  :" + searchsettinggenderitem.SearchSetting.ProfileID + "it alaready has search settings gender   ");
                        }
                        else
                        {
                            var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettinggenderitem.SearchSetting.ProfileID).FirstOrDefault();
                            if (matchedsearchsetting != null)
                            {
                                searchsettinggenderobject.searchsetting = matchedsearchsetting;
                                searchsettinggenderobject.gender = context.lu_gender.Where(p => p.id == searchsettinggenderitem.GenderID).FirstOrDefault();
                                context.searchsetting_gender.Add(searchsettinggenderobject);
                                //save data one per row
                                searchsettinggenderobjecttest = searchsettinggenderobject;
                                Console.WriteLine("added a search setting gender for the old profileid of    :" + searchsettinggenderitem.SearchSetting.ProfileID);
                                counter = counter + 1;
                                Console.WriteLine("saving  a total of : " + counter + " searchsettinggenders"); context.SaveChanges();
                            }
                        }
                    }
                }




                //haircolor
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_HairColor searchsettinghaircoloritem in olddb.SearchSettings_HairColor)
                {
                    if (searchsettinghaircoloritem.SearchSetting != null)
                    {
                        Console.WriteLine("attempting a search setting haircolor for the old profileid of    :" + searchsettinghaircoloritem.SearchSetting.ProfileID);
                        var searchsettinghaircolorobject = new Anewluv.Domain.Data.searchsetting_haircolor();
                        if (context.searchsetting_haircolor.Any(p => p.haircolor.id == searchsettinghaircoloritem.HairColorID ))
                        {
                            Console.WriteLine("skipping profile with email  :" + searchsettinghaircoloritem.SearchSetting.ProfileID + "it alaready has search settings haircolor   ");
                        }
                        else
                        {

                            var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettinghaircoloritem.SearchSetting.ProfileID).FirstOrDefault();
                            if (matchedsearchsetting != null)
                            {
                                searchsettinghaircolorobject.searchsetting = matchedsearchsetting;
                                searchsettinghaircolorobject.haircolor = context.lu_haircolor.Where(p => p.id == searchsettinghaircoloritem.HairColorID).FirstOrDefault();
                                context.searchsetting_haircolor.Add(searchsettinghaircolorobject);
                                //save data one per row
                                Console.WriteLine("added a search setting haircolor for the old profileid of    :" + searchsettinghaircoloritem.SearchSetting.ProfileID);
                                counter = counter + 1;
                            }
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettinghaircolors"); context.SaveChanges();




                //hotfeature
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_HotFeature searchsettinghotfeatureitem in olddb.SearchSettings_HotFeature)
                {
                    if (searchsettinghotfeatureitem.SearchSetting != null)
                    {
                        Console.WriteLine("attempting a search setting hotfeature for the old profileid of    :" + searchsettinghotfeatureitem.SearchSetting.ProfileID);
                        var searchsettinghotfeatureobject = new Anewluv.Domain.Data.searchsetting_hotfeature();
                        if (context.searchsetting_hotfeature.Any(p => p.hotfeature.id == searchsettinghotfeatureitem.HotFeatureID ))
                        {
                            Console.WriteLine("skipping profile with email  :" + searchsettinghotfeatureitem.SearchSetting.ProfileID + "it alaready has search settings hotfeature   ");
                        }
                        else
                        {
                            var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettinghotfeatureitem.SearchSetting.ProfileID).FirstOrDefault();
                            if (matchedsearchsetting != null)
                            {
                                searchsettinghotfeatureobject.searchsetting = matchedsearchsetting;
                                searchsettinghotfeatureobject.hotfeature = context.lu_hotfeature.Where(p => p.id == searchsettinghotfeatureitem.HotFeatureID).FirstOrDefault();
                                context.searchsetting_hotfeature.Add(searchsettinghotfeatureobject);
                                //save data one per row
                                Console.WriteLine("added a search setting hotfeature for the old profileid of    :" + searchsettinghotfeatureitem.SearchSetting.ProfileID);
                                counter = counter + 1;
                            }
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettinghotfeatures"); context.SaveChanges();




                //havekids
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_HaveKids searchsettinghavekidsitem in olddb.SearchSettings_HaveKids)
                {
                    if (searchsettinghavekidsitem.SearchSetting != null)
                    {
                        Console.WriteLine("attempting a search setting havekids for the old profileid of    :" + searchsettinghavekidsitem.SearchSetting.ProfileID);
                        var searchsettinghavekidsobject = new Anewluv.Domain.Data.searchsetting_havekids();
                        if (context.searchsetting_havekids.Any(p => p.havekids.id == searchsettinghavekidsitem.HaveKidsID ))
                        {
                            Console.WriteLine("skipping profile with email  :" + searchsettinghavekidsitem.SearchSetting.ProfileID + "it alaready has search settings havekids   ");
                        }
                        else
                        {
                            var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettinghavekidsitem.SearchSetting.ProfileID).FirstOrDefault();
                            if (matchedsearchsetting != null)
                            {
                                searchsettinghavekidsobject.searchsetting = matchedsearchsetting;
                                searchsettinghavekidsobject.havekids = context.lu_havekids.Where(p => p.id == searchsettinghavekidsitem.HaveKidsID).FirstOrDefault();
                                context.searchsetting_havekids.Add(searchsettinghavekidsobject);
                                //save data one per row
                                Console.WriteLine("added a search setting havekids for the old profileid of    :" + searchsettinghavekidsitem.SearchSetting.ProfileID);
                                counter = counter + 1;
                            }
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettinghavekidss"); context.SaveChanges();




                //hobby
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_Hobby searchsettinghobbyitem in olddb.SearchSettings_Hobby)
                {
                    if (searchsettinghobbyitem.SearchSetting != null)
                    {
                        Console.WriteLine("attempting a search setting hobby for the old profileid of    :" + searchsettinghobbyitem.SearchSetting.ProfileID);
                        var searchsettinghobbyobject = new Anewluv.Domain.Data.searchsetting_hobby();
                        if (context.searchsetting_hobby.Any(p => p.hobby.id == searchsettinghobbyitem.HobbyID ))
                        {
                            Console.WriteLine("skipping profile with email  :" + searchsettinghobbyitem.SearchSetting.ProfileID + "it alaready has search settings hobby   ");
                        }
                        else
                        {
                            var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettinghobbyitem.SearchSetting.ProfileID).FirstOrDefault();
                            if (matchedsearchsetting != null)
                            {
                                searchsettinghobbyobject.searchsetting = matchedsearchsetting;
                                searchsettinghobbyobject.hobby = context.lu_hobby.Where(p => p.id == searchsettinghobbyitem.HobbyID).FirstOrDefault();
                                context.searchsetting_hobby.Add(searchsettinghobbyobject);
                                //save data one per row
                                Console.WriteLine("added a search setting hobby for the old profileid of    :" + searchsettinghobbyitem.SearchSetting.ProfileID);
                                counter = counter + 1;
                            }
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettinghobbys"); context.SaveChanges();




                //humor
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_Humor searchsettinghumoritem in olddb.SearchSettings_Humor)
                {
                    if (searchsettinghumoritem.SearchSetting != null)
                    {
                        Console.WriteLine("attempting a search setting humor for the old profileid of    :" + searchsettinghumoritem.SearchSetting.ProfileID);
                        var searchsettinghumorobject = new Anewluv.Domain.Data.searchsetting_humor();
                        if (context.searchsetting_humor.Any(p => p.humor.id == searchsettinghumoritem.HumorID ))
                        {
                            Console.WriteLine("skipping profile with email  :" + searchsettinghumoritem.SearchSetting.ProfileID + "it alaready has search settings humor   ");
                        }
                        else
                        {
                            var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettinghumoritem.SearchSetting.ProfileID).FirstOrDefault();
                            if (matchedsearchsetting != null)
                            {
                                searchsettinghumorobject.searchsetting = matchedsearchsetting;
                                searchsettinghumorobject.humor = context.lu_humor.Where(p => p.id == searchsettinghumoritem.HumorID).FirstOrDefault();
                                context.searchsetting_humor.Add(searchsettinghumorobject);
                                //save data one per row
                                Console.WriteLine("added a search setting humor for the old profileid of    :" + searchsettinghumoritem.SearchSetting.ProfileID);
                                counter = counter + 1;
                            }
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettinghumors"); context.SaveChanges();




                //incomelevel
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_IncomeLevel searchsettingincomelevelitem in olddb.SearchSettings_IncomeLevel)
                {
                    if (searchsettingincomelevelitem.SearchSetting != null)
                    {
                        Console.WriteLine("attempting a search setting incomelevel for the old profileid of    :" + searchsettingincomelevelitem.SearchSetting.ProfileID);
                        var searchsettingincomelevelobject = new Anewluv.Domain.Data.searchsetting_incomelevel();
                        if (context.searchsetting_incomelevel.Any(p => p.incomelevel.id == searchsettingincomelevelitem.ImcomeLevelID))
                        {
                            Console.WriteLine("skipping profile with email  :" + searchsettingincomelevelitem.SearchSetting.ProfileID + "it alaready has search settings incomelevel   ");
                        }
                        else
                        {
                            var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingincomelevelitem.SearchSetting.ProfileID).FirstOrDefault();
                            if (matchedsearchsetting != null)
                            {
                                searchsettingincomelevelobject.searchsetting = matchedsearchsetting;
                                searchsettingincomelevelobject.incomelevel = context.lu_incomelevel.Where(p => p.id == searchsettingincomelevelitem.ImcomeLevelID).FirstOrDefault();
                                context.searchsetting_incomelevel.Add(searchsettingincomelevelobject);
                                //save data one per row
                                Console.WriteLine("added a search setting incomelevel for the old profileid of    :" + searchsettingincomelevelitem.SearchSetting.ProfileID);
                                counter = counter + 1;
                            }
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettingincomelevels"); context.SaveChanges();




                //livingstituation
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_LivingStituation searchsettinglivingstituationitem in olddb.SearchSettings_LivingStituation)
                {
                    if (searchsettinglivingstituationitem.SearchSetting != null)
                    {
                        Console.WriteLine("attempting a search setting livingstituation for the old profileid of    :" + searchsettinglivingstituationitem.SearchSetting.ProfileID);
                        var searchsettinglivingstituationobject = new Anewluv.Domain.Data.searchsetting_livingstituation();
                        if (context.searchsetting_livingstituation.Any(p => p.livingsituation .id == searchsettinglivingstituationitem.LivingStituationID))
                        {
                            Console.WriteLine("skipping profile with email  :" + searchsettinglivingstituationitem.SearchSetting.ProfileID + "it alaready has search settings livingstituation   ");
                        }
                        else
                        {
                            var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettinglivingstituationitem.SearchSetting.ProfileID).FirstOrDefault();
                            if (matchedsearchsetting != null)
                            {
                                searchsettinglivingstituationobject.searchsetting = matchedsearchsetting;
                                searchsettinglivingstituationobject.livingsituation = context.lu_livingsituation.Where(p => p.id == searchsettinglivingstituationitem.LivingStituationID).FirstOrDefault();
                                context.searchsetting_livingstituation.Add(searchsettinglivingstituationobject);
                                //save data one per row
                                Console.WriteLine("added a search setting livingstituation for the old profileid of    :" + searchsettinglivingstituationitem.SearchSetting.ProfileID);
                                counter = counter + 1;
                            }
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettinglivingstituations"); context.SaveChanges();




                //location
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_Location searchsettinglocationitem in olddb.SearchSettings_Location)
                {
                    if (searchsettinglocationitem.SearchSetting != null)
                    {
                        Console.WriteLine("attempting a search setting location for the old profileid of    :" + searchsettinglocationitem.SearchSetting.ProfileID);
                        var searchsettinglocationobject = new Anewluv.Domain.Data.searchsetting_location();
                        if (context.searchsetting_location.Any(p => p.countryid == searchsettinglocationitem.CountryID))
                        {
                            Console.WriteLine("skipping profile with email  :" + searchsettinglocationitem.SearchSetting.ProfileID + "it alaready has search settings location   ");
                        }
                        else
                        {
                            var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettinglocationitem.SearchSetting.ProfileID).FirstOrDefault();
                            if (matchedsearchsetting != null)
                            {
                                searchsettinglocationobject.searchsetting = matchedsearchsetting;
                                searchsettinglocationobject.countryid = searchsettinglocationitem.CountryID;  //context.lu_location.Where(p => p.id == searchsettinglocationitem.locationsID).FirstOrDefault();
                                searchsettinglocationobject.postalcode = searchsettinglocationitem.PostalCode;
                                context.searchsetting_location.Add(searchsettinglocationobject);
                                //save data one per row
                                Console.WriteLine("added a search setting location for the old profileid of    :" + searchsettinglocationitem.SearchSetting.ProfileID);
                                counter = counter + 1;
                            }
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettinglocations"); context.SaveChanges();




                //lookingfor
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_LookingFor searchsettinglookingforitem in olddb.SearchSettings_LookingFor)
                {
                    if (searchsettinglookingforitem.SearchSetting != null)
                    {
                        Console.WriteLine("attempting a search setting lookingfor for the old profileid of    :" + searchsettinglookingforitem.SearchSetting.ProfileID);
                        var searchsettinglookingforobject = new Anewluv.Domain.Data.searchsetting_lookingfor();
                        if (context.searchsetting_lookingfor .Any(p => p.lookingfor .id == searchsettinglookingforitem.LookingForID ))
                        {
                            Console.WriteLine("skipping profile with email  :" + searchsettinglookingforitem.SearchSetting.ProfileID + "it alaready has search settings lookinfor   ");
                        }
                        else
                        {
                            var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettinglookingforitem.SearchSetting.ProfileID).FirstOrDefault();
                            if (matchedsearchsetting != null)
                            {
                                searchsettinglookingforobject.searchsetting = matchedsearchsetting;
                                searchsettinglookingforobject.lookingfor = context.lu_lookingfor.Where(p => p.id == searchsettinglookingforitem.LookingForID).FirstOrDefault();
                                context.searchsetting_lookingfor.Add(searchsettinglookingforobject);
                                //save data one per row
                                Console.WriteLine("added a search setting lookingfor for the old profileid of    :" + searchsettinglookingforitem.SearchSetting.ProfileID);
                                counter = counter + 1;
                            }
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettinglookingfors"); context.SaveChanges();




                //maritalstatus
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_MaritalStatus searchsettingmaritalstatusitem in olddb.SearchSettings_MaritalStatus)
                {
                    if (searchsettingmaritalstatusitem.SearchSetting != null)
                    {
                        Console.WriteLine("attempting a search setting maritalstatus for the old profileid of    :" + searchsettingmaritalstatusitem.SearchSetting.ProfileID);
                        var searchsettingmaritalstatusobject = new Anewluv.Domain.Data.searchsetting_maritalstatus();
                        if (context.searchsetting_maritalstatus.Any(p => p.maritalstatus.id == searchsettingmaritalstatusitem.MaritalStatusID ))
                        {
                            Console.WriteLine("skipping profile with email  :" + searchsettingmaritalstatusitem.SearchSetting.ProfileID + "it alaready has search settings maritalstatus   ");
                        }
                        else
                        {
                            var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingmaritalstatusitem.SearchSetting.ProfileID).FirstOrDefault();
                            if (matchedsearchsetting != null)
                            {
                                searchsettingmaritalstatusobject.searchsetting = matchedsearchsetting;
                                searchsettingmaritalstatusobject.maritalstatus = context.lu_maritalstatus.Where(p => p.id == searchsettingmaritalstatusitem.MaritalStatusID).FirstOrDefault();
                                context.searchsetting_maritalstatus.Add(searchsettingmaritalstatusobject);
                                //save data one per row
                                Console.WriteLine("added a search setting maritalstatus for the old profileid of    :" + searchsettingmaritalstatusitem.SearchSetting.ProfileID);
                                counter = counter + 1;
                            }
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettingmaritalstatuss"); context.SaveChanges();




                //politicalview
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_PoliticalView searchsettingpoliticalviewitem in olddb.SearchSettings_PoliticalView)
                {
                    if (searchsettingpoliticalviewitem.SearchSetting != null)
                    {
                        Console.WriteLine("attempting a search setting politicalview for the old profileid of    :" + searchsettingpoliticalviewitem.SearchSetting.ProfileID);
                        var searchsettingpoliticalviewobject = new Anewluv.Domain.Data.searchsetting_politicalview();
                        if (context.searchsetting_politicalview.Any(p => p.politicalview.id == searchsettingpoliticalviewitem.PoliticalViewID ))
                        {
                            Console.WriteLine("skipping profile with email  :" + searchsettingpoliticalviewitem.SearchSetting.ProfileID + "it alaready has search settings politicalview   ");
                        }
                        else
                        {
                            var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingpoliticalviewitem.SearchSetting.ProfileID).FirstOrDefault();
                            if (matchedsearchsetting != null)
                            {
                                searchsettingpoliticalviewobject.searchsetting = matchedsearchsetting;
                                searchsettingpoliticalviewobject.politicalview = context.lu_politicalview.Where(p => p.id == searchsettingpoliticalviewitem.PoliticalViewID).FirstOrDefault();
                                context.searchsetting_politicalview.Add(searchsettingpoliticalviewobject);
                                //save data one per row
                                Console.WriteLine("added a search setting politicalview for the old profileid of    :" + searchsettingpoliticalviewitem.SearchSetting.ProfileID);
                                counter = counter + 1;
                            }
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettingpoliticalviews"); context.SaveChanges();




                //professsion
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_Profession searchsettingprofesssionitem in olddb.SearchSettings_Profession)
                {
                    if (searchsettingprofesssionitem.SearchSetting != null)
                    {
                        Console.WriteLine("attempting a search setting professsion for the old profileid of    :" + searchsettingprofesssionitem.SearchSetting.ProfileID);
                        var searchsettingprofesssionobject = new Anewluv.Domain.Data.searchsetting_profession();
                        if (context.searchsetting_profession.Any(p => p.profession.id == searchsettingprofesssionitem.ProfessionID ))
                        {
                            Console.WriteLine("skipping profile with email  :" + searchsettingprofesssionitem.SearchSetting.ProfileID + "it alaready has search settings profession   ");
                        }
                        else
                        {
                            var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingprofesssionitem.SearchSetting.ProfileID).FirstOrDefault();
                            if (matchedsearchsetting != null)
                            {
                                searchsettingprofesssionobject.searchsetting = matchedsearchsetting;
                                searchsettingprofesssionobject.profession = context.lu_profession.Where(p => p.id == searchsettingprofesssionitem.ProfessionID).FirstOrDefault();
                                context.searchsetting_profession.Add(searchsettingprofesssionobject);
                                //save data one per row
                                Console.WriteLine("added a search setting professsion for the old profileid of    :" + searchsettingprofesssionitem.SearchSetting.ProfileID);
                                counter = counter + 1;
                            }
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettingprofesssions"); context.SaveChanges();




                //religion
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_Religion searchsettingreligionitem in olddb.SearchSettings_Religion)
                {
                    if (searchsettingreligionitem.SearchSetting != null)
                    {
                        Console.WriteLine("attempting a search setting religion for the old profileid of    :" + searchsettingreligionitem.SearchSetting.ProfileID);
                        var searchsettingreligionobject = new Anewluv.Domain.Data.searchsetting_religion();
                        if (context.searchsetting_religion.Any(p => p.religion.id == searchsettingreligionitem.ReligionID ))
                        {
                            Console.WriteLine("skipping profile with email  :" + searchsettingreligionitem.SearchSetting.ProfileID + "it alaready has search settings religion   ");
                        }
                        else
                        {
                            var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingreligionitem.SearchSetting.ProfileID).FirstOrDefault();
                            if (matchedsearchsetting != null)
                            {
                                searchsettingreligionobject.searchsetting = matchedsearchsetting;
                                searchsettingreligionobject.religion = context.lu_religion.Where(p => p.id == searchsettingreligionitem.ReligionID).FirstOrDefault();
                                context.searchsetting_religion.Add(searchsettingreligionobject);
                                //save data one per row
                                Console.WriteLine("added a search setting religion for the old profileid of    :" + searchsettingreligionitem.SearchSetting.ProfileID);
                                counter = counter + 1;
                            }
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettingreligions"); context.SaveChanges();



                //religiousattendance
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_ReligiousAttendance searchsettingreligiousattendanceitem in olddb.SearchSettings_ReligiousAttendance)
                {
                    if (searchsettingreligiousattendanceitem.SearchSetting != null)
                    {
                        Console.WriteLine("attempting a search setting religiousattendance for the old profileid of    :" + searchsettingreligiousattendanceitem.SearchSetting.ProfileID);
                        var searchsettingreligiousattendanceobject = new Anewluv.Domain.Data.searchsetting_religiousattendance();
                        if (context.searchsetting_religiousattendance.Any(p => p.religiousattendance.id == searchsettingreligiousattendanceitem.ReligiousAttendanceID ))
                        {
                            Console.WriteLine("skipping profile with email  :" + searchsettingreligiousattendanceitem.SearchSetting.ProfileID + "it alaready has search settings religiousattendance   ");
                        }
                        else
                        {
                            var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingreligiousattendanceitem.SearchSetting.ProfileID).FirstOrDefault();
                            if (matchedsearchsetting != null)
                            {
                                searchsettingreligiousattendanceobject.searchsetting = matchedsearchsetting;
                                searchsettingreligiousattendanceobject.religiousattendance = context.lu_religiousattendance.Where(p => p.id == searchsettingreligiousattendanceitem.ReligiousAttendanceID).FirstOrDefault();
                                context.searchsetting_religiousattendance.Add(searchsettingreligiousattendanceobject);
                                //save data one per row
                                Console.WriteLine("added a search setting religiousattendance for the old profileid of    :" + searchsettingreligiousattendanceitem.SearchSetting.ProfileID);
                                counter = counter + 1;
                            }
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettingreligiousattendances"); context.SaveChanges();



                //showme
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_ShowMe searchsettingshowmeitem in olddb.SearchSettings_ShowMe)
                {
                    if (searchsettingshowmeitem.SearchSetting != null)
                    {
                        Console.WriteLine("attempting a search setting showme for the old profileid of    :" + searchsettingshowmeitem.SearchSetting.ProfileID);
                        var searchsettingshowmeobject = new Anewluv.Domain.Data.searchsetting_showme();
                        if (context.searchsetting_showme.Any(p => p.showme.id == searchsettingshowmeitem.ShowMeID ))
                        {
                            Console.WriteLine("skipping profile with email  :" + searchsettingshowmeitem.SearchSetting.ProfileID + "it alaready has search settings showme   ");
                        }
                        else
                        {
                            var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingshowmeitem.SearchSetting.ProfileID).FirstOrDefault();
                            if (matchedsearchsetting != null)
                            {
                                searchsettingshowmeobject.searchsetting = matchedsearchsetting;
                                searchsettingshowmeobject.showme = context.lu_showme.Where(p => p.id == searchsettingshowmeitem.ShowMeID).FirstOrDefault();
                                context.searchsetting_showme.Add(searchsettingshowmeobject);
                                //save data one per row
                                Console.WriteLine("added a search setting showme for the old profileid of    :" + searchsettingshowmeitem.SearchSetting.ProfileID);
                                counter = counter + 1;
                            }
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettingshowmes"); context.SaveChanges();




                //sign
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_Sign searchsettingsignitem in olddb.SearchSettings_Sign)
                {
                    if (searchsettingsignitem.SearchSetting != null)
                    {
                        Console.WriteLine("attempting a search setting sign for the old profileid of    :" + searchsettingsignitem.SearchSetting.ProfileID);
                        var searchsettingsignobject = new Anewluv.Domain.Data.searchsetting_sign();
                        if (context.searchsetting_sign.Any(p => p.sign.id == searchsettingsignitem.SignID ))
                        {
                            Console.WriteLine("skipping profile with email  :" + searchsettingsignitem.SearchSetting.ProfileID + "it alaready has search settings sign   ");
                        }
                        else
                        {
                            var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingsignitem.SearchSetting.ProfileID).FirstOrDefault();
                            if (matchedsearchsetting != null)
                            {
                                searchsettingsignobject.searchsetting = matchedsearchsetting;
                                searchsettingsignobject.sign = context.lu_sign.Where(p => p.id == searchsettingsignitem.SignID).FirstOrDefault();
                                context.searchsetting_sign.Add(searchsettingsignobject);
                                //save data one per row
                                Console.WriteLine("added a search setting sign for the old profileid of    :" + searchsettingsignitem.SearchSetting.ProfileID);
                                counter = counter + 1;
                            }
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettingsigns"); context.SaveChanges();






                //smokes
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_Smokes searchsettingsmokesitem in olddb.SearchSettings_Smokes)
                {
                    if (searchsettingsmokesitem.SearchSetting != null)
                    {
                        Console.WriteLine("attempting a search setting smokes for the old profileid of    :" + searchsettingsmokesitem.SearchSetting.ProfileID);
                        var searchsettingsmokesobject = new Anewluv.Domain.Data.searchsetting_smokes();
                        if (context.searchsetting_smokes.Any(p => p.smoke.id == searchsettingsmokesitem.SmokesID ))
                        {
                            Console.WriteLine("skipping profile with email  :" + searchsettingsmokesitem.SearchSetting.ProfileID + "it alaready has search settings smokes   ");
                        }
                        else
                        {
                            var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingsmokesitem.SearchSetting.ProfileID).FirstOrDefault();
                            if (matchedsearchsetting != null)
                            {
                                searchsettingsmokesobject.searchsetting = matchedsearchsetting;
                                searchsettingsmokesobject.smoke = context.lu_smokes.Where(p => p.id == searchsettingsmokesitem.SmokesID).FirstOrDefault();
                                context.searchsetting_smokes.Add(searchsettingsmokesobject);
                                //save data one per row
                                Console.WriteLine("added a search setting smokes for the old profileid of    :" + searchsettingsmokesitem.SearchSetting.ProfileID);
                                counter = counter + 1;
                            }
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettingsmokess"); context.SaveChanges();




                //sortbytype
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_SortByType searchsettingsortbytypeitem in olddb.SearchSettings_SortByType)
                {
                    if (searchsettingsortbytypeitem.SearchSetting != null)
                    {
                        Console.WriteLine("attempting a search setting sortbytype for the old profileid of    :" + searchsettingsortbytypeitem.SearchSetting.ProfileID);
                        var searchsettingsortbytypeobject = new Anewluv.Domain.Data.searchsetting_sortbytype();
                        if (context.searchsetting_sortbytype.Any(p => p.sortbytype.id == searchsettingsortbytypeitem.SortByTypeID ))
                        {
                            Console.WriteLine("skipping profile with email  :" + searchsettingsortbytypeitem.SearchSetting.ProfileID + "it alaready has search settings sortbytype   ");
                        }
                        else
                        {
                            var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingsortbytypeitem.SearchSetting.ProfileID).FirstOrDefault();
                            if (matchedsearchsetting != null)
                            {
                                searchsettingsortbytypeobject.searchsetting = matchedsearchsetting;
                                searchsettingsortbytypeobject.sortbytype = context.lu_sortbytype.Where(p => p.id == searchsettingsortbytypeitem.SortByTypeID).FirstOrDefault();
                                context.searchsetting_sortbytype.Add(searchsettingsortbytypeobject);
                                //save data one per row
                                Console.WriteLine("added a search setting sortbytype for the old profileid of    :" + searchsettingsortbytypeitem.SearchSetting.ProfileID);
                                counter = counter + 1;
                            }
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettingsortbytypes"); context.SaveChanges();


                //wantskids
                counter = 0;
                foreach (Dating.Server.Data.Models.SearchSettings_WantKids searchsettingwantskidsitem in olddb.SearchSettings_WantKids)
                {
                    if (searchsettingwantskidsitem.SearchSetting != null)
                    {
                        Console.WriteLine("attempting a search setting wantskids for the old profileid of    :" + searchsettingwantskidsitem.SearchSetting.ProfileID);
                        var searchsettingwantskidsobject = new Anewluv.Domain.Data.searchsetting_wantkids();
                        if (context.searchsetting_wantkids.Any(p => p.wantskids .id == searchsettingwantskidsitem.WantKidsID ))
                        {
                            Console.WriteLine("skipping profile with email  :" + searchsettingwantskidsitem.SearchSetting.ProfileID + "it alaready has search settings wantskids   ");
                        }
                        else
                        {
                            var matchedsearchsetting = context.searchsetting.Where(p => p.profilemetadata.profile.emailaddress == searchsettingwantskidsitem.SearchSetting.ProfileID).FirstOrDefault();
                            if (matchedsearchsetting != null)
                            {
                                searchsettingwantskidsobject.searchsetting = matchedsearchsetting;
                                searchsettingwantskidsobject.wantskids = context.lu_wantskids.Where(p => p.id == searchsettingwantskidsitem.WantKidsID).FirstOrDefault();
                                context.searchsetting_wantkids.Add(searchsettingwantskidsobject);
                                //save data one per row
                                Console.WriteLine("added a search setting wantskids for the old profileid of    :" + searchsettingwantskidsitem.SearchSetting.ProfileID);
                                counter = counter + 1;
                            }
                        }
                    }
                }
                Console.WriteLine("saving  a total of : " + counter + " searchsettingwantskidss"); context.SaveChanges();




            }
            catch (Exception ex)
            {
                var mytest = searchsettinggenderobjecttest.searchsetting;
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
