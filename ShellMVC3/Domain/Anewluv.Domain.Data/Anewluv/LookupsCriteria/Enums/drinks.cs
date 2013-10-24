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
    public enum drinksEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Any")] 
        [EnumMember]
        Any ,
        [Description("Regularly")]
        [EnumMember]
        Regularly,
        [Description("Never")]
        [EnumMember]
        Never,
        [Description("Socially")]
        [EnumMember]
        Socially,
        [Description("Binge")]
        [EnumMember]
        Binge
    }


  
}

