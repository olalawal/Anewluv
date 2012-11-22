using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Shell.MVC2.Infrastructure.Entities.ApiKeyModel
{
 

    /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into database values when the context is created
    /// </summary>
    [DataContract]
    public enum accesslevelsenum : int
    {
        [Description("readonlyuser")][EnumMember]
        readonlyuser = 1,
        [Description("readwriteuser")][EnumMember]
        readwriteuser = 2,
        [Description("admin")][EnumMember]
        admin = 3,
        [Description("superadmin")][EnumMember]
        superadmin = 4
    }

}

