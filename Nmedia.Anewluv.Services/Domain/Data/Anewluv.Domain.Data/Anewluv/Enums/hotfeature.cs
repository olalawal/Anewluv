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
    public enum hotfeatureEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Smile")] 
        [EnumMember]
        Smile,
        [Description("Hands")]
        [EnumMember]
        Hands,
        [Description("Hair")]
        [EnumMember]
        Hair,
        [Description("Arms")]
        [EnumMember]
        Arms,
        [Description("Stomach")]
        [EnumMember]
        Stomach,
        [Description("Legs")]
        [EnumMember]
        Legs,
        [Description("Neck")]
        [EnumMember]
        Neck,
        [Description("Lips ")]
        [EnumMember]
        Lips,
        [Description("Face")]
        [EnumMember]
        Face,
        [Description("Feet")]
        [EnumMember]
        Feet,
        [Description("Chest")]
        [EnumMember]
        Chest,
        [Description("Nails")]
        [EnumMember]
        Nails,
        [Description("Full Figured")]
        [EnumMember]
        FullFigured,
        [Description("Calves")]
        [EnumMember]
        Calves,
        [Description("Booty")]
        [EnumMember]
        Booty,
        [Description("Eyes")]
        [EnumMember]
        Eyes
    }


  
}

