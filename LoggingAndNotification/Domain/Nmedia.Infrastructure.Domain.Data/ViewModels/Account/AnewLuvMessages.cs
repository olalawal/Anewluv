using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Nmedia.Infrastructure.Domain.Data.ViewModels
{
    [DataContract]
   public  class AnewluvMessages
    {
        [DataMember]
        public string message { get; set; }
        [DataMember]
       public  List<string> errormessages { get; set; }
        //TO DO allow for objects to comeback

        
    }
}
