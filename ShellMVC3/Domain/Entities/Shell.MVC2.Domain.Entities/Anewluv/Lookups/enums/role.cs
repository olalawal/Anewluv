using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
 

    /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into database values when the context is created
    /// </summary>
    [DataContract]
    public enum roleEnum : int
    {
        [Description("Member")] 
        [EnumMember]
        Member,
        [Description("Suscriber")]
        [EnumMember]
        Suscriber,
        [Description("Gold")]
        [EnumMember]
        Gold,
        [Description("Platinum")]
        [EnumMember]
        Platinum,
        [Description("Admin")]
        [EnumMember]
        Admin,
    }


  
}

