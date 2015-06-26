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
    public enum educationlevelEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Any")] 
        [EnumMember]
        Any ,
        [Description("Some College")]
        [EnumMember]
        SomeCollege,
        [Description("Ged")]
        [EnumMember]
        Ged,
        [Description("Armed Forces / Military")]
        [EnumMember]
        ArmedForcesMilitary,
        [Description("GraduateDegree")]
        [EnumMember]
        GraduateDegree,
        [Description("PostDoctoral")]
        [EnumMember]
        PostDoctoral,
        [Description("Some High School")]
        [EnumMember]
        SomeHighSchool,
        [Description("Associate Degree")]
        [EnumMember]
        AssociateDegree,
        [Description("Vocational / Trade")]
        [EnumMember]
        VocationalTrade,
        [Description("Bachelors degree")]
        [EnumMember]
        Bachelorsdegree
    }


  
}

