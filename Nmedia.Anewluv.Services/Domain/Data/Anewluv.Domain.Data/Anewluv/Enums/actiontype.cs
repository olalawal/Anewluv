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
    public enum actiontypeEnum : int
    {
        [Description("Like")] 
        [EnumMember]
        Like ,
        [Description("Peek")]
        [EnumMember]
        Like,
        [Description("Interest")]
        [EnumMember]
        Like,
        [Description("Friend")]
        [EnumMember]
        Friend,
        [Description("Hotlist")]
        [EnumMember]
        Hotlist,
        [Description("Favorite")]
        [EnumMember]
        Favorite,
        [Description("Block")]
        [EnumMember]
        Block,
        [Description("AbuseReport")]
        [EnumMember]
        AbuseReport,
        [Description("PhotoReject")]
        [EnumMember]
        AbuseReport,
         [Description("PhotoAprove")]
        [EnumMember]
        AbuseReport,
         [Description("AccountBann")]
         [EnumMember]
         AbuseReport
      
       
       
        
    }


  
}

