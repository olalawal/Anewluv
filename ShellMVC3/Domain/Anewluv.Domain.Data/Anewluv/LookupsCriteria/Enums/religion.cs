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
    public enum religionEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Any")] 
        [EnumMember]
        Any ,
        [Description("BuddhistTaoist")]
        [EnumMember]
        BuddhistTaoist,
        [Description("Spiritual but not religious")]
        [EnumMember]
        Spiritualbutnotreligious,
        [Description("Hindu")]
        [EnumMember]
        Hindu,
        [Description("Mormon")]
        [EnumMember]
        Mormon,
        [Description("Catholic")]
        [EnumMember]
        Catholic,
        [Description("Agnostic")]
        [EnumMember]
        Agnostic,
        [Description("Christian")]
        [EnumMember]
        Christian,
        [Description("Not Religious")]
        [EnumMember]
        NotReligious,
        [Description("Other")]
        [EnumMember]
        Other,
        [Description("Protestant")]
        [EnumMember]
        Protestant,
        [Description("Muslim / Islam")]
        [EnumMember]
        MuslimIslam,
        [Description("Atheist")]
        [EnumMember]
        Atheist,
        [Description("Doesn't Matter")]
        [EnumMember]
        DoesntMatter,
        [Description("Scientolgist")]
        [EnumMember]
        Scientolgist,
        [Description("Jewish")]
        [EnumMember]
        Jewish,
        [Description("LDS")]
        [EnumMember]
        LDS
    }


  
}

