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
    public enum photoImageresizerformatEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("maxwidth=94&maxheight=95&format=jpg&mode=max&carve=true;crop=auto")] 
        [EnumMember]
        GalleryCropped,
        [Description("maxwidth=400&maxheight=400&format=jpg&mode=max&carve=true")]
        [EnumMember]
        MediumUncropped,
        [Description("maxwidth=2000&maxheight=2000&format=jpg&mode=max&carve=true")]
        [EnumMember]
        LargeUnCropped,
        
    }


  
}

