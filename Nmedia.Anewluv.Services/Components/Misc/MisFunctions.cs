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
using Anewluv.Services.Contracts;
using Repository.Pattern.Infrastructure;

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

            int profilecount = 1;
            foreach (AnewLuvFTS.DomainAndData.Models.profile item in olddb.profiles)
            {
                var myprofile = new Anewluv.Domain.Data.profile();
                
                var myprofiledata = new Anewluv.Domain.Data.profiledata();
                //build the related  profilemetadata noew
                //var myprofilemetadata = new Anewluv.Domain.Data.profilemetadata();
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
                        Console.WriteLine("attempting to assign old profile with email  :" + item.ProfileID + "to the new database with" + "added profiledcounter = " + profilecount);

                      
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

                        myprofile.profiledata = null; myprofile.profilemetadata = null;
                        myprofile.ObjectState = ObjectState.Added;
                        context.profiles.AddOrUpdate(myprofile);                    
                        context.SaveChanges();
                       // var newprofilecreated = context.profiles.Where(p => p.emailaddress == item.ProfileID).First();


                        //query the profile data
                        var matchedprofiledata = olddb.ProfileDatas.Where(p => p.ProfileID == item.ProfileID);
                        // Metadata classes are not meant to be instantiated.
                        context.profiledatas.Add(new profiledata
                        {
                            profile_id = myprofile.id, //newprofileid;
                            age = matchedprofiledata.FirstOrDefault().Age,
                            birthdate = matchedprofiledata.FirstOrDefault().Birthdate,
                            city = matchedprofiledata.FirstOrDefault().City,
                            countryregion = matchedprofiledata.FirstOrDefault().Country_Region,
                            stateprovince = matchedprofiledata.FirstOrDefault().State_Province,
                            countryid = matchedprofiledata.FirstOrDefault().CountryID,
                            longitude = matchedprofiledata.FirstOrDefault().Longitude,
                            latitude = matchedprofiledata.FirstOrDefault().Latitude,
                            aboutme = matchedprofiledata.FirstOrDefault().AboutMe,
                            height = (long)matchedprofiledata.FirstOrDefault().Height.GetValueOrDefault(),
                            mycatchyintroLine = matchedprofiledata.FirstOrDefault().MyCatchyIntroLine,
                            phone = matchedprofiledata.FirstOrDefault().Phone,
                            postalcode = matchedprofiledata.FirstOrDefault().PostalCode,
                            lu_gender = context.lu_gender.Where(p => p.id == item.ProfileData.GenderID).FirstOrDefault()
                            ,
                            lu_bodytype = context.lu_bodytype.Where(p => p.id == item.ProfileData.BodyTypeID).FirstOrDefault()
                            ,
                            lu_eyecolor = context.lu_eyecolor.Where(p => p.id == item.ProfileData.EyeColorID).FirstOrDefault()
                            ,
                            lu_haircolor = context.lu_haircolor.Where(p => p.id == item.ProfileData.HairColorID).FirstOrDefault()
                            ,
                            lu_diet = context.lu_diet.Where(p => p.id == item.ProfileData.DietID).FirstOrDefault()
                            ,
                            lu_drinks = context.lu_drinks.Where(p => p.id == item.ProfileData.DrinksID).FirstOrDefault()
                            ,
                            lu_exercise = context.lu_exercise.Where(p => p.id == item.ProfileData.ExerciseID).FirstOrDefault()
                            ,
                            lu_humor = context.lu_humor.Where(p => p.id == item.ProfileData.HumorID).FirstOrDefault()
                            ,
                            lu_politicalview = context.lu_politicalview.Where(p => p.id == item.ProfileData.PoliticalViewID).FirstOrDefault()
                            ,
                            lu_religion = context.lu_religion.Where(p => p.id == item.ProfileData.ReligionID).FirstOrDefault()
                            ,
                            lu_religiousattendance = context.lu_religiousattendance.Where(p => p.id == item.ProfileData.ReligiousAttendanceID).FirstOrDefault()
                            ,
                            lu_sign = context.lu_sign.Where(p => p.id == item.ProfileData.SignID).FirstOrDefault()
                            ,
                            lu_smokes = context.lu_smokes.Where(p => p.id == item.ProfileData.SmokesID).FirstOrDefault()
                            ,
                            lu_educationlevel = context.lu_educationlevel.Where(p => p.id == item.ProfileData.EducationLevelID).FirstOrDefault()
                            ,
                            lu_employmentstatus = context.lu_employmentstatus.Where(p => p.id == item.ProfileData.EmploymentSatusID).FirstOrDefault()
                            ,
                            lu_havekids = context.lu_havekids.Where(p => p.id == item.ProfileData.HaveKidsId).FirstOrDefault()
                            ,
                            lu_incomelevel = context.lu_incomelevel.Where(p => p.id == item.ProfileData.IncomeLevelID).FirstOrDefault()
                            ,
                            lu_livingsituation = context.lu_livingsituation.Where(p => p.id == item.ProfileData.LivingSituationID).FirstOrDefault()
                            ,
                            lu_maritalstatus = context.lu_maritalstatus.Where(p => p.id == item.ProfileData.MaritalStatusID).FirstOrDefault()
                            ,
                            lu_profession = context.lu_profession.Where(p => p.id == item.ProfileData.ProfessionID).FirstOrDefault()
                            ,
                            lu_wantskids = context.lu_wantskids.Where(p => p.id == item.ProfileData.WantsKidsID).FirstOrDefault(), ObjectState = ObjectState.Added
                        });
                        //visiblity settings was never implemented anyways.
                        // myprofiledata.visibilitysettings=  context.visibilitysettings.Where(p => p.id   == item.Prof).FirstOrDefault();     


                        //myprofilemetadata.profile_id = myprofile.id;


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
                                profile_id = myprofile.id,ObjectState = ObjectState.Added
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
                               profile_id = myprofile.id,
                               sessionid = logtime.SessionID,
                               ObjectState = ObjectState.Added
                           });
                            Console.WriteLine("profile logintime added succesfull ");
                        }

                        //mail stuff 





                        //add the two new objects to profile
                        //********************************
                        //myprofile.profiledata = myprofiledata;
                        myprofile.profilemetadata = new profilemetadata { profile_id = myprofile.id , ObjectState = ObjectState.Added,profile = null,profiledata = null };
                        myprofile.openids = myopenids;
                        myprofile.userlogtimes = mylogtimes;
                        context.profiles.AddOrUpdate(myprofile);

                        context.SaveChanges();
                       
                      
                    }




                    catch (Exception ex)
                    {

                        var dd = ex.ToString();
                    }
                    finally
                    {
                        //iccrement on faliture too
                        profilecount = profilecount + 1;
                    }
                }



            }

            Console.WriteLine("Total number of profiles proccessed :" + profilecount);
            //attempt bulk save
            try
            {
              //  context.SaveChanges();
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
            context.Configuration.LazyLoadingEnabled = true;
            context.Configuration.ProxyCreationEnabled = true;

            //global try for the rest of objects that are tied to profile
         

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
                        var dd = oldprofile.ProfileData.MailboxFolders;
                        //get mailbox folders first 
                        if (dd.Count !=0)
                        {
                            Console.WriteLine("attempting to create mailbox folders    :" + oldprofile.ProfileID);
                            var mailboxfolder = new mailboxfolder();
                            // List<mailboxfolder> maildboxfolders = new List<mailboxfolder>();
                            //List<mailboxmessagefolder> newmailboxmesages = new List<mailboxmessagefolder>();
                            foreach (MailboxFolder oldfolder in oldprofile.ProfileData.MailboxFolders)
                            {
                                try
                                {
                                    mailboxfolder = null;
                                    //check for matching folder nmae
                                    if ((matchedprofile.profilemetadata == null))
                                    {
                                        Utils.fixmissingprofiledataandmetadata(olddb, context, oldfolder.ProfileID, matchedprofile.id);

                                        matchedprofile = context.profiles.Where(p => p.emailaddress == oldprofile.ProfileID).FirstOrDefault();
                                    }


                                    mailboxfolder = matchedprofile.profilemetadata.mailboxfolders.Where(z => z.displayname.ToUpper() == oldfolder.MailboxFolderTypeName.ToUpper()).FirstOrDefault();

                                    //only create new mailbox folders
                                    if (mailboxfolder == null)
                                    {
                                        mailboxfolder = (new mailboxfolder
                                        {
                                            active = oldfolder.Active,
                                            defaultfolder_id = context.lu_defaultmailboxfolder.Where(p => p.description == oldfolder.MailboxFolderTypeName).FirstOrDefault().id,
                                            profile_id = matchedprofile.id,
                                            creationdate = DateTime.Now,
                                            maxsizeinbytes = 128000,
                                            profilemetadata =null,                                            
                                            displayname = oldfolder.MailboxFolderTypeName,
                                            ObjectState = ObjectState.Added
                                        });

                                       // mailboxfolder.profilemetadata.ObjectState = ObjectState.Unchanged;
                                      //  mailboxfolder.profilemetadata.profiledata.ObjectState = ObjectState.Unchanged;
                                        context.Configuration.ProxyCreationEnabled = false;
                                        context.mailboxfolders.Add(mailboxfolder);

                                        
                                        //save the folder 
                                        context.SaveChanges();
                                        context.Configuration.ProxyCreationEnabled = true;
                                    }


                                    var folderid = mailboxfolder.id;
                                    var MailboxMessagesFoldersToAdd = oldfolder.MailboxMessagesFolders.Where(p => p.MailboxFolderID == oldfolder.MailboxFolderID);
                                    //find all mailbox messages messages tied to this folder for the link table
                                    foreach (MailboxMessagesFolder OldMailBoxMessagesFolder in oldfolder.MailboxMessagesFolders.Where(p => p.MailboxFolderID == oldfolder.MailboxFolderID))
                                    {

                                        mailboxmessage existingmessage = null;
                                        mailboxmessage mailboxmessage = null;


                                        //see if the user is a recpient if so add to recived 
                                        if (OldMailBoxMessagesFolder.MailboxMessage.RecipientID == matchedprofile.emailaddress)
                                        {

                                            existingmessage = context.mailboxmessages.Where(z => z.recipient_id == matchedprofile.id && z.body == OldMailBoxMessagesFolder.MailboxMessage.Body
                                                && z.subject == OldMailBoxMessagesFolder.MailboxMessage.Subject).FirstOrDefault();

                                            if (existingmessage == null)
                                            {

                                                //create the new message 
                                                mailboxmessage = (new mailboxmessage
                                                {
                                                    body = OldMailBoxMessagesFolder.MailboxMessage.Body,
                                                    subject = OldMailBoxMessagesFolder.MailboxMessage.Subject,
                                                    recipient_id = matchedprofile.id,                                                    
                                                    sender_id = context.profiles.Where(z => z.emailaddress == OldMailBoxMessagesFolder.MailboxMessage.SenderID).FirstOrDefault().id,
                                                    creationdate = OldMailBoxMessagesFolder.MailboxMessage.CreationDate,
                                                    ObjectState = ObjectState.Added,
                                                    sizeinbtyes = (OldMailBoxMessagesFolder.MailboxMessage.Body.Length + OldMailBoxMessagesFolder.MailboxMessage.Subject.Length)

                                                });

                                                //check if a matching mail message exists 
                                                                                           
                                                //if sender meatadata and profiledata are not here fix that
                                                if ((context.profiledatas.Where(p=>p.profile_id == mailboxmessage.sender_id).FirstOrDefault() == null))
                                                {
                                                    Utils.fixmissingprofiledataandmetadata(olddb, context, OldMailBoxMessagesFolder.MailboxMessage.SenderID, mailboxmessage.sender_id);
                                                 
                                                }
                                                else if (mailboxmessage.sender_id == null)
                                                {
                                                    Console.WriteLine("missing profile skipping    : " + OldMailBoxMessagesFolder.MailboxMessage.SenderID);
                                                }
                                               

                                               if ((context.profiledatas.Where(p=>p.profile_id == mailboxmessage.sender_id).FirstOrDefault() != null && mailboxmessage.sender_id !=null))
                                               {
                                                    Console.WriteLine("creating mapping btween recived messages and folders for    : " + oldprofile.ProfileID);   
                                                    context.mailboxmessages.Add(mailboxmessage);
                                                    context.SaveChanges();
                                                }


                                                //create the mailboxmesagesflder now
                                            }

                                        }


                                        else if (OldMailBoxMessagesFolder.MailboxMessage.SenderID == matchedprofile.emailaddress)
                                        {

                                            //check if a matching mail message exists 
                                            existingmessage = context.mailboxmessages.Where(z => z.sender_id == matchedprofile.id && z.body == OldMailBoxMessagesFolder.MailboxMessage.Body
                                               && z.subject == OldMailBoxMessagesFolder.MailboxMessage.Subject).FirstOrDefault();

                                            if (existingmessage == null)
                                            {
                                                //create the new message 
                                                mailboxmessage = (new mailboxmessage
                                                {
                                                    body = OldMailBoxMessagesFolder.MailboxMessage.Body,
                                                    subject = OldMailBoxMessagesFolder.MailboxMessage.Subject,
                                                    recipient_id = context.profiles.Where(z => z.emailaddress == OldMailBoxMessagesFolder.MailboxMessage.RecipientID).FirstOrDefault().id,                                                   
                                                    sender_id = matchedprofile.id,
                                                    creationdate = OldMailBoxMessagesFolder.MailboxMessage.CreationDate,
                                                    ObjectState = ObjectState.Added,
                                                    sizeinbtyes = (OldMailBoxMessagesFolder.MailboxMessage.Body.Length + OldMailBoxMessagesFolder.MailboxMessage.Subject.Length)

                                                });

                                                //if sender meatadata and profiledata are not here fix that
                                                if ((context.profiledatas.Where(p => p.profile_id == mailboxmessage.recipient_id).FirstOrDefault() == null))
                                                {
                                                    Utils.fixmissingprofiledataandmetadata(olddb, context, OldMailBoxMessagesFolder.MailboxMessage.RecipientID, mailboxmessage.recipient_id);

                                                }
                                                else if (mailboxmessage.recipient_id == null)
                                                {
                                                    Console.WriteLine("missing profile skipping    : " + OldMailBoxMessagesFolder.MailboxMessage.RecipientID);
                                                }


                                                if ((context.profiledatas.Where(p => p.profile_id == mailboxmessage.recipient_id).FirstOrDefault() != null && mailboxmessage.recipient_id != null))
                                                {
                                                    Console.WriteLine("creating mapping btween sent messages and folders for    : " + oldprofile.ProfileID);
                                                    context.mailboxmessages.Add(mailboxmessage);
                                                    context.SaveChanges();
                                                }
                                            }

                                        }

                                        if (existingmessage == null && mailboxmessage != null)
                                        {
                                            //now do the join table tables 
                                            var newmailboxmesagesfolder = (new mailboxmessagefolder
                                            {

                                                mailboxmessage_id = mailboxmessage.id,
                                                mailboxfolder_id = folderid,
                                                deleted = OldMailBoxMessagesFolder.MessageDeleted == 1 ? true : false,
                                                flagged = OldMailBoxMessagesFolder.MessageFlagged == 1 ? true : false,
                                                draft = OldMailBoxMessagesFolder.MessageDraft == 1 ? true : false,
                                                replied = OldMailBoxMessagesFolder.MessageReplied == 1 ? true : false,
                                                read = OldMailBoxMessagesFolder.MessageRead == 1 ? true : false,
                                                
                                                ObjectState = ObjectState.Added
                                            });

                                            context.mailboxmessagefolders.Add(newmailboxmesagesfolder);
                                            context.SaveChanges();
                                        }
                                        else
                                        {
                                            Console.WriteLine("duplicate message for   : " + oldprofile.ProfileID + " skipping");
                                        }
                                    }


                                }

                                catch (Exception ex)
                                {
                                    var dds = ex.ToString();
                                    Console.WriteLine(ex.ToString());
                                    Console.WriteLine("Critical error press Stop proccessing  Press <Enter> to stop the debugging");
                                    Console.ReadLine();
                                }



                            }
                          
                        }                                               
                        else
                        {
                            Console.WriteLine("No mailbox detected skipping    :" + oldprofile.ProfileID);
                        }


                        //   Console.WriteLine("role added for old profileid of   :" + membersinroleitem.ProfileID);
                        //save data one per row
                    }

                }
                
        

            }

          




           

         //   context.SaveChanges();

        
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
                        membersinroleobject.ObjectState = ObjectState.Added;
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
                        activitylogobject.ObjectState = ObjectState.Added;

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
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Critical error press Stop proccessing  Press <Enter> to stop the debugging");
                Console.ReadLine();
            }

            context.SaveChanges();

        }     
        public static void ConvertProfileMetaActionsToNewFormat()
        {
            var olddb = new AnewluvFTSContext();
            var postaldb = new PostalData2Context();
            var context = new AnewluvContext();


            //global try for the rest of objects that are tied to profile
            try
            {

                //populate collections tied to profilemetadata

                
                //build  members in role data if it exists
                foreach (AnewLuvFTS.DomainAndData.Models.profile oldprofile in olddb.profiles)
                {
                    
                    //query the profile data
                    //var matchedprofile = context.profiles.Where(p => p.emailaddress == membersinroleitem.ProfileID).FirstOrDefault();
                    // Metadata classes are not meant to be instantiated.
                    // myprofile.id = matchedprofile.First().id ;
                    var matchedprofile = context.profiles.Where(p => p.emailaddress == oldprofile.ProfileID).FirstOrDefault();
                    var actionobjects = new List<Anewluv.Domain.Data.action>();
                   //LIKES conversion
                     
                       Console.WriteLine("attempting to create Like actions    :" + oldprofile.ProfileID);
                         
                        //handle Likes this user created first
                        foreach (AnewLuvFTS.DomainAndData.Models.Like Likesitem in olddb.Likes.Where(p=>p.ProfileID == matchedprofile.emailaddress))
                        {                          

                            Console.WriteLine("attempting to add a creator actiontype of Like for the old profileid of    :" + Likesitem.ProfileID);
                            //add the realted proflemetadatas                            
                            var matchedLikeprofilemetadata = context.profilemetadatas.Where(p => p.profile.emailaddress == Likesitem.LikeID).FirstOrDefault();
                            if (matchedLikeprofilemetadata != null)
                            {
                                var action = (new action
                                { creator_profile_id = matchedprofile.id, actiontype_id = (int)actiontypeEnum.Like,  creationdate = Likesitem.LikeDate
                                , viewdate = Likesitem.LikeViewedDate , deletedbycreatordate= Likesitem.DeletedByProfileIDDate, deletedbytargetdate = Likesitem.DeletedByLikeIDDate
                                , active = true , target_profile_id = matchedLikeprofilemetadata.profile_id,  ObjectState = ObjectState.Added
                                    
                                });



                                actionobjects.Add(action);
                               
                               
                                Console.WriteLine("action type of creator like  added for old profileid of    :" + Likesitem.ProfileID);
                            }
                        }

                        Console.WriteLine("attempting to create Peek actions    :" + oldprofile.ProfileID);

                        //handle Peeks this user created first
                        foreach (AnewLuvFTS.DomainAndData.Models.ProfileView Peeksitem in olddb.ProfileViews.Where(p => p.ProfileID == matchedprofile.emailaddress))
                        {

                            Console.WriteLine("attempting to add a creator actiontype of Peek for the old profileid of    :" + Peeksitem.ProfileViewerID);
                            //add the realted proflemetadatas                            
                            var matchedPeekprofilemetadata = context.profilemetadatas.Where(p => p.profile.emailaddress == Peeksitem.ProfileID).FirstOrDefault();
                            if (matchedPeekprofilemetadata != null)
                            {
                                var action = (new action
                                {
                                    creator_profile_id = matchedprofile.id,
                                    actiontype_id = (int)actiontypeEnum.Peek,
                                    creationdate = Peeksitem.ProfileViewDate
                                ,
                                    viewdate = Peeksitem.ProfileViewViewedDate,
                                    deletedbycreatordate = Peeksitem.DeletedByProfileViewerIDDate,
                                    deletedbytargetdate = Peeksitem.DeletedByProfileIDDate
                                     
                                ,
                                    active = true,
                                    target_profile_id = matchedPeekprofilemetadata.profile_id,
                                    ObjectState = ObjectState.Added

                                });
                                actionobjects.Add(action);


                                Console.WriteLine("action type of creator Peek  added for old profileid of    :" + Peeksitem.ProfileID);
                            }
                        }

                      
                       Console.WriteLine("attempting to create Interest actions    :" + oldprofile.ProfileID);

                        //handle Interests this user created first
                        foreach (AnewLuvFTS.DomainAndData.Models.Interest Interestsitem in olddb.Interests.Where(p => p.ProfileID == matchedprofile.emailaddress))
                        {

                            Console.WriteLine("attempting to add a creator actiontype of Interest for the old profileid of    :" + Interestsitem.ProfileID);
                            //add the realted proflemetadatas                            
                            var matchedInterestprofilemetadata = context.profilemetadatas.Where(p => p.profile.emailaddress == Interestsitem.InterestID).FirstOrDefault();
                            if (matchedInterestprofilemetadata != null)
                            {
                                var action = (new action
                                {
                                    creator_profile_id = matchedprofile.id,
                                    actiontype_id = (int)actiontypeEnum.Interest,
                                    creationdate = Interestsitem.InterestDate
                                ,
                                    viewdate = Interestsitem.IntrestViewedDate,
                                    deletedbycreatordate = Interestsitem.DeletedByProfileIDDate,
                                    deletedbytargetdate = Interestsitem.DeletedByInterestIDDate
                                ,
                                    active = true,
                                    target_profile_id = matchedInterestprofilemetadata.profile_id,
                                    ObjectState = ObjectState.Added

                                });
                                actionobjects.Add(action);


                                Console.WriteLine("action type of creator Interest  added for old profileid of    :" + Interestsitem.ProfileID);
                            }
                        }

                        Console.WriteLine("attempting to create Friend actions    :" + oldprofile.ProfileID);

                        //handle Friends this user created first
                        foreach (AnewLuvFTS.DomainAndData.Models.Friend Friendsitem in olddb.Friends.Where(p => p.ProfileID == matchedprofile.emailaddress))
                        {

                            Console.WriteLine("attempting to add a creator actiontype of Friend for the old profileid of    :" + Friendsitem.ProfileID);
                            //add the realted proflemetadatas                            
                            var matchedFriendprofilemetadata = context.profilemetadatas.Where(p => p.profile.emailaddress == Friendsitem.FriendID).FirstOrDefault();
                            if (matchedFriendprofilemetadata != null)
                            {
                                var action = (new action
                                {
                                    creator_profile_id = matchedprofile.id,
                                    actiontype_id = (int)actiontypeEnum.Friend,
                                    creationdate = Friendsitem.FriendDate
                                ,
                                    viewdate = Friendsitem.FriendViewedDate                                    
                                ,
                                    active = true,
                                    target_profile_id = matchedFriendprofilemetadata.profile_id,ObjectState =  ObjectState.Added

                                });
                                actionobjects.Add(action);


                                Console.WriteLine("action type of creator Friend  added for old profileid of    :" + Friendsitem.ProfileID);
                            }
                        }

                        Console.WriteLine("attempting to create Hotlist actions    :" + oldprofile.ProfileID);

                        //handle Hotlists this user created first
                        foreach (AnewLuvFTS.DomainAndData.Models.Hotlist Hotlistsitem in olddb.Hotlists.Where(p => p.ProfileID == matchedprofile.emailaddress))
                        {

                            Console.WriteLine("attempting to add a creator actiontype of Hotlist for the old profileid of    :" + Hotlistsitem.ProfileID);
                            //add the realted proflemetadatas                            
                            var matchedHotlistprofilemetadata = context.profilemetadatas.Where(p => p.profile.emailaddress == Hotlistsitem.HotlistID).FirstOrDefault();
                            if (matchedHotlistprofilemetadata != null)
                            {
                                var action = (new action
                                {
                                    creator_profile_id = matchedprofile.id,
                                    actiontype_id = (int)actiontypeEnum.Hotlist,
                                    creationdate = Hotlistsitem.HotlistDate
                                ,
                                    viewdate = Hotlistsitem.HotlistViewedDate,
                                 
                                 active = true,
                                    target_profile_id = matchedHotlistprofilemetadata.profile_id,
                                    ObjectState = ObjectState.Added

                                });
                                actionobjects.Add(action);


                                Console.WriteLine("action type of creator Hotlist  added for old profileid of    :" + Hotlistsitem.ProfileID);
                            }
                        }


                        Console.WriteLine("attempting to create Favorite actions    :" + oldprofile.ProfileID);

                        //handle Favorites this user created first
                        foreach (AnewLuvFTS.DomainAndData.Models.favorite Favoritesitem in olddb.favorites.Where(p => p.ProfileID == matchedprofile.emailaddress))
                        {

                            Console.WriteLine("attempting to add a creator actiontype of Favorite for the old profileid of    :" + Favoritesitem.ProfileID);
                            //add the realted proflemetadatas                            
                            var matchedFavoriteprofilemetadata = context.profilemetadatas.Where(p => p.profile.emailaddress == Favoritesitem.FavoriteID).FirstOrDefault();
                            if (matchedFavoriteprofilemetadata != null)
                            {
                                var action = (new action
                                {
                                    creator_profile_id = matchedprofile.id,
                                    actiontype_id = (int)actiontypeEnum.Favorite,
                                    creationdate = Favoritesitem.FavoriteDate
                                ,
                                    viewdate = Favoritesitem.FavoriteViewedDate                                
                                ,
                                    active = true,
                                    target_profile_id = matchedFavoriteprofilemetadata.profile_id,
                                    ObjectState = ObjectState.Added

                                });
                                actionobjects.Add(action);


                                Console.WriteLine("action type of creator Favorite  added for old profileid of    :" + Favoritesitem.ProfileID);
                            }
                        }


                        Console.WriteLine("attempting to create Block actions    :" + oldprofile.ProfileID);

                        //handle Blocks this user created first
                        foreach (AnewLuvFTS.DomainAndData.Models.Mailboxblock Blocksitem in olddb.Mailboxblocks.Where(p => p.ProfileID == matchedprofile.emailaddress))
                        {

                            Console.WriteLine("attempting to add a creator actiontype of Block for the old profileid of    :" + Blocksitem.ProfileID);
                            //add the realted proflemetadatas                            
                            var matchedBlockprofilemetadata = context.profilemetadatas.Where(p => p.profile.emailaddress == Blocksitem.BlockID).FirstOrDefault();
                            if (matchedBlockprofilemetadata != null)
                            {
                                var action = (new action
                                {
                                    creator_profile_id = matchedprofile.id,
                                    actiontype_id = (int)actiontypeEnum.Block,
                                    creationdate = Blocksitem.MailboxBlockDate
                                ,
                                 
                                    deletedbycreatordate = Blocksitem.BlockRemovedDate,                                  
                                
                                    active = true,
                                    target_profile_id = matchedBlockprofilemetadata.profile_id,
                                    ObjectState = ObjectState.Added

                                });
                                actionobjects.Add(action);


                                Console.WriteLine("action type of creator Block  added for old profileid of    :" + Blocksitem.ProfileID);
                            }
                        }


 

                    Console.WriteLine("saving  " + actionobjects.Count()  + " actions for profileID :" + oldprofile.ProfileID);
                    matchedprofile.profilemetadata.createdactions = actionobjects;
                    context.SaveChanges();

                }
               

            }
            catch (Exception ex)
            {

                var dd = ex.ToString();
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Critical error press Stop proccessing  Press <Enter> to stop the debugging");
                Console.ReadLine();
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
                        var matchedprofilemetatdata = context.profilemetadatas.Where(p => p.profile.emailaddress == profiledataethnicityitem.ProfileID).FirstOrDefault();
                        if (matchedprofilemetatdata != null)
                        {
                            profiledataethnicityobject.profilemetadata = matchedprofilemetatdata;
                            //context.profilemetadata.Where(p => p.profile.emailaddress == profiledataethnicityitem.ProfileID).FirstOrDefault();
                            profiledataethnicityobject.lu_ethnicity = context.lu_ethnicity.Where(p => p.description.ToUpper() == profiledataethnicityitem.CriteriaAppearance_Ethnicity.EthnicityName.ToUpper()).FirstOrDefault();
                            profiledataethnicityobject.ObjectState = ObjectState.Added; 
                            //add the object to profile object
                            context.profiledata_ethnicity.Add(profiledataethnicityobject);
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
                        var matchedprofilemetatdata = context.profilemetadatas.Where(p => p.profile.emailaddress == profiledatahobbyitem.ProfileID).FirstOrDefault();
                        if (matchedprofilemetatdata != null)
                        {
                            profiledatahobbyobject.profilemetadata = matchedprofilemetatdata;
                            //context.profilemetadata.Where(p => p.profile.emailaddress == profiledatahobbyitem.ProfileID).FirstOrDefault();
                            profiledatahobbyobject.lu_hobby = context.lu_hobby.Where(p => p.description.ToUpper() == profiledatahobbyitem.CriteriaCharacter_Hobby.HobbyName.ToUpper()).FirstOrDefault();
                            profiledatahobbyobject.ObjectState = ObjectState.Added;
                            //add the object to profile object
                            context.profiledata_hobby.Add(profiledatahobbyobject);
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
                        var matchedprofilemetatdata = context.profilemetadatas.Where(p => p.profile.emailaddress == profiledatahotfeatureitem.ProfileID).FirstOrDefault();
                        if (matchedprofilemetatdata != null)
                        {
                            profiledatahotfeatureobject.profilemetadata = matchedprofilemetatdata;
                            //context.profilemetadata.Where(p => p.profile.emailaddress == profiledatahotfeatureitem.ProfileID).FirstOrDefault();
                            profiledatahotfeatureobject.lu_hotfeature = context.lu_hotfeature.Where(p => p.description.ToUpper() == profiledatahotfeatureitem.CriteriaCharacter_HotFeature.HotFeatureName.ToUpper()).FirstOrDefault();
                            profiledatahotfeatureobject.ObjectState = ObjectState.Added;
                            //add the object to profile object
                            context.profiledata_hotfeature.Add(profiledatahotfeatureobject);
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
                        var matchedprofilemetatdata = context.profilemetadatas.Where(p => p.profile.emailaddress == profiledatalookingforitem.ProfileID).FirstOrDefault();
                        if (matchedprofilemetatdata != null)
                        {
                           

                            profiledatalookingforobject.profilemetadata = matchedprofilemetatdata;
                            //context.profilemetadata.Where(p => p.profile.emailaddress == profiledatalookingforitem.ProfileID).FirstOrDefault();
                            profiledatalookingforobject.lu_lookingfor = context.lu_lookingfor.Where(p => p.description == profiledatalookingforitem.CriteriaLife_LookingFor.LookingForName.ToUpper()).FirstOrDefault();
                            profiledatalookingforobject.ObjectState = ObjectState.Added;
                            //add the object to profile object
                            context.profiledata_lookingfor.Add(profiledatalookingforobject);
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
          
            //global try for the rest of objects that are tied to profile
            try
            {

               //build  members in role data if it exists
                foreach (AnewLuvFTS.DomainAndData.Models.profile oldprofile in olddb.profiles)
                {

                    //query the profile data
                    //var matchedprofile = context.profiles.Where(p => p.emailaddress == membersinroleitem.ProfileID).FirstOrDefault();
                    // Metadata classes are not meant to be instantiated.
                    // myprofile.id = matchedprofile.First().id ;
                    var matchedprofile = context.profiles.Where(p => p.emailaddress == oldprofile.ProfileID).FirstOrDefault();
                    
                    //LIKES conversion                     
               

                    //handle Likes this user created first
                    foreach (AnewLuvFTS.DomainAndData.Models.SearchSetting oldsearchsetting in olddb.SearchSettings.Where(p => p.ProfileID == matchedprofile.emailaddress))
                    {
                            var searchsettingobject = new Anewluv.Domain.Data.searchsetting();
                            var searchsettingsdetails = new List<Anewluv.Domain.Data.searchsettingdetail>();
                            Console.WriteLine("attempting a search setting for the old profileid of    :" + oldsearchsetting.ProfileID);

                            if (context.searchsettings.Any(p => p.profilemetadata.profile.emailaddress == oldsearchsetting.ProfileID))
                            {
                                Console.WriteLine("skipping profile with email  :" + oldsearchsetting.ProfileID + "it alaready has search settings   ");
                            }
                            else
                            {

                                //add the realted proflemetadatas 
                                //searchsettingobject.profilemetadata = context.profilemetadatas.Where(p => p.profile.emailaddress == searchsettingitem.ProfileID).FirstOrDefault();

                                if (searchsettingobject.profilemetadata != null)
                                {
                                    searchsettingobject.agemax = oldsearchsetting.AgeMax;
                                    searchsettingobject.agemin = oldsearchsetting.AgeMin;
                                    searchsettingobject.creationdate = oldsearchsetting.CreationDate;
                                    searchsettingobject.distancefromme = oldsearchsetting.DistanceFromMe;
                                    searchsettingobject.heightmax = oldsearchsetting.HeightMin;
                                    searchsettingobject.heightmin = oldsearchsetting.HeightMin;
                                    searchsettingobject.lastupdatedate = oldsearchsetting.LastUpdateDate;
                                    searchsettingobject.myperfectmatch = oldsearchsetting.MyPerfectMatch;
                                    searchsettingobject.savedsearch = oldsearchsetting.SavedSearch;
                                    searchsettingobject.searchname = oldsearchsetting.SearchName;
                                    searchsettingobject.searchrank = oldsearchsetting.SearchRank;
                                    searchsettingobject.systemmatch = oldsearchsetting.SystemMatch;
                                    searchsettingobject.ObjectState = ObjectState.Added;
                                    //add the object to profile object
                                    context.searchsettings.AddOrUpdate(searchsettingobject);
                                    counter = counter + 1;
                                    //save data one per row
                                    context.SaveChanges();
                                    Console.WriteLine("added a search setting for the old profileid of    :" + oldsearchsetting.ProfileID);
                                }
                            }


                         //add related collections

                        
                            foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_Location searchsettinglocationitem in oldsearchsetting.SearchSettings_Location)
                            {
                                if (searchsettinglocationitem != null)
                                {
                                    Console.WriteLine("attempting a search setting location for the old profileid of    :" + searchsettinglocationitem.SearchSetting.ProfileID);
                                    var searchsettinglocationobject = new Anewluv.Domain.Data.searchsetting_location();
                                    if (context.searchsetting_location.Any(p => p.countryid == searchsettinglocationitem.CountryID))
                                    {
                                        Console.WriteLine("skipping profile with email  :" + searchsettinglocationitem.SearchSetting.ProfileID + "it alaready has search settings location   ");
                                    }
                                    else
                                    {
                                        var matchedsearchsetting = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == searchsettinglocationitem.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsetting != null)
                                        {
                                            searchsettinglocationobject.searchsetting = matchedsearchsetting;
                                            searchsettinglocationobject.countryid = searchsettinglocationitem.CountryID;  //context.lu_location.Where(p => p.id == searchsettinglocationitem.locationsID).FirstOrDefault();
                                            searchsettinglocationobject.postalcode = searchsettinglocationitem.PostalCode;
                                            searchsettinglocationobject.ObjectState = ObjectState.Added;
                                            context.searchsetting_location.Add(searchsettinglocationobject);
                                            //save data one per row
                                            Console.WriteLine("added a search setting location for the old profileid of    :" + searchsettinglocationitem.SearchSetting.ProfileID);
                                            counter = counter + 1;                                      
                                        }
                                    }
                                }

                                  context.SaveChanges();
                            }
                            
                             
 
                        //Details go here now !

                         

                                //create the searchsetting detail stuff for bodytype
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_BodyTypes bodytype in oldsearchsetting.SearchSettings_BodyTypes)
                                {
                                    if (bodytype != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  bodytype  for the old profileid of    :" + bodytype.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == bodytype.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.bodytype,                                        
                                                creationdate = DateTime.Now, 
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added, value = bodytype.BodyTypesID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of bodytype like  added for old profileid of    :" + bodytype.SearchSetting.ProfileID);
                                        }
                                    }
                          
                                }




                                //create the searchsetting detail stuff for Diet
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_Diet Diet in oldsearchsetting.SearchSettings_Diet)
                                {
                                    if (Diet != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  Diet  for the old profileid of    :" + Diet.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == Diet.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.diet,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = Diet.DietID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of Diet like  added for old profileid of    :" + Diet.SearchSetting.ProfileID);
                                        }
                                    }

                                }



                                //create the searchsetting detail stuff for Drink
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_Drinks Drink in oldsearchsetting.SearchSettings_Drinks)
                                {
                                    if (Drink != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  Drink  for the old profileid of    :" + Drink.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == Drink.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.drink,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = Drink.DrinksID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of Drink like  added for old profileid of    :" + Drink.SearchSetting.ProfileID);
                                        }
                                    }

                                }



                                //create the searchsetting detail stuff for Educationlevel
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_EducationLevel Educationlevel in oldsearchsetting.SearchSettings_EducationLevel)
                                {
                                    if (Educationlevel != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  Educationlevel  for the old profileid of    :" + Educationlevel.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == Educationlevel.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.educationlevel,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = Educationlevel.EducationLevelID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of Educationlevel like  added for old profileid of    :" + Educationlevel.SearchSetting.ProfileID);
                                        }
                                    }

                                }


                                //create the searchsetting detail stuff for EmploymentStatus
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_EmploymentStatus EmploymentStatus in oldsearchsetting.SearchSettings_EmploymentStatus)
                                {
                                    if (EmploymentStatus != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  EmploymentStatus  for the old profileid of    :" + EmploymentStatus.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == EmploymentStatus.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.employmentstatus,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = EmploymentStatus.EmploymentStatusID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of EmploymentStatus like  added for old profileid of    :" + EmploymentStatus.SearchSetting.ProfileID);
                                        }
                                    }

                                }



                                //create the searchsetting detail stuff for Ethnicity
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_Ethnicity Ethnicity in oldsearchsetting.SearchSettings_Ethnicity)
                                {
                                    if (Ethnicity != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  Ethnicity  for the old profileid of    :" + Ethnicity.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == Ethnicity.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.ethnicity,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = Ethnicity.EthicityID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of Ethnicity like  added for old profileid of    :" + Ethnicity.SearchSetting.ProfileID);
                                        }
                                    }

                                }



                                //create the searchsetting detail stuff for excercise
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_Exercise excercise in oldsearchsetting.SearchSettings_Exercise)
                                {
                                    if (excercise != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  excercise  for the old profileid of    :" + excercise.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == excercise.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.excercise,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = excercise.ExerciseID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of excercise like  added for old profileid of    :" + excercise.SearchSetting.ProfileID);
                                        }
                                    }

                                }



                                //create the searchsetting detail stuff for eyecolor
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_EyeColor eyecolor in oldsearchsetting.SearchSettings_EyeColor)
                                {
                                    if (eyecolor != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  eyecolor  for the old profileid of    :" + eyecolor.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == eyecolor.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.eyecolor,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = eyecolor.EyeColorID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of eyecolor like  added for old profileid of    :" + eyecolor.SearchSetting.ProfileID);
                                        }
                                    }

                                }



                                //create the searchsetting detail stuff for gender
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_Genders gender in oldsearchsetting.SearchSettings_Genders)
                                {
                                    if (gender != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  gender  for the old profileid of    :" + gender.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == gender.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.gender,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = gender.GenderID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of gender like  added for old profileid of    :" + gender.SearchSetting.ProfileID);
                                        }
                                    }

                                }



                                //create the searchsetting detail stuff for haircolor
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_HairColor haircolor in oldsearchsetting.SearchSettings_HairColor)
                                {
                                    if (haircolor != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  haircolor  for the old profileid of    :" + haircolor.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == haircolor.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.haircolor,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = haircolor.HairColorID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of haircolor like  added for old profileid of    :" + haircolor.SearchSetting.ProfileID);
                                        }
                                    }

                                }



                                //create the searchsetting detail stuff for havekids
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_HaveKids havekids in oldsearchsetting.SearchSettings_HaveKids)
                                {
                                    if (havekids != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  havekids  for the old profileid of    :" + havekids.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == havekids.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.havekids,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = havekids.HaveKidsID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of havekids like  added for old profileid of    :" + havekids.SearchSetting.ProfileID);
                                        }
                                    }

                                }



                                //create the searchsetting detail stuff for hobby
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_Hobby hobby in oldsearchsetting.SearchSettings_Hobby)
                                {
                                    if (hobby != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  hobby  for the old profileid of    :" + hobby.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == hobby.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.hobby,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = hobby.HobbyID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of hobby like  added for old profileid of    :" + hobby.SearchSetting.ProfileID);
                                        }
                                    }

                                }


                                //create the searchsetting detail stuff for hotfeature
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_HotFeature hotfeature in oldsearchsetting.SearchSettings_HotFeature)
                                {
                                    if (hotfeature != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  hotfeature  for the old profileid of    :" + hotfeature.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == hotfeature.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.hotfeature,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = hotfeature.HotFeatureID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of hotfeature like  added for old profileid of    :" + hotfeature.SearchSetting.ProfileID);
                                        }
                                    }

                                }



                                //create the searchsetting detail stuff for humor
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_Humor humor in oldsearchsetting.SearchSettings_Humor)
                                {
                                    if (humor != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  humor  for the old profileid of    :" + humor.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == humor.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.humor,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = humor.HumorID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of humor like  added for old profileid of    :" + humor.SearchSetting.ProfileID);
                                        }
                                    }

                                }



                                //create the searchsetting detail stuff for incomelevel
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_IncomeLevel incomelevel in oldsearchsetting.SearchSettings_IncomeLevel)
                                {
                                    if (incomelevel != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  incomelevel  for the old profileid of    :" + incomelevel.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == incomelevel.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.incomelevel,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = incomelevel.ImcomeLevelID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of incomelevel like  added for old profileid of    :" + incomelevel.SearchSetting.ProfileID);
                                        }
                                    }

                                }



                                //create the searchsetting detail stuff for livingsituation
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_LivingStituation livingsituation in oldsearchsetting.SearchSettings_LivingStituation)
                                {
                                    if (livingsituation != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  livingsituation  for the old profileid of    :" + livingsituation.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == livingsituation.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.livingsituation,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = livingsituation.LivingStituationID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of livingsituation like  added for old profileid of    :" + livingsituation.SearchSetting.ProfileID);
                                        }
                                    }

                                }



                                //create the searchsetting detail stuff for lookingfor
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_LookingFor lookingfor in oldsearchsetting.SearchSettings_LookingFor)
                                {
                                    if (lookingfor != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  lookingfor  for the old profileid of    :" + lookingfor.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == lookingfor.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.lookingfor,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = lookingfor.LookingForID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of lookingfor like  added for old profileid of    :" + lookingfor.SearchSetting.ProfileID);
                                        }
                                    }

                                }


                                //create the searchsetting detail stuff for maritialstatus
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_MaritalStatus maritialstatus in oldsearchsetting.SearchSettings_MaritalStatus)
                                {
                                    if (maritialstatus != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  maritialstatus  for the old profileid of    :" + maritialstatus.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == maritialstatus.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.maritialstatus,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = maritialstatus.MaritalStatusID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of maritialstatus like  added for old profileid of    :" + maritialstatus.SearchSetting.ProfileID);
                                        }
                                    }

                                }



                                //create the searchsetting detail stuff for politicalview
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_PoliticalView politicalview in oldsearchsetting.SearchSettings_PoliticalView)
                                {
                                    if (politicalview != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  politicalview  for the old profileid of    :" + politicalview.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == politicalview.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.politicalview,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = politicalview.PoliticalViewID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of politicalview like  added for old profileid of    :" + politicalview.SearchSetting.ProfileID);
                                        }
                                    }

                                }



                                //create the searchsetting detail stuff for profession
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_Profession profession in oldsearchsetting.SearchSettings_Profession)
                                {
                                    if (profession != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  profession  for the old profileid of    :" + profession.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == profession.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.profession,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = profession.ProfessionID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of profession like  added for old profileid of    :" + profession.SearchSetting.ProfileID);
                                        }
                                    }

                                }



                                //create the searchsetting detail stuff for religion
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_Religion religion in oldsearchsetting.SearchSettings_Religion)
                                {
                                    if (religion != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  religion  for the old profileid of    :" + religion.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == religion.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.religion,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = religion.ReligionID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of religion like  added for old profileid of    :" + religion.SearchSetting.ProfileID);
                                        }
                                    }

                                }



                                //create the searchsetting detail stuff for religiousattendance
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_ReligiousAttendance religiousattendance in oldsearchsetting.SearchSettings_ReligiousAttendance)
                                {
                                    if (religiousattendance != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  religiousattendance  for the old profileid of    :" + religiousattendance.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == religiousattendance.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.religiousattendance,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = religiousattendance.ReligiousAttendanceID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of religiousattendance like  added for old profileid of    :" + religiousattendance.SearchSetting.ProfileID);
                                        }
                                    }

                                }



                                //create the searchsetting detail stuff for showme
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_ShowMe showme in oldsearchsetting.SearchSettings_ShowMe)
                                {
                                    if (showme != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  showme  for the old profileid of    :" + showme.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == showme.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.showme,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = showme.ShowMeID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of showme like  added for old profileid of    :" + showme.SearchSetting.ProfileID);
                                        }
                                    }

                                }


                                //create the searchsetting detail stuff for sign
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_Sign sign in oldsearchsetting.SearchSettings_Sign)
                                {
                                    if (sign != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  sign  for the old profileid of    :" + sign.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == sign.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.sign,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = sign.SignID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of sign like  added for old profileid of    :" + sign.SearchSetting.ProfileID);
                                        }
                                    }

                                }



                                //create the searchsetting detail stuff for smokes
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_Smokes smokes in oldsearchsetting.SearchSettings_Smokes)
                                {
                                    if (smokes != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  smokes  for the old profileid of    :" + smokes.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == smokes.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.smokes,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = smokes.SmokesID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of smokes like  added for old profileid of    :" + smokes.SearchSetting.ProfileID);
                                        }
                                    }

                                }



                                //create the searchsetting detail stuff for sortbytype
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_SortByType sortbytype in oldsearchsetting.SearchSettings_SortByType)
                                {
                                    if (sortbytype != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  sortbytype  for the old profileid of    :" + sortbytype.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == sortbytype.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.sortbytype,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = sortbytype.SortByTypeID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of sortbytype like  added for old profileid of    :" + sortbytype.SearchSetting.ProfileID);
                                        }
                                    }

                                }



                                //create the searchsetting detail stuff for wantskids
                                foreach (AnewLuvFTS.DomainAndData.Models.SearchSettings_WantKids wantskids in oldsearchsetting.SearchSettings_WantKids)
                                {
                                    if (wantskids != null)
                                    {
                                        Console.WriteLine("attempting to add a searchsetting detail of  wantskids  for the old profileid of    :" + wantskids.SearchSetting.ProfileID);
                                        //add the realted proflemetadatas                            
                                        var matchedsearchsettingobject = context.searchsettings.Where(p => p.profilemetadata.profile.emailaddress == wantskids.SearchSetting.ProfileID).FirstOrDefault();
                                        if (matchedsearchsettingobject != null)
                                        {
                                            var detail = (new searchsettingdetail
                                            {
                                                searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.wantskids,
                                                creationdate = DateTime.Now,
                                                active = true,
                                                searchsetting_id = matchedsearchsettingobject.id,
                                                ObjectState = ObjectState.Added,
                                                value = wantskids.WantKidsID.GetValueOrDefault()

                                            });
                                            searchsettingsdetails.Add(detail);
                                            Console.WriteLine("search detail type of wantskids like  added for old profileid of    :" + wantskids.SearchSetting.ProfileID);
                                        }
                                    }

                                }





                      






                        Console.WriteLine("saving  a total of : " + counter + " searchsettinglocations");
                        searchsettingobject.details = searchsettingsdetails;
                        context.SaveChanges();



                    }

                }


            }
            catch (Exception ex)
            {
               // var mytest = searchsettinggenderobjecttest.searchsetting;
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
