using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Anewluv.Domain.Data
{
 

    /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into database values when the context is created
    /// </summary>
    [DataContract]
    public enum haircolorEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Any")] 
        [EnumMember]
        Any ,
        [Description("Auburn")]
        [EnumMember]
        Auburn,
        [Description("Black")]
        [EnumMember]
        Black,
        [Description("Sandy Brown")]
        [EnumMember]
        SandyBrown,
        [Description("Silver / White")]
        [EnumMember]
        SilverWhite,
        [Description("Bald")]
        [EnumMember]
        Bald,
        [Description("Dark Blonde")]
        [EnumMember]
        DarkBlonde,
        [Description("Blonde")]
        [EnumMember]
        Blonde,
        [Description("Red")]
        [EnumMember]
        Red,
        [Description("Brown")]
        [EnumMember]
        Brown,
        [Description("Grey")]
        [EnumMember]
        Grey
    }


  
}

