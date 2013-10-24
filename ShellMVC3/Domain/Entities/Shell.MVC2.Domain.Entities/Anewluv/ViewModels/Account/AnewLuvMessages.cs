using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    [DataContract]
   public  class AnewluvMessages
    {
        //TO DO update this to allow for multiple messages as well 
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public  List<string> errormessages { get; set; }
        //TO DO allow for objects to comeback

        public AnewluvMessages()
        {
            errormessages = new List<string>();
        }
    }
}
