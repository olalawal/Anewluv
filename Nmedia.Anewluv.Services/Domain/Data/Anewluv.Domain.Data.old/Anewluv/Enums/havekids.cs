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
    public enum havekidsEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Doesn't Matter")] 
        [EnumMember]
        DoesntMatter ,
        [Description("No")]
        [EnumMember]
        No,
        [Description("Yes / Part time")]
        [EnumMember]
        YesParttime,
        [Description("Yes / Full time")]
        [EnumMember]
        YesFulltime,
        [Description("Yes / But not with me")]
        [EnumMember]
        YesButnotwithme,
        
       
    }


  
}

