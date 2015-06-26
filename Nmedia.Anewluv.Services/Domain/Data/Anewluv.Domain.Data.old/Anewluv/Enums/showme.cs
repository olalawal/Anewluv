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
    public enum showmeEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Members With Photos")] 
        [EnumMember]
        MembersWithPhotos ,
        [Description("Show My Likes")]
        [EnumMember]
        Showmylikes,
        [Description("Show Members I have Viewed (Peeks)")]
        [EnumMember]
        ShowmyPeeks,
        [Description("Show My Interests")]
        [EnumMember]
        ShowMemyInterests,
        
    }


  
}

