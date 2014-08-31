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
    /// this is parsed into Initial Catalog= values when the context is created
    /// </summary>
    [DataContract]
    public enum employmentstatusEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Any")] 
        [EnumMember]
        Any ,
        [Description("Work at Home")]
        [EnumMember]
        WorkatHome,
        [Description("Retired")]
        [EnumMember]
        Retired,
        [Description("Student")]
        [EnumMember]
        Student,
        [Description("Self Employed")]
        [EnumMember]
        SelfEmployed,
        [Description("Part Time")]
        [EnumMember]
        PartTime,
        [Description("Unemployed")]
        [EnumMember]
        Unemployed,
        [Description("Doesn't Matter")]
        [EnumMember]
        DoesntMatter,
        [Description("Full time")]
        [EnumMember]
        Fulltime
        
    }


  
}

