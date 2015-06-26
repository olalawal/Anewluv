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
    public enum incomelevelEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Doesn't Matter")] 
        [EnumMember]
        DoesntMatter ,
        [Description("$10,000 to $24,000")]
        [EnumMember]
        tenKtotwentyfourK,
        [Description("$25,000 to $34,000")]
        [EnumMember]
        twentyfiveKtothirtyfourK,
        [Description("$35,000 to $50,000")]
        [EnumMember]
        thirtyfiveKtofiftytK,
        [Description("$51,000 to $75,000")]
        [EnumMember]
        fiftyoneKtoseventyfiveK,
        [Description("$76,000 to $100,000")]
        [EnumMember]
        seventysixKtonehundredK,
        [Description("$101,000  to $150,000")]
        [EnumMember]
        onehundredandoneKtoonefiftyK,
        [Description("$151,000  to $499,000")]
        [EnumMember]
         onefiftyoneKtofourhundredK,
        [Description("$500,000 or More")]
        [EnumMember]
         fivehundredKorMore
    }


  
}

