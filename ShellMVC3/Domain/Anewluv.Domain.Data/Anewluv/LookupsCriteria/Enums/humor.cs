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
    public enum humorEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Any")] 
        [EnumMember]
        Any ,
        [Description("Obscure / Different")]
        [EnumMember]
        ObscureDifferent,
        [Description("Sarcastic / Dry")]
        [EnumMember]
        SarcasticDry,
        [Description("Nice / Friendly")]
        [EnumMember]
        NiceFriendly,
        [Description("Quick Witted / Clever")]
        [EnumMember]
        QuickWittedClever,
        [Description("Slapstick / Boisterious")]
        [EnumMember]
        SlapstickBoisterious,
        [Description("Goofy / Silly")]
        [EnumMember]
        GoofySilly,
        [Description("Other")]
        [EnumMember]
        Other,
        [Description("Raunchy / Dirty")]
        [EnumMember]
        RaunchyDirty,
        [Description("Cheesy / Campy")]
        [EnumMember]
        CheesyCampy,
       
    }


  
}

