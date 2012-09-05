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
    public enum smokesEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Ask Me")] 
        [EnumMember]
        AskMe ,
        [Description("Yes, trying to quit")]
        [EnumMember]
        Never,
        [Description("Occasionally")]
        [EnumMember]
        Occasionally,
        [Description("Often")]
        [EnumMember]
        Often,
        [Description("Socially")]
        [EnumMember]
        Socially
    }


  
}

