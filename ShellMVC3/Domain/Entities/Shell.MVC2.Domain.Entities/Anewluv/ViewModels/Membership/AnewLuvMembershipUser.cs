using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Security;

namespace Shell.MVC2.Domain.Entities.Anewluv
{


        [DataContract]
        public class AnewLuvMembershipUser : MembershipUser
        {
            // AnewLuvFTSEntities context = new AnewLuvFTSEntities();
            //  DatingService datingService = new DatingService();




            DateTime _birthdate;
            int _profileid;
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

            [DataMember]
            public profile thisprofile
            {
                get { return _thisprofile; }
                set { _thisprofile = value; }
            }

            [DataMember]
            public string screenName
            {
                get { return _screenName; }
                set { _screenName = value; }
            }
            [DataMember]
            public string city
            {
                get { return _city; }
                set { _city = value; }
            }

            [DataMember]
            public string stateprovince
            {
                get { return _stateprovince; }
                set { _stateprovince = value; }
            }
            [DataMember]
            public double? lattitude
            {
                get { return _lattitude; }
                set { _lattitude = value; }
            }
            [DataMember]
            public double? longitude
            {
                get { return _longitude; }
                set { _longitude = value; }
            }
            [DataMember]
            public string country
            {
                get { return _country; }
                set { _country = value; }
            }
            [DataMember]
            public int profileid
            {
                get { return _profileid; }
                set { _profileid = value; }
            }
            [DataMember]
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
            [DataMember]
            public DateTime birthdate
            {
                get { return _birthdate; }
                set { _birthdate = value; }
            }

            [DataMember]
            public string gender
            {
                get { return _gender; }
                set { _gender = value; }
            }

            [DataMember]
            public string password
            {
                get { return _password; }
                set { _password = value; }
            }

            /// <summary>
            ///  used for SSO like jainrain
            /// </summary>
            /// 
            /// 
            /// 
            [DataMember]
            public string openidIdentifer
            {
                get { return _openidIdentifer; }
                set { _openidIdentifer = value; }
            }

            [DataMember]
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
                this.city = city;
                this.country = country;
                this.birthdate = birthdate;
                this.profileid = profileid;
                // this.securityquestion  = securityquestion;
                //  this.securityanswer = securityanswer ;
                this.ziporpostalcode = ziporpostalcode;
                this.lattitude = lattitude;
                this.longitude = longitude;
                this.stateprovince = stateprovince;
                this.thisprofile = thisprofile;
                this.gender = gender;
                this.password = password;
                this.openidIdentifer = openidIdentifer;
                this.openidProvidername = openidProvidername;



            }



        }
    
}
