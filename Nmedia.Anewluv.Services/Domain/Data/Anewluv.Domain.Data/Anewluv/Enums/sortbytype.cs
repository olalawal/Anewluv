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
    public enum sortbytypeEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Closest")] 
        [EnumMember]
        Closest,
        [Description("Farthest")]
        [EnumMember]
        Farthest,
        [Description("Ethnicty")]
        [EnumMember]
        Ethnicty,
        [Description("BodyType")]
        [EnumMember]
        BodyType,
        [Description("EyeColor")]
        [EnumMember]
        EyeColor,
        [Description("HairColor")]
        [EnumMember]
        HairColor,
        [Description("Big and Beautiful")]
        [EnumMember]
        BigandBeautiful,
        [Description("Height")]
        [EnumMember]
        Height
    }


  
}

