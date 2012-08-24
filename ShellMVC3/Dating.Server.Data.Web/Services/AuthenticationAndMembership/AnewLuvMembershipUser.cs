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

    public class AnewLuvMembershipUser : MembershipUser
    {
        AnewLuvFTSEntities context = new AnewLuvFTSEntities();
        DatingService datingService = new DatingService();

       
        
       
        DateTime _birthdate;                                 
        string _profileid;
      //  string _securityquestion;
      //  string _securityanswer;
        string _ziporpostalcode;
        string _city;
        string _country;
          double? _longitude;
          double? _lattitude;
          string _stateprovince;
        public profile _thisprofile;       
        string _screenName;
        string _gender;
        string _password;
        string _openidIdentifer;
        string _openidProvidername;
        

        public profile  thisprofile
        {
            get { return _thisprofile; }
            set { _thisprofile  = value; }
        }


        public string screenName
        {
            get { return _screenName; }
            set { _screenName = value; }
        }

        public string city
        {
            get { return _city; }
            set { _city = value; }
        }


        public string stateprovince
        {
            get { return _stateprovince; }
            set { _stateprovince = value; }
        }

        public double? lattitude
        {
            get { return _lattitude; }
            set { _lattitude = value; }
        }

        public double? longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
        
        public string country
        {
            get { return _country; }
            set { _country = value; }
        }

        public string profileid
        {
            get { return _profileid; }
            set { _profileid = value; }
        }

        public string ziporpostalcode
        {
            get { return _ziporpostalcode; }
            set { _ziporpostalcode = value; }
        }

        //public string securityquestion
        //{
        //    get { return _securityquestion; }
        //    set { _securityquestion = value; }
        //}

        //public string securityanswer
        //{
        //    get { return _securityanswer ; }
        //    set { _securityanswer  = value; }
        //}

        public DateTime  birthdate
        {
            get { return _birthdate; }
            set { _birthdate  = value; }
        }

        public string gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        public string password
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        ///  used for SSO like jainrain
        /// </summary>
        public string openidIdentifer
        {
            get { return _openidIdentifer; }
            set { _openidIdentifer = value; }
        }

        public string openidProvidername
        {
            get { return _openidProvidername; }
            set { _openidProvidername = value; }
        }


        public AnewLuvMembershipUser(string providername,
                                  string username,
                                  object providerUserKey,
                                  string email,
                                  string passwordQuestion,
                                  string comment,
                                  bool isApproved,
                                  bool isLockedOut,
                                  DateTime creationDate,
                                  DateTime lastLoginDate,
                                  DateTime lastActivityDate,
                                  DateTime lastPasswordChangedDate,
                                  DateTime lastLockedOutDate,
                                 // string [] roles,           
                                  string customerID) :
            base(providername,
                                       username,
                                       providerUserKey,
                                       email,
                                       passwordQuestion,
                                       comment,
                                       isApproved,
                                       isLockedOut,
                                       creationDate,
                                       lastLoginDate,
                                       lastActivityDate,
                                       lastPasswordChangedDate,
                                       lastLockedOutDate)
        {
            

            this.screenName = screenName;
            this.city  = city;
            this.country  = country;
           this.birthdate = birthdate ;                                 
              this.profileid = profileid ;
        // this.securityquestion  = securityquestion;
      //  this.securityanswer = securityanswer ;
        this.ziporpostalcode = ziporpostalcode;
        this.lattitude = lattitude;
        this.longitude = longitude;
        this.stateprovince = stateprovince;
        this.thisprofile = thisprofile  ;
        this.gender = gender;
        this.password = password;
        this.openidIdentifer = openidIdentifer;
        this.openidProvidername = openidProvidername;

       

        }



    }


    public class iTwitterMembershipUser : MembershipUser 
    { private string _tokenKey; private string _tokenSecret; private string _twitterUserId; private string _login; private string _email; private int _providerUserKey; public string tokenKey
    { get { return _tokenKey; } set { _tokenKey = value; } } public string tokenSecret { get { return _tokenSecret; } set { _tokenSecret = value; } } public string twitterUserId { get { return _twitterUserId; } set { _twitterUserId = value; } } 
        public string login { get { return _login; } set { _login = value; } } public string email { get { return _email; } set { _email = value; } } public int providerUserKey { get { return _providerUserKey; } set { _providerUserKey = value; } }
        public iTwitterMembershipUser(object providername, string login, int providerUserKey, string email, string tokenKey, string tokenSecret, string twitterUserId)
        { this.tokenKey = tokenKey; this.tokenSecret = tokenSecret; this.twitterUserId = twitterUserId; this.login = login; this.providerUserKey = providerUserKey; this.email = email; } }
}
