

namespace Dating.Server.Data.Services
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;

    using Dating.Server.Data;
    using Dating.Server.Data.Services;
    using Dating.Server.Data.Models;

    using System.Data.EntityClient;
    using System.Collections.ObjectModel;
    using System.Security.Principal;

    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using System.ServiceModel.DomainServices.Server.ApplicationServices;
    using Common;

    using System.Globalization;



    //=======================================================
    //Service provided by Telerik (www.telerik.com)
    //Conversion powered by NRefactory.
    //Twitter: @telerik, @toddanglin
    //Facebook: facebook.com/telerik
    //=======================================================


    // this membership serverice is for MVC
    public class AnewLuvMembershipProvider :  MembershipProvider
    {

        //constant strings for reseting passwords
        const String UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; const String LOWER = "abcdefghijklmnopqrstuvwxyz"; const String NUMBERS = "1234567890"; const String SPECIAL = "*$-+?&=!%/";
        AnewLuvFTSEntities context = new AnewLuvFTSEntities();
        DatingService datingService = new DatingService();

        IQueryable<profile> myQuery = default(IQueryable<profile>);

       


        public override bool ValidateUser(string username, string password)
        {

            
           
            //first you have to get the encrypted sctring by email address and username 
            string encryptedPassword = "";
            //get profile created date
            DateTime creationdate;
            DateTime? passwordchangedate;
            DateTime EncrpytionChangeDate = new DateTime(2011, 8, 3, 4, 5, 0);
            string decryptedPassword;
            string actualpasswordstring;

            //Dim ctx As New Entities()
            //added profile status ID validation as well i.e 2 for activated and is not banned 
            myQuery = context.profiles.Where(p => p.UserName == username && p.ProfileStatusID == 2 );

            

            if (myQuery.Count() > 0)
            {
                //retirve encypted password
                encryptedPassword = myQuery.FirstOrDefault().Password;
                creationdate = myQuery.FirstOrDefault().CreationDate;
                passwordchangedate = myQuery.FirstOrDefault().PasswordChangedDate;
            }
            else
            {
                return false;
            }

            //case for if the user account was created before encrpyption algorithm was changed
            //compare the dates
            int resultcreateddate = DateTime.Compare(creationdate,EncrpytionChangeDate);
            int resultpasswordchangedate = DateTime.Compare(passwordchangedate.GetValueOrDefault() , EncrpytionChangeDate);
            if (resultpasswordchangedate > 0 |  resultcreateddate > 0)
            //use new decryption method
            {
               
                decryptedPassword = Common.Encryption.decryptString (encryptedPassword);
                actualpasswordstring = password;            
            }
            else
            {
                decryptedPassword = Common.Encryption.Decrypt(encryptedPassword, password);
                actualpasswordstring = username.ToUpper() + Common.Encryption.EncryptionKey;            
            }




            

            //check if decrypted string macthed username to upper  + secret
            if (actualpasswordstring == decryptedPassword)
            {

            
            //log the user logtime here so it is common to silverlight and MVC
            datingService.UpdateUserLoginTime(username, HttpContext.Current.Session.SessionID);
                //also update the profiledata for the last login date


            return true;
            }
            else
            {
            return false;
            }





        }
    

        //5-82012 updated to only valudate username
        //overide for validate user that uses just the username, this can be used for pass through auth where a user was already prevalidated via another method
        public  bool ValidateUser(string username)
        {
            
            using (AnewLuvFTSEntities context = new AnewLuvFTSEntities())
            {


                //test code here 
               // dynamic user = from p in context.profiles
                //               where p.UserName == username
                 //              select new { p.ScreenName };
                //end test code


                //use the encyrption service in common
                //dynamic user = context.profiles.Where(u => u.UserName == username && u.Password == Common.Encryption.EncodePasswordWithSalt(password, username).FirstOrDefault());
                // Return user IsNot Nothing
                //string encryptedPassword = Common.Encryption.EncodePasswordWithSalt(password, username);
                IQueryable<profile> myQuery = default(IQueryable<profile>);
                //Dim ctx As New Entities()
                myQuery = context.profiles.Where(p => p.UserName == username);//&& p.ProfileStatusID == 2);


                if (myQuery.Count() > 0)
                {
                    //log the user logtime here so it is common to silverlight and MVC
                    datingService.UpdateUserLoginTime(username, HttpContext.Current.Session.SessionID);            

                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
                        
        public  bool ValidateUser(string VerifedEmail, string openidIdentifer,string openidProvidername)
        {


            //open ID members are already verifed but it is posublethat a member who is not activated tries to use open ID
            //so they could be in order status 1
           var  myprofile = context.profiles.Where(p => p.ProfileID == VerifedEmail && p.ProfileStatusID  <= 2).FirstOrDefault();

            //check for the openIDidenfier , to see if it was used before , if it was do nothing but normal updates for user
           var myopenIDstore = myprofile.profileOpenIDStores.Where(p => p.openidIdentifier == openidIdentifer && p.openidProviderName == openidProvidername && p.active == true).FirstOrDefault();

           //if we found an openID store for this type
           if (myopenIDstore == null && myprofile != null)
           //add the openID provider if its a new one
           {
               datingService.AddNewOpenIDForProfile(VerifedEmail, openidIdentifer, openidProvidername);
           }

            //first you have to get the encrypted sctring by email address and username 
           // string encryptedopenidIdentifer = "";
            //get profile created date
            //DateTime creationdate;
           // DateTime? passwordchangedate;
            //DateTime EncrpytionChangeDate = new DateTime(2011, 8, 3, 4, 5, 0);        

            //Dim ctx As New Entities()
            //added profile status ID validation as well i.e 2 for activated and is not banned 
           // myQuery = context.profiles.Where(p => p.ProfileID == VerifedEmail && p.ProfileStatusID == 2);
            if (myprofile != null)
            {
               datingService.UpdateUserLoginTimeByProfileID (VerifedEmail, HttpContext.Current.Session.SessionID);
               return true;
            }
            else
            {
                return false;
            }
                     
            
          
           // datingService.UpdateUserLoginTimeByProfileID(VerifedEmail, HttpContext.Current.Session.SessionID);
             

            //    return false;
           // }





        }


        //Public Overrides Property ApplicationName() As String
        //    Get
        //        Return "TaskManager"
        //    End Get
        //    Set(ByVal value As String)
        //    End Set
        //End Property


        public override string ApplicationName
        {

           
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

     

        public override MembershipUser CreateUser(string username,
           string password,
           string email,
           string securityQuestion,
                  string securityAnswer,
           bool isApproved,
           object providerUserKey,
           out MembershipCreateStatus status)
        {
            //set default values for a basic memeber created with just password,
            //sec question and aswers
            return this.CreateUserCustom(username, password,"","",
                  email,
                 //  securityQuestion,
                 //  securityAnswer
                 DateTime.Now,
                  "","", "", "", null,null,"","","",
                  false ,
                  null,
                  out status);
        }

        public  AnewLuvMembershipUser  CreateUserCustom(string username,
                   string password,string openidIdentifer,string openidProvidername,
                  string email,
                //  string securityQuestion,
                 // string securityAnswer,
                  DateTime birthdate, string gender, string country, string city,string stateprovince,double? longitude,double? lattitude, string screenname, string zippostalcode,string activationcode,
                  bool isApproved,
                  object providerUserKey,
                 out MembershipCreateStatus status)

        {

           

           AnewLuvMembershipUser  newUser = null;
            
            try
            {
               // AnewLuvFTSEntities  mydb = new  AnewLuvFTSEntities();


                using (AnewLuvFTSEntities  dbContext = new  AnewLuvFTSEntities())
                {
                  

               profile ObjProfileEntity = new profile();
               ProfileData objprofileDataEntity = new ProfileData();
               //TO DO new entity for OPEN ID data
               int countryID = 0;
               Random objRandom = new Random();  
               int intStart = objRandom.Next(1, 9);
               int intLastTwo = objRandom.Next(10, 99);
               //convert the string values to byte as needed
               //NumberFormatInfo provider = new NumberFormatInfo();
               // These properties affect the conversion.
               //provider.PositiveSign = "pos";


               //conver the unquiqe coountry Name to an ID
                using (PostalDataService postaldbContext = new PostalDataService())
                {  //store country ID for use later
                    countryID = _georepository .GetCountryIdByCountryName(country);               
                }

                //split up the city from state province
                var tempCityAndStateProvince = city.Split(',');
             
                //Build the profile data table
                objprofileDataEntity.ProfileID = email;
                objprofileDataEntity.Latitude = lattitude ;
                objprofileDataEntity.Longitude = longitude; //_GpsData.Longitude;
                objprofileDataEntity.City = tempCityAndStateProvince[0];
                objprofileDataEntity.Country_Region = "NA";

                objprofileDataEntity.State_Province = ((tempCityAndStateProvince.Count() > 1)) ?  tempCityAndStateProvince[1] : "NA" ;

                objprofileDataEntity.CountryID = countryID;
                objprofileDataEntity.PostalCode = zippostalcode;
                 
                 objprofileDataEntity.GenderID = Int32.Parse(gender);
                 
                
             //  =  Int32.Parse(gender): objprofileDataEntity.gender.GenderName  = gender;
                objprofileDataEntity.Birthdate = birthdate;
                objprofileDataEntity.Phone = "NA";
                objprofileDataEntity.AboutMe = "Hello";



                //set all the entity values
                ObjProfileEntity.UserName = username;
                //changed the encryption to something stronger
                //make username upper so that we can get actual mateches withoute user having to type in a case sensitive username
                    ObjProfileEntity.Password = (openidIdentifer !="") ?Common.Encryption.encryptString(password) : openidIdentifer;
                ObjProfileEntity.ProfileID = email;
                ObjProfileEntity.ScreenName = screenname;
                //need to add a new feild
                ObjProfileEntity.ActivationCode = activationcode;
                //Mid(intStart, intStart, 14) & CStr(intLastTwo) 'need to beef this up with the session variable
                ObjProfileEntity.CreationDate = System.DateTime.Now;
                ObjProfileEntity.ModificationDate = System.DateTime.Now;
                ObjProfileEntity.LoginDate = System.DateTime.Now;
                // fix this to null
                ObjProfileEntity.ForwardMessages = 1;
              //  ObjProfileEntity.SecurityQuestionID = 1;                
               // ObjProfileEntity.SecurityAnswer =  securityAnswer;
              ObjProfileEntity.ProfileStatusID = (openidIdentifer ==  "" ||  openidIdentifer == null)?   1 : 2;  //auto activate profiles fi we have an openID user since we have verifed thier info

                //TOD DO add open ID identifier as well Profider type to ssoProvider table 

                  dbContext.AddToProfileDatas(objprofileDataEntity);
                  dbContext.AddToprofiles(ObjProfileEntity);

                  dbContext.SaveChanges();
                  
                }




                status = MembershipCreateStatus.Success;
            }
            catch (Exception ex)
            {
                status = MembershipCreateStatus.ProviderError;
                newUser = null;
                throw ex;
            }
            
            return newUser;
        }


        public override string ResetPassword(string profileID, string answer)
        {


            return this.ResetPasswordCustom(profileID);
             
        }

        //handles reseting password duties.  First verifys that security uqestion was correct for the profile ID, the generated a password
        // using the local generatepassword method the send the encyrpted passwoerd and profile ID to the dating service so it can be updated in the DB
        //finally returns the new password to the calling functon or an empty string if failure.
        public string ResetPasswordCustom(string profileid)
        {

          //  if (securityquestionID == null) return "";

          // var username = datingService.ValidateSecurityAnswerIsCorrect(profileid, securityquestionID.GetValueOrDefault(), answer);
             var username = datingService.GetUserNamebyProfileID(profileid);
            var generatedpassword = "";
            if (username != "")
            {
                //we have the generated password now update the user's account with new password

                generatedpassword = GeneratePassword();
                datingService.UpdatePassword(profileid, Common.Encryption.encryptString(generatedpassword));


                //'reset the password 
                return generatedpassword;
            }
            // throw new NotImplementedException();
            return "";
        }



        public override void UpdateUser(MembershipUser user)
        {



            AnewLuvMembershipUser u = (AnewLuvMembershipUser)user;
            try
            {
                
                // AnewLuvFTSEntities  mydb = new  AnewLuvFTSEntities();
                using (AnewLuvFTSEntities dbContext = new AnewLuvFTSEntities())
                {
                   // dbContext.Dispose();

                    //get profile and profile datas
                    profile ObjProfileEntity = dbContext.profiles.Where(p => p.ProfileID == u.Email).FirstOrDefault();
                    ProfileData objprofileDateEntity = dbContext.ProfileDatas.Where(p => p.ProfileID == u.Email).FirstOrDefault();
                    
                    GpsData _GpsData = new GpsData();

                    string[] tempCityAndStateProvince = u.city.Split(',');
                    int countryID;
                    Random objRandom = new Random();
                    int intStart = objRandom.Next(1, 9);
                    int intLastTwo = objRandom.Next(10, 99);
                    //convert the string values to byte as needed
                    //NumberFormatInfo provider = new NumberFormatInfo();
                    // These properties affect the conversion.
                    //provider.PositiveSign = "pos";


                    //conver the unquiqe coountry Name to an ID
                    using (PostalDataService postaldbContext = new PostalDataService())
                    {
                        //store country ID for use later
                        countryID = postaldbContext.GetCountryIdByCountryName(u.country);

                        //get the longidtue and latttude 
                        _GpsData = postaldbContext.GetGpsDataSingleByCityCountryAndPostalCode(u.country, tempCityAndStateProvince[0], u.ziporpostalcode);


                    }

                    //split up the city from state province
                    //Build the profile data table                   
                    objprofileDateEntity.Latitude = _GpsData.Latitude;
                    objprofileDateEntity.Longitude = _GpsData.Longitude;
                    objprofileDateEntity.City = tempCityAndStateProvince[0];
                    objprofileDateEntity.Country_Region = "NA";

                    if (tempCityAndStateProvince.Count() == 2)
                    {
                        objprofileDateEntity.State_Province = (string.IsNullOrEmpty(tempCityAndStateProvince[1])) ? "NA" : tempCityAndStateProvince[1];
                    }
                    else 
                    {
                        objprofileDateEntity.State_Province = "NA";
                    }

                    objprofileDateEntity.CountryID = countryID;
                    objprofileDateEntity.PostalCode = u.ziporpostalcode;
                    objprofileDateEntity.GenderID = Int32.Parse(u.gender);
                    objprofileDateEntity.Birthdate = u.birthdate;
                    objprofileDateEntity.Phone = "NA";
                    //objprofileDateEntity.AboutMe = "Hello";



                    //set all the entity values
                    // ObjProfileEntity.UserName = username;
                    //changed the encryption to something stronger

                    //only update password if it changed
                    if (u.password != null)
                    {
                        ObjProfileEntity.Password = Common.Encryption.encryptString(u.password);
                        ObjProfileEntity.PasswordChangedDate = DateTime.Now;
                        ObjProfileEntity.PasswordChangedCount = (ObjProfileEntity.PasswordChangedCount == null) ? 1 : ObjProfileEntity.PasswordChangedCount+1;
                    }
                    // ObjProfileEntity.ProfileID = email;
                    // ObjProfileEntity.ScreenName = screenname;
                    //need to add a new feild
                    // ObjProfileEntity.ActivationCode = Common.Encryption.EncodeString(email + screenname);
                    //Mid(intStart, intStart, 14) & CStr(intLastTwo) 'need to beef this up with the session variable
                    //ObjProfileEntity.CreationDate = System.DateTime.Now;
                    ObjProfileEntity.ModificationDate = System.DateTime.Now;
                    // ObjProfileEntity.LoginDate = System.DateTime.Now;
                    // fix this to null
                    //ObjProfileEntity.ForwardMessages = 1;
                  //  ObjProfileEntity.SecurityQuestionID = System.Convert.ToByte(u.securityquestion );
                   //   ObjProfileEntity.SecurityAnswer = u.securityanswer;
                    //ObjProfileEntity.ProfileStatusID = 1;



                    //dbContext.AddToProfileDatas(objprofileDateEntity);
                   // dbContext.AddToprofiles(ObjProfileEntity);

                    dbContext.SaveChanges();

                }




                // status = MembershipCreateStatus.Success;
            }
            catch (Exception ex)
            {
                // status = MembershipCreateStatus.ProviderError;
                //  newUser = null;
                throw ex;
            }






        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return this.GetUserCustom(username, userIsOnline);
        }

        //custom remapped membership get user function
        public AnewLuvMembershipUser GetUserCustom(string username, bool userIsOnline)
        {
            Object providerUserKey = null;  //we dont use this
            profile u = new profile();

            //using (AnewLuvFTSEntities context = new AnewLuvFTSEntities())
            //{

            
            //    //get roles too maybe

            //    //    strScreenName = tmpScreenName.FirstOrDefault().ToString();

            //}

            u = datingService.GetProfileByUsername(username);


            if(u == null)
                return null;


            AnewLuvMembershipUser usr = null;

            string providername = this.Name;
            //usr.= strScreenName;
            // usr.Name = userName;
            // usr.IsSubscriber  = username;
         usr = new AnewLuvMembershipUser("AnewLuvFTSEntities",
                                       username,
                                       providerUserKey,
                                      u.ProfileID,
                                      u.SecurityAnswer,                                    
                                      "",
                                      true,
                                      false,
                                      u.CreationDate,
                                      u.LoginDate,
                                      u.ModificationDate,
                                      DateTime.Now,
                                      DateTime.Now,
                                      "");

            //add the profile as a test
         usr.thisprofile = u;
         datingService.Dispose();
            return usr;
        }

        public string GeneratePassword()
        {
            Random rand = new Random(); String password = "";
            List<String> data = new List<String>();
            for (int i = 0; i < 10; i++)
            {
                if (i < 3) data.Add(UPPER[rand.Next(UPPER.Length)].ToString());
                else if (i < 6) data.Add(LOWER[rand.Next(LOWER.Length)].ToString());
                else if (i < 8) data.Add(NUMBERS[rand.Next(NUMBERS.Length)].ToString());
                else if (i < 10) data.Add(SPECIAL[rand.Next(SPECIAL.Length)].ToString());
            }
            while (data.Count > 0)
            {
                int pos = rand.Next(data.Count); password += data[pos];
                data.RemoveAt(pos);
            }

            return password;
        }

  
        public void UpdateUserCustom(string username,string ProfileID,
                string password,
                string securityQuestion,
                string securityAnswer,
                DateTime birthdate, string gender, string country, string city, string zippostalcode)
        {


          
            

            
        }

        #region "Extra Methods added to interface to clean up MVC controllers that do member stuff"

        public bool CheckForUploadedPhotoByProfileID(string profileid)
        {
            return datingService.CheckForUploadedPhotoByProfileID(profileid);
        }
        public bool CheckIfPhotoCaptionAlreadyExists(string profileid, string photocaption)
        {
            return datingService.CheckIfPhotoCaptionAlreadyExists(profileid, photocaption);
        }

        public bool CheckIfMailBoxFoldersAreCreated(string profileid)
        {
            return datingService.CheckIfMailBoxFoldersAreCreated(profileid);
        }

        public bool CheckIfEmailAlreadyExists(string email)
        {
            return datingService.CheckIfEmailAlreadyExists(email);
        }

        public bool CheckIfProfileisActivated(string profileid)
        {
            return datingService.CheckIfProfileisActivated(profileid);
        }

        public bool ActivateProfile(string profileid)
        {
            return datingService.ActivateProfile(profileid);
        }

        public bool CreateMailBoxFolders(string profileid)
        {
            return datingService.CreateMailBoxFolders(profileid);
        }

        public string GetUserNamebyProfileID(string profileid)
        {
            return datingService.GetUserNamebyProfileID(profileid);
        }

        public string GetScreenNamebyProfileID(string profileid)
        {

            return datingService.GetScreenNamebyProfileID(profileid);
        }

        public string GetScreenNamebyUserName(string username)
        {
            return datingService.GetScreenNamebyUserName(username);
        }




        #endregion

        // Other overrides not implemented
        #region "Other overrides not implemented"

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }


        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

     


       



        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredPasswordLength
        {

            get
            {
                return 6;

              //  throw new NotImplementedException();
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }


        #endregion


      
    }
}