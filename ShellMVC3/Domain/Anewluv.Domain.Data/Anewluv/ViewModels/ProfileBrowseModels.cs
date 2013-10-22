using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Shell.MVC2.Domain.Entities.Anewluv;


using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;



using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    [Serializable]
    [DataContract]
    public class ProfileBrowseModel
    {
          

        public MemberSearchViewModel ViewerProfileDetails { get; set; }
        public MemberSearchViewModel ProfileDetails { get; set; }   
        //Moved profileData to ProfileDetails
       // public profiledata  ViewerProfileData { get; set; }
       // public profiledata  profiledata { get; set; }  
        public ProfileCriteriaModel ViewerProfileCriteria { get; set; }
        public ProfileCriteriaModel ProfileCriteria { get; set; }  
   
        public bool PeekedAtMe { get; set; }  //if you have already sent a peek to this memmber
        public bool PeekedAtThisMember { get; set; }  //if you have already sent a peek to this memmber
        public bool LikesMe { get; set; }  //if you have already sent a like to this member
        public bool LikedThisMember { get; set; }  //if you have already sent a like to this member
        public bool BlockedMe { get; set; }  // if this user has blocked you
        public bool BlockedThisMember { get; set; }  // if you have blocked this member       
        public bool IntrestSentToMe { get; set; }  // if you have already sent an interest to this member
        public bool IntrestSentToThisMember { get; set; }  // if you have already sent an interest to this membe
        //added collection of pictures
        
    }

  

}