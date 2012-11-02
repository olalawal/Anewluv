using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{


  

    /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into database values when the context is created
    /// </summary>    
    /// 
    [DataContract ]
    public enum messagesystemaddresstypeenum : int
    {
        [Description("NotSet")][EnumMember]
        DoNotReplyAddress = 1,
        [Description("NotSet")][EnumMember]
        ExternalSenderAddress = 2,
        [Description("NotSet")][EnumMember]
        SupportSenderAddress = 3,

    }

   

}
