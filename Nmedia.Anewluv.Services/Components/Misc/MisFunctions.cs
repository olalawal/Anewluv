using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Migrations;

//using DatingModel;



//using Shell.MVC2.Infrastructure.Entities;
//using LoggingLibrary;



using System.ServiceModel.Web;

using Anewluv.Domain;
using Anewluv.Domain.Data;
using Anewluv.Domain.Data.ViewModels;
using AnewLuvFTS.DomainAndData.Models;
using GeoData.Domain.Models;

namespace Misc
{
    public static class MisFunctions
    {

        //synch up anew luv database with the new database 
        //add the old database model
        //once this is tested and working we want to move this code into migrations ins Domain.Entities
        public static void StartDebuggingTest()
        {
            var olddb = new AnewluvFTSContext();
            var postaldb = new PostalData2Context();
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
            var olddb = new AnewluvFTSContext();
            var postaldb = new PostalData2Context();
            var context = new AnewluvContext();

            //code for simple type data swap
            //olddb.profiles.ToList().ForEach(p => context.profiles.AddOrUpdate(new Anewluv.Domain.Data.profile()
            //{
            //})          
            //);

            
            
            //convert profileData and profile first 
            //convet abusers data

            int newprofileid = 1;
            foreach (AnewLuvFTS.DomainAndData.Models.profile item in olddb.profiles)
            {
                var myprofile = new Anewluv.Domain.Data.profile();
                
                var myprofiledata = new Anewluv.Domain.Data.profiledata();
                //build the related  profilemetadata noew
                var myprofilemetadata = new Anewluv.Domain.Data.profilemetadata();
                var myopenids = new List<openid>();
                var mylogtimes = new List<userlogtime>();


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
                        myprofile.status_id = context.lu_profilestatus.Where(z => z.id == item.ProfileStatusID).FirstOrDefault().id;
                        // myprofile.securityquestion_id = item.SecurityQuestionID;
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
                        myprofiledata.lu_gender = context.lu_gender.Where(p => p.id == item.ProfileData.GenderID).FirstOrDefault();
                        myprofiledata.lu_bodytype = context.lu_bodytype.Where(p => p.id == item.ProfileData.BodyTypeID).FirstOrDefault();
                        myprofiledata.lu_eyecolor = context.lu_eyecolor.Where(p => p.id == item.ProfileData.EyeColorID).FirstOrDefault();
                        myprofiledata.lu_haircolor = context.lu_haircolor.Where(p => p.id == item.ProfileData.HairColorID).FirstOrDefault();
                        myprofiledata.lu_diet = context.lu_diet.Where(p => p.id == item.ProfileData.DietID).FirstOrDefault();
                        myprofiledata.lu_drinks = context.lu_drinks.Where(p => p.id == item.ProfileData.DrinksID).FirstOrDefault();
                        myprofiledata.lu_exercise = context.lu_exercise.Where(p => p.id == item.ProfileData.ExerciseID).FirstOrDefault();
                        myprofiledata.lu_humor = context.lu_humor.Where(p => p.id == item.ProfileData.HumorID).FirstOrDefault();
                        myprofiledata.lu_politicalview = context.lu_politicalview.Where(p => p.id == item.ProfileData.PoliticalViewID).FirstOrDefault();
                        myprofiledata.lu_religion = context.lu_religion.Where(p => p.id == item.ProfileData.ReligionID).FirstOrDefault();
                        myprofiledata.lu_religiousattendance = context.lu_religiousattendance.Where(p => p.id == item.ProfileData.ReligiousAttendanceID).FirstOrDefault();
                        myprofiledata.lu_sign = context.lu_sign.Where(p => p.id == item.ProfileData.SignID).FirstOrDefault();
                        myprofiledata.lu_smokes = context.lu_smokes.Where(p => p.id == item.ProfileData.SmokesID).FirstOrDefault();
                        myprofiledata.lu_educationlevel = context.lu_educationlevel.Where(p => p.id == item.ProfileData.EducationLevelID).FirstOrDefault();
                        myprofiledata.lu_employmentstatus = context.lu_employmentstatus.Where(p => p.id == item.ProfileData.EmploymentSatusID).FirstOrDefault();
                        myprofiledata.lu_havekids = context.lu_havekids.Where(p => p.id == item.ProfileData.HaveKidsId).FirstOrDefault();
                        myprofiledata.lu_incomelevel = context.lu_incomelevel.Where(p => p.id == item.ProfileData.IncomeLevelID).FirstOrDefault();
                        myprofiledata.lu_livingsituation = context.lu_livingsituation.Where(p => p.id == item.ProfileData.LivingSituationID).FirstOrDefault();
                        myprofiledata.lu_maritalstatus = context.lu_maritalstatus.Where(p => p.id == item.ProfileData.MaritalStatusID).FirstOrDefault();
                        myprofiledata.lu_profession = context.lu_profession.Where(p => p.id == item.ProfileData.ProfessionID).FirstOrDefault();
                        myprofiledata.lu_wantskids = context.lu_wantskids.Where(p => p.id == item.ProfileData.WantsKidsID).FirstOrDefault();
                        //visiblity settings was never implemented anyways.
                        // myprofiledata.visibilitysettings=  context.visibilitysettings.Where(p => p.id   == item.Prof).FirstOrDefault();     


                        myprofilemetadata.profile_id = newprofilecreated.id;


                        //call create mail here


                        //add openids
                        foreach (profileOpenIDStore openiditem in item.profileOpenIDStores)
                        {
                            myopenids.Add(new openid
                            {
                                active = openiditem.active,
                                creationdate = openiditem.creationDate,
                                openidprovider_id = context.lu_openidprovider.Where(z => z.description == openiditem.openidProviderName).FirstOrDefault().id,
                                openididentifier = openiditem.openidIdentifier,
                                profile_id = newprofilecreated.id
                            });

                            Console.WriteLine("added open id for user :    :" + item.ProfileID);
                        }

                        //add user logtimes 
                        foreach (AnewLuvFTS.DomainAndData.Models.User_Logtime logtime in olddb.User_Logtime.
                                 Where(p => p.ProfileData.profile.ProfileID == myprofile.emailaddress))
                        {

                            mylogtimes.Add(new userlogtime
                           {
                               logintime = logtime.LoginTime,
                               logouttime = logtime.LogoutTime,
                               offline = true,
                               profile_id = newprofilecreated.id,
                               sessionid = logtime.SessionID
                           });
                            Console.WriteLine("profile added succesfull   :");
                        }

                        //mail stuff 





                        //add the two new objects to profile
                        //********************************
                        myprofile.profiledata = myprofiledata;
                        myprofile.profilemetadata = myprofilemetadata;
                        context.profiles.AddOrUpdate(myprofile);
                       
                      
                    }




                    catch (Exception ex)
                    {

                        var dd = ex.ToString();
                    }
                    finally
                    {
                        //iccrement on faliture too
                        newprofileid = newprofileid + 1;
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


        public static void ConvertProfileMails()
        {
            var olddb = new AnewluvFTSContext();
            var postaldb = new PostalData2Context();
            var context = new AnewluvContext();


            //global try for the rest of objects that are tied to profile
            try
            {

                //populate collections tied to profile and profiledata


                //build  members in role data if it exists
                foreach (AnewLuvFTS.DomainAndData.Models.profile oldprofile in olddb.profiles)
                {
                    var membersinroleobject = new Anewluv.Domain.Data.membersinrole();

                   
                    //query the profile data
                    //var matchedprofile = context.profiles.Where(p => p.emailaddress == membersinroleitem.ProfileID).FirstOrDefault();
                    // Metadata classes are not meant to be instantiated.
                    // myprofile.id = matchedprofile.First().id ;
                    var matchedprofile = context.profiles.Where(p => p.emailaddress == oldprofile.ProfileID).FirstOrDefault();
                    if (matchedprofile != null)
                    {
                        
                        //get mailbox folders first 
                        if (oldprofile.ProfileData.MailboxFolders.Count() > 0)
                        {
                            Console.WriteLine("attempting to create mailbox folders    :" + oldprofile.ProfileID);
                            List<mailboxfolder> maildboxfolders = new List<mailboxfolder>();
                            foreach (MailboxFolder oldfolder in oldprofile.ProfileData.MailboxFolders)
                            {

                                maildboxfolders.Add(new mailboxfolder
                                {
                                    active = oldfolder.Active ,  
                                    foldertype_id = context.mailboxfolders
                                    
                                   , profile_id 
                                });


                            }





















                        }
                        else
                        {
                            Console.WriteLine("No mailbox detected skipping    :" + oldprofile.ProfileID);
                        }









                        Console.WriteLine("role added for old profileid of   :" + membersinroleitem.ProfileID);
                        //save data one per row
                    }

                }



          




            }
            catch (Exception ex)
            {

                var dd = ex.ToString();
            }

            context.SaveChanges();

        }


        public static void ConvertProfileCollections()
        {
            var olddb = new AnewluvFTSContext();
            var postaldb = new PostalData2Context();
            var context = new AnewluvContext();


            //global try for the rest of objects that are tied to profile
            try
            {

                //populate collections tied to profile and profiledata


                //build  members in role data if it exists
                foreach (AnewLuvFTS.DomainAndData.Models.MembersInRole membersinroleitem in olddb.MembersInRoles)
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
                        membersinroleobject.lu_role = context.lu_role.Where(z => z.id == membersinroleitem.Role.RoleID).FirstOrDefault();
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
                foreach (AnewLuvFTS.DomainAndData.Models.ProfileGeoDataLogger activitylogitem in olddb.ProfileGeoDataLoggers)
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
                        activitylogobject.lu_activitytype = context.lu_activitytype.Where(p => p.id == (int)activitytypeEnum.fromoldInitialdatabasestructure).FirstOrDefault();
                        //add the profile
                        activitylogobject.profile = matchedprofile; //context.profiles.Where(p => p.emailaddress == activitylogitem.ProfileID).FirstOrDefault();

                        //build related geodata  object
                        var myprofileactivitygeodata = new Anewluv.Domain.Data.profileactivitygeodata();

                        myprofileactivitygeodata.city = activitylogitem.City;
                        myprofileactivitygeodata.regionname = activitylogitem.RegionName;
                        myprofileactivitygeodata.continent = activitylogitem.Continent;
                        myprofileactivitygeodata.countryId = postaldb.GetCountryPostalCodeList().Where(p => p.CountryName == activitylogitem.CountryName).FirstOrDefault().CountryID;
                        myprofileactivitygeodata.countryname = activitylogitem.CountryName;
                        myprofileactivitygeodata.creationdate = activitylogitem.CreationDate;
                        myprofileactivitygeodata.lattitude = activitylogitem.Lattitude;
                        myprofileactivitygeodata.longitude = activitylogitem.Longitude;

                        //add the geodata value for this object to activitylog
                        activitylogobject.profileactivitygeodata = myprofileactivitygeodata;

                        //add the object to profile object
                        context.profileactivities.Add(activitylogobject);
                        //save data one per row
                        Console.WriteLine("geo activity log value added for old profileid of   :" + activitylogitem.ProfileID);
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
            var olddb = new AnewluvFTSContext();
            var postaldb = new PostalData2Context();
            var context = new AnewluvContext();


            //global try for the rest of objects that are tied to profile
            try
            {

                //populate collections tied to profilemetadata


                //handle favorites
                foreach (AnewLuvFTS.DomainAndData.Models.favorite favoritesitem in olddb.favorites)
                {
                    var favoritesobject = new Anewluv.Domain.Data.favorites();
                    Console.WriteLine("attempting to add a favorite for the old profileid of    :" + favoritesitem.ProfileID);
                    //add the realted proflemetadatas 
                    var matchedprofilemetatdata = context.profilemetadatas.Where(p => p.profile.emailaddress == favoritesitem.ProfileID).FirstOrDefault();
                    var matchedfavoriteprofilemetadata = context.profilemetadatas.Where(p => p.profile.emailaddress == favoritesitem.FavoriteID).FirstOrDefault();
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
                foreach (AnewLuvFTS.DomainAndData.Models.Friend friendsitem in olddb.Friends)
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
                foreach (AnewLuvFTS.DomainAndData.Models.Interest Interestsitem in olddb.Interests)
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
                foreach (AnewLuvFTS.DomainAndData.Models.Like likesitem in olddb.Likes)
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
                foreach (AnewLuvFTS.DomainAndData.Models.ProfileView peeksitem in olddb.ProfileViews)
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
                foreach (AnewLuvFTS.DomainAndData.Models.Hotlist hotlistsitem in olddb.Hotlists)
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
                foreach (AnewLuvFTS.DomainAndData.Models.Mailboxblock blocksitem in olddb.Mailboxblocks)
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
                foreach (AnewLuvFTS.DomainAndData.Models.ProfileData_Ethnicity profiledataethnicityitem in olddb.ProfileData_Ethnicity)
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
                foreach (AnewLuvFTS.DomainAndData.Models.ProfileData_Hobby profiledatahobbyitem in olddb.ProfileData_Hobby)
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
                foreach (AnewLuvFTS.DomainAndData.Models.ProfileData_HotFeature profiledatahotfeatureitem in olddb.ProfileData_HotFeature)
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
                foreach (AnewLuvFTS.DomainAndData.Models.ProfileData_LookingFor profiledatalookingforitem in olddb.ProfileData_LookingFor)
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
            var olddb = new AnewluvFTSContext();
            var postaldb = new PostalData2Context();
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
                foreach (AnewLuvFTS.DomainAndData.Models.photo photositem in olddb.photos)
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
            var olddb = new AnewluvFTSContext();
            var postaldb = new PostalData2Context();
            var context = new AnewluvContext();
            var counter = 0;
            var searchsettinggenderobjecttest = new Anewluv.Domain.Data.searchsetting_gender();
            //global try for the rest of objects that are tied to profile
            try
            {

                //populate collections tied to profilemetadata


                //handle searchsetting
                counter = 0;
                foreach (AnewLuvFTS.DomainAndData.Models.SearchSetting searchsettingitem in olddb.SearchSettings)
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
                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_BodyTypes searchsettingbodytypeitem in olddb.SearchSettings_BodyTypes)
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

          
                //location
                counter = 0;
                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_Location searchsettinglocationitem in olddb.SearchSettings_Location)
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
    //    var datingentities = new AnewluvFTSContext();
    //    var postaldataentities = new PostalData2Context();
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
