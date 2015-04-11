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
    public enum mailfoldertypeEnum : int
    {
        [Description("Inbox")]
        [EnumMember]
        Inbox,
        [Description("Sent")] 
        [EnumMember]
        Sent ,
        [Description("Trash")]
        [EnumMember]
        Trash,
        [Description("Drafts")]
        [EnumMember]
        Drafts,
        [Description("Custom")]
        [EnumMember]
        Custom
    }


  
}

