using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Anewluv.Domain.Data.ViewModels;

namespace Anewluv.Domain.Data.ViewModels
{
    [DataContract]
   public  class AnewluvMessages
    {
        [DataMember]
        public List<string> messages { get; set; }
        [DataMember]
        public  List<string> errormessages { get; set; }
        //[DataMember]
        //public bool activitycreated { get; set; }
        [DataMember]
        public List<activitytypeEnum> actvitytypes { get; set; }
       // [DataMember]
        //public List<ActivityModel> activities { get; set; }
        //TO DO allow for objects to comeback

       
        public AnewluvMessages()
        {
            this.messages = new List<string>();
            this.actvitytypes = new List<activitytypeEnum>();
            this.errormessages = new List<string>();
            //this.activitycreated = false;
           // this.activities = new List<ActivityModel>();
        }

        
    }
}
