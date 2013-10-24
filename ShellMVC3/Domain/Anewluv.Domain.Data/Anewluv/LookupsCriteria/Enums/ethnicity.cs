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
    public enum ethnicityEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Any")] 
        [EnumMember]
        Any ,
        [Description("Latino / Hispanic")]
        [EnumMember]
        LatinoHispanic,
        [Description("Asian")]
        [EnumMember]
        Asian,
        [Description("Interacial")]
        [EnumMember]
        Interacial,
        [Description("Middle Eastern")]
        [EnumMember]
        MiddleEastern,
        [Description("Other")]
        [EnumMember]
        Other,
        [Description("Black / African descent")]
        [EnumMember]
        BlackAfricandescent,
        [Description("Caucasian / European descent")]
        [EnumMember]
        CaucasianEuropeandescent,
        [Description("Native American")]
        [EnumMember]
        NativeAmerican,
        [Description("Pacific Islander")]
        [EnumMember]
        PacificIslander,      
        [Description("East Indian")]
        [EnumMember]
        EastIndian        
    }


  
}

