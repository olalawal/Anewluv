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
    public enum livingsituationEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Any")] 
        [EnumMember]
        Any ,
        [Description("Alone")]
        [EnumMember]
        Alone,
        [Description("With parents / other family members")]
        [EnumMember]
        Withparentsotherfamilymembers,
        [Description("With Roommate(s)")]
        [EnumMember]
        WithRoommates,
        [Description("With kids")]
        [EnumMember]
        Withkids,
        [Description("With Pets")]
        [EnumMember]
        WithPets
       
    }


  
}

