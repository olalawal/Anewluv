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
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Like")] 
        [EnumMember]
        Like ,
        [Description("Peek")]
        [EnumMember]
        Peek,
        [Description("Interest")]
        [EnumMember]
        Interest,
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
        PhotoReject,
         [Description("PhotoAprove")]
        [EnumMember]
        PhotoAprove,
         [Description("AccountBann")]
         [EnumMember]
         AccountBann
      
       
       
        
    }


  
}

