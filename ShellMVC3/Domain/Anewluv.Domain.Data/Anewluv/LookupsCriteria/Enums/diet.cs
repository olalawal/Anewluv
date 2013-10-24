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
    public enum dietEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Any")] 
        [EnumMember]
        Any ,
        [Description("Healthy  Blend")]
        [EnumMember]
        HealthyBlend,
        [Description("Mostly Meat")]
        [EnumMember]
        MostlyMeat,
        [Description("Vegetarian / Vegan")]
        [EnumMember]
        VegetarianVegan,
        [Description("Fast Food")]
        [EnumMember]
        FastFood,
        [Description("Organics Whole Foods")]
        [EnumMember]
        OrganicsWholeFoods
    }


  
}

