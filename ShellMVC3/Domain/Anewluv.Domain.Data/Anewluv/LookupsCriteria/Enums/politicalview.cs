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
    public enum politicalviewEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Doesn't Matter")] 
        [EnumMember]
        DoesntMatter ,
        [Description("Liberal")]
        [EnumMember]
        Liberal,
        [Description("Conservative")]
        [EnumMember]
        Conservative,
        [Description("Independant")]
        [EnumMember]
        Independant,
        [Description("Very Liberal")]
        [EnumMember]     
        VeryLiberal,
        [Description("Ultra Conservative")]
        [EnumMember]
        UltraConservative,
        [Description("Middle of Road")]
        [EnumMember]
        MiddleofRoad,
        [Description("TreeHugger / Green Party")]
        [EnumMember]
        TreeHuggerGreenParty
    }


  
}

