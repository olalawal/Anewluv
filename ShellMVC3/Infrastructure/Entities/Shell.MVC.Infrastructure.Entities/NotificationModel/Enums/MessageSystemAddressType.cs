using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{


  

    /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into database values when the context is created
    /// </summary>    
    /// 
    [DataContract ]
    public enum MessageSystemAddressTypeEnum : int
    {
        [EnumMember]
        DoNotReplyAddress = 1,
        [EnumMember]
        ExternalSenderAddress = 2,
        [EnumMember]
        SupportSenderAddress = 3,

    }

   

}
