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
    /// this is parsed into database values when the context is created
    /// </summary>
    [DataContract]
    public enum applicationpaymenttypeEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Anewluv Currency")] 
        [EnumMember]
        AnewLuvCoins ,
        [Description("Electronic phone payment")]
        [EnumMember]
        Phone,
        [Description("Electronic Card payment")]
        [EnumMember]
        CreditCard,
        [Description("Money Order")]
        [EnumMember]
        MoneyOrder,
        [Description("3rd Party")]
        [EnumMember]
        ThirdParty,
        [Description("Customer Support")]
        [EnumMember]
        CustomerSupport
        
    }


  
}

