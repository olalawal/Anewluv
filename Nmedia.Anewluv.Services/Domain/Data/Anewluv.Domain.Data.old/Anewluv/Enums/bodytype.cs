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
    public enum bodytypeEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Any")] 
        [EnumMember]
        Any,
        [Description("Slim")]
        [EnumMember]
        Slim,
        [Description("Fit / Toned")]
        [EnumMember]
        FitToned,
        [Description("Athletic")]
        [EnumMember]
        Athletic,
        [Description("Voluptuous")]
        [EnumMember]
        Voluptuous,
        [Description("Large")]
        [EnumMember]
        Large,
        [Description("Big and Beautiful")]
        [EnumMember]
        BigandBeautiful,
        [Description("Thick")]
        [EnumMember]
        Thick,
        [Description("Curvy")]
        [EnumMember]
        Curvy,
        [Description("Slender")]
        [EnumMember]
        Slender,
        [Description("A few extra pounds")]
        [EnumMember]
        Afewextrapounds,
        [Description("Stocky")]
        [EnumMember]
        Stocky,
        [Description("Full Figured")]
        [EnumMember]
        FullFigured,
        [Description("Bodybuilder")]
        [EnumMember]
        Bodybuilder,
        [Description("Heavyset")]
        [EnumMember]
        Heavyset,
        [Description("Average")]
        [EnumMember]
        Average
    }


  
}

