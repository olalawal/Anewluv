using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Nmedia.Infrastructure.Domain.Data.Apikey
{
 

    /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into Initial Catalog= values when the context is created
    /// </summary>
    [DataContract]
    public enum accesslevelsenum : int
    {
        [Description("user")][EnumMember]
        user = 1,
        [Description("application")][EnumMember]
        readwriteuser = 2,
        [Description("admin")][EnumMember]
        admin = 3
      
    }

}

