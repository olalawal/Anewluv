using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace NotificationModel
{

    /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into database values when the context is created
    /// </summary>

    public enum MessageTypeEnum : int
    {
        [EnumMember]
        GlobalSystemUpdate = 1,       
        [EnumMember]
        SysAdminUpdate= 2,
        [EnumMember]
        SysAdminError = 3,
        [EnumMember]
        DeveloperUpdate = 4,
        [EnumMember]
        DeveloperError= 5,
        [EnumMember]       
        GlobalError = 6,
        [EnumMember]
        ProjectManagerUpdate= 7,
        [EnumMember]
        QAUpdate = 8,
        [EnumMember]
        UserUpdate= 9,
        [EnumMember]     
        GlobalUserUpdate = 10,

    }


    public class MessageTypeTypeLookup
    {
        //we generate this manually from enums for now
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MessageTypeLookupID { get; set; }
        [DataMember]
        public string Description { get; set; }


    }
}
