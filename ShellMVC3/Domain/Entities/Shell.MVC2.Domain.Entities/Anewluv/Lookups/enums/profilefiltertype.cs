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
    public enum profilefiltertypeEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("WhoIamInterestedin")] 
        [EnumMember]
        WhoIamInterestedin,
        [Description("WhoisInterestsedinMe")]
        [EnumMember]
        WhoisInterestsedinMe,
        [Description("WhoIPeekedAt")]
        [EnumMember]
        WhoIPeekedAt,
        [Description("WhoPeekedAtMe")]
        [EnumMember]
        WhoPeekedAtMe,
        [Description("WhoIBlocked")]
        [EnumMember]
        WhoIBlocked,
        [Description("WhoILike")]
        [EnumMember]
        WhoILike,
        [Description("WhoLIkesMe")]
        [EnumMember]
        WhoLIkesMe
     
    }


  
}

