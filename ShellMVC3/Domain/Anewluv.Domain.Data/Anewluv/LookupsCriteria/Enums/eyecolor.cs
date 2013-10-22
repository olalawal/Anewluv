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
    public enum eyecolorEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Any")] 
        [EnumMember]
        Any ,
        [Description("Green")]
        [EnumMember]
        Green,
        [Description("Grey")]
        [EnumMember]
        Grey,
        [Description("Black")]
        [EnumMember]
        Black,
        [Description("Brown")]
        [EnumMember]
        Brown,
        [Description("Hazel")]
        [EnumMember]
        Hazel,
        [Description("Blue")]
        [EnumMember]
        Blue
    }


  
}

