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
        Inbox=1,
        [Description("Sent")] 
        [EnumMember]
        Sent=2 ,
        [Description("Trash")]
        [EnumMember]
        Trash=3,
        [Description("Drafts")]
        [EnumMember]
        Drafts=4,
        [Description("Custom")]
        [EnumMember]
        Custom=5
    }


  
}

