﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Nmedia.Infrastructure.Domain.Data.Apikey
{
    /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into Initial Catalog= values when the context is created
    /// </summary>
    [DataContract]
    public enum applicationenum : int
    {
        [Description("anewluv")][EnumMember]
        anewluv = 1,
        [Description("nigeriaconnections")][EnumMember]
        nigeriaconnections = 2,
        [Description("anewluvipad")]
        [EnumMember]
        anewluvipad = 3,
        
    }

}

