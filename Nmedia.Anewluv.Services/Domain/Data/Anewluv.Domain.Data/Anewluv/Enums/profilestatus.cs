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
    public enum profilestatusEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("NotActivated")] 
        [EnumMember]
        NotActivated,
        [Description("Activated")]
        [EnumMember]
        Activated,
        [Description("Banned")]
        [EnumMember]
        Banned,
        [Description("Inactive")]
        [EnumMember]
        Inactive,
        [Description("ResetingPassword")]
        [EnumMember]
        ResetingPassword,
        
    }


//    1	NotActivated
//2	Activated
//3	Banned
//4	Inactive
//5	ResetingPassword
//NULL	NULL

  
}

