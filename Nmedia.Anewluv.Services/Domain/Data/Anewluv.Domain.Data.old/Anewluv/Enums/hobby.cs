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
    public enum hobbyEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Health / Fitness")] 
        [EnumMember]
        HealthFitness ,
        [Description("Photography")]
        [EnumMember]
        Photography,
        [Description("Art")]
        [EnumMember]
        Art,
        [Description("Dancing")]
        [EnumMember]
        Dancing,
        [Description("Theatre")]
        [EnumMember]
        Theatre,
        [Description("Writing")]
        [EnumMember]
        Writing,   
        [Description("Dining")]
        [EnumMember]
        Dining,
        [Description("Cooking")]
        [EnumMember]
        Cooking,
        [Description("Poetry")]
        [EnumMember]
        Poetry,
        [Description("Travel")]
        [EnumMember]
        Travel,
        [Description("Watching Sports")]
        [EnumMember]
        WatchingSports,
        [Description("Family")]
        [EnumMember]
        Family,        
        [Description("Gaming")]
        [EnumMember]
        Gaming,
        [Description("Television")]
        [EnumMember]
        Television,
        [Description("Internet / Computers")]
        [EnumMember]
        InternetComputers,
        [Description("Music")]
        [EnumMember]
        Music,
        [Description("Gardening")]
        [EnumMember]
        Gardening,  
        [Description("Playing Music / Instrument")]
        [EnumMember]
        PlayingMusicInstrument,
        [Description("Outdoors")]
        [EnumMember]
        Outdoors,
        [Description("Reading")]
        [EnumMember]
        Reading,
        [Description("Crafts")]
        [EnumMember]
        Crafts,
        [Description("Volunteering")]
        [EnumMember]
        Volunteering,
        [Description("Video Gaming")]
        [EnumMember]
        VideoGaming,
        [Description("Computer Gaming")]
        [EnumMember]
        ComputerGaming,
    }


  
}

