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
    public enum signEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Any")] 
        [EnumMember]
        Any ,
        [Description("Libra")]
        [EnumMember]
        Libra,
        [Description("Cancer")]
        [EnumMember]
        Cancer,
        [Description("Capricorn")]
        [EnumMember]
        Capricorn,
        [Description("Aries")]
        [EnumMember]
        Aries,
        [Description("Leo")]
        [EnumMember]
        Leo,
        [Description("Scorpio")]
        [EnumMember]
        Scorpio,
        [Description("Taurus")]
        [EnumMember]
        Taurus,
        [Description("Aquarius")]
        [EnumMember]
        Aquarius,
        [Description("Virgo")]
        [EnumMember]
        Virgo,
        [Description("Sagittarius")]
        [EnumMember]
        Sagittarius,
        [Description("Gemini")]
        [EnumMember]
        Gemini,
        [Description("Pisces")]
        [EnumMember]
        Pisces
        
    }


  
}

