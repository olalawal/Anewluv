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
    public enum systemaddresstypeenum : int
    {
        [Description("Do not reply address ")][EnumMember]
        DoNotReplyAddress = 1,
        [Description("External sender address")][EnumMember]
        ExternalSenderAddress = 2,
        [Description("Support sender address")][EnumMember]
        SupportSenderAddress = 3,

    }

   

}
