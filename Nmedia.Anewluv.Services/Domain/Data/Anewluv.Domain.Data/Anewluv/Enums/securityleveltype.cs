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
    public enum securityleveltypeEnum : int
    {
            
        [Description("Friends")]
        [EnumMember]
        Friends=2,
        [Description("Likes")]
        [EnumMember]
        Likes=3,
        [Description("Intrests")]
        [EnumMember]
        Intrests=4,
        [Description("No One")]
        [EnumMember]
        NoOne=5,
        [Description("Peeks")]
        [EnumMember]
        Peeks = 6,  
     
     
        
    }



  
}

