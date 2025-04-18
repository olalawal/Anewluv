﻿using System;
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
    public enum exerciseEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Any")] 
        [EnumMember]
        Any ,
        [Description("1-2 times a week")]
        [EnumMember]
        OneToTwoTimesAweek,
        [Description("3-4 times a week")]
        [EnumMember]
        ThreeToFourTimesAweek,
        [Description("Never")]
        [EnumMember]
        Never,
        [Description("5 or more times a week")]
        [EnumMember]
        FiveormoreTimesAweek
    }


  
}

