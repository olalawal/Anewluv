using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace NotificationModel
{



    public class MessageAddressTypeLookup
    {
        //we generate this manually from enums for now
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MessageAddressTypeLookupID { get; set; }
        [DataMember]
        public string Description { get; set; }


    }
}
