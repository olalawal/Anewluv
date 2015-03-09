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
    public enum notetypeEnum : int
    {
        [Description("Member abuse report note")] 
        [EnumMember]
        MemberAbuseReportNote ,
        [Description("Member block note")]
        [EnumMember]
        MemberBlockNote,
        [Description("Admin abuse report review Note")]
        [EnumMember]
        AdminAbuseReportReviewNote,
        [Description("Admin block review Note")]
        [EnumMember]
        AdminBlockReviewNote
        
    }


  
}

