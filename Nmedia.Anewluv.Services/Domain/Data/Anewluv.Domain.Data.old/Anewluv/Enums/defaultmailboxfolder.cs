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
    public enum defaultmailboxfoldertypeEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Inbox")] 
        [EnumMember]
        Inbox,
        [Description("Sent")]
        [EnumMember]
        Sent,
        [Description("Drafts")]
        [EnumMember]
        Drafts,
        [Description("Trash")]
        [EnumMember]
        Trash,
        [Description("Spam")]
        [EnumMember]
        Spam,
        [Description("Custom")]
        [EnumMember]
        Custom,
        
        
        
    }


  
}

