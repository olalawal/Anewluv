using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Anewluv.Domain.Data;


using Anewluv.Domain.Data.ViewModels;



using System.Runtime.Serialization;

namespace Anewluv.Domain.Data.ViewModels
{
    [Serializable]
    [DataContract]
    public class ProfileBrowseModel
    {


        [DataMember]
        public MemberSearchViewModel ViewerProfileDetails { get; set; }
          [DataMember]
        public MemberSearchViewModel ProfileDetails { get; set; }   
        //Moved profileData to ProfileDetails
       // public profiledata  ViewerProfileData { get; set; }
       // public profiledata  profiledata { get; set; }  
        [DataMember]
        public ProfileCriteriaModel ViewerProfileCriteria { get; set; }
          [DataMember]
        public ProfileCriteriaModel ProfileCriteria { get; set; }  
   
        [DataMember] public bool PeekedAtMe { get; set; }  //if you have already sent a peek to this memmber
        [DataMember] public bool PeekedAtThisMember { get; set; }  //if you have already sent a peek to this memmber
        [DataMember] public bool LikesMe { get; set; }  //if you have already sent a like to this member
        [DataMember] public bool LikedThisMember { get; set; }  //if you have already sent a like to this member
        [DataMember] public bool BlockedMe { get; set; }  // if this user has blocked you
        [DataMember] public bool BlockedThisMember { get; set; }  // if you have blocked this member       
        [DataMember] public bool IntrestSentToMe { get; set; }  // if you have already sent an interest to this member
        [DataMember] public bool IntrestSentToThisMember { get; set; }  // if you have already sent an interest to this membe
        //added collection of pictures
        
    }

  

}