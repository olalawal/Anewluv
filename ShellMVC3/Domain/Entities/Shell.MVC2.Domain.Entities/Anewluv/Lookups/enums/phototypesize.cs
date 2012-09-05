using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
 

    /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into database values when the context is created
    /// </summary>
    [DataContract]
    public enum phototypesizeEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("94x95 thumb cropped")] 
        [EnumMember]
        ThumNailcropped,
        [Description("400x400 image not cropped")]
        [EnumMember]
        MediumUnCropped,
        [Description("1900x1900 image not cropped")]
        [EnumMember]
        LargeUnCropped,
        
    }


  
}

