using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data.ViewModels
{
    [DataContract]
   public  class AnewluvMessages
    {
        [DataMember]
        public List<string> messages { get; set; }
        [DataMember]
       public  List<string> errormessages { get; set; }
        //TO DO allow for objects to comeback

       
        public AnewluvMessages()
        {
            this.messages = new List<string>();
            this.errormessages = new List<string>();
        }

        
    }
}
