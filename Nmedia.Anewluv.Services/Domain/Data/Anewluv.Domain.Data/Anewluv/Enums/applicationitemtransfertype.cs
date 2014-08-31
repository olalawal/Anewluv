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
    public enum applicationitemtransfertypeEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Member Permanent one use To other Member")] 
        [EnumMember]
        MemberToMemberOneUse ,
        [Description("Member Permanent Reusable other Member")]
        [EnumMember]
        MemberToMemberReusable,
        [Description("Member Temporary Resuable to other Member")]
        [EnumMember]
        MemperToMemberOneUserTemporary,
        [Description("Customer Service one use to other Member")]
        [EnumMember]
        CustomerServiceToMemberOneUse
        
    }


  
}

