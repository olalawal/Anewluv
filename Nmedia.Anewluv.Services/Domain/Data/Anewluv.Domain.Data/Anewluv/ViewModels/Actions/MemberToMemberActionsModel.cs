using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Anewluv.Domain.Data;
using Anewluv.Domain.Data.Helpers;



namespace Anewluv.Domain.Data.ViewModels
{

  [Serializable]
  [DataContract]
    public class MemberToMemberActionsModel
    {
        //actions btween members and dates       
        [DataMember]
        public DateTime? peekedataviewerdate { get; set; }
        [DataMember]
        public int? peekedataviewercount { get; set; }
        [DataMember]
        public DateTime? peekedatbyviewerdate { get; set; }  //if you have already sent a peek to this memmber
        [DataMember]
        public int? peekedatbyviewercount { get; set; }  //if you have already sent a peek to this memmber
        [DataMember]
        public DateTime? likedviewerdate { get; set; }  //if you have already sent a like to this member
        [DataMember]
        public int? likedviewercount { get; set; }  //if you have already sent a like to this member
        [DataMember]
        public DateTime? likedbyviewerdate { get; set; }  //if you have already sent a like to this member        [DataMember]
        public int? likedbyviewercount { get; set; }  //if you have already sent a like to this member
        [DataMember]
        public DateTime? blockedviewerdate { get; set; }  // if this user has blocked you
        [DataMember]
        public int? blockedviewercount { get; set; }  // if this user has blocked you
        [DataMember]
        public DateTime? blockedbyviewerdate { get; set; }  // if you have blocked this member   
        [DataMember]
        public int? blockedbyviewecount { get; set; }  // if you have blocked this member       
        [DataMember]
        public DateTime? intrestsenttoviewerDate { get; set; }  // if you have already sent an interest to this member
        [DataMember]
        public int? intrestsenttoviewercount { get; set; }  // if you have already sent an interest to this member
        [DataMember]
        public DateTime? interestsentbyviewerDate { get; set; }
        [DataMember]
        public int? interestsentbyviewercount { get; set; }  // if you have already sent an interest to this membe



    }
}
