﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Anewluv.Domain.Data;




namespace Anewluv.Domain.Data.ViewModels
{

  [Serializable]
  [DataContract]
    public class MemberActionsModel
    {
        //added countfeilds for times of events
        [DataMember]
        public int? whoiaminterestedintcount { get; set; }
        [DataMember]
        public int? interestcount{ get; set; }
        [DataMember]
        public int? interestnewcount { get; set; }
        [DataMember]
        public int? whoipeekedatcount { get; set; }
        [DataMember]
        public int? peekcount{ get; set; }
        [DataMember]
        public int? peeknewcount { get; set; }
        [DataMember]
        public int? whoilikecount { get; set; }
        [DataMember]
        public int? likecount{ get; set; }
        [DataMember]
        public int? likenewcount { get; set; }
        [DataMember]
        public int? blockcount{ get; set; }   
    
      //mail
        [DataMember]
        public int? mailreceivednewcount { get; set; }
        [DataMember]
        public int?  mailreceivedcount { get; set; }
        [DataMember]
        public int? mailsentcount { get; set; }
        [DataMember]
        public int? mailsentunreadcount { get; set; }   

      //to do chat ?
       



    }
}
