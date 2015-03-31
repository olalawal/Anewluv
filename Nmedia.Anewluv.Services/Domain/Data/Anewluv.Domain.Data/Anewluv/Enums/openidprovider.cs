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
    public enum openidproviderEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("FaceBook")]
        [EnumMember]
        FaceBook,
        [Description("Gmail")] 
        [EnumMember]
        Gmail,
        [Description("Twitter")]
        [EnumMember]
        Twitter,
        [Description("Yahoo")]
        [EnumMember]
        Yahoo
     
        
        

        
    }


  
}

