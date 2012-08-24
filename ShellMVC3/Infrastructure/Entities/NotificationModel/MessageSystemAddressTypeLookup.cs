using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace NotificationModel
{
   

  


    public class MessageSystemAddressTypeLookup
    {
        //we generate this manually from enums for now
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MessageSystemAddressTypeLookupID { get; set; }
        [DataMember]
        public string Description { get; set; }


    }
}
