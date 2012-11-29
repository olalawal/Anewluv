using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
 

    /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into database values when the context is created
    /// </summary>
    [DataContract]
    public enum photorejectionreasonEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Pornographic material")] 
        [EnumMember]
        Pornographicmaterial,
        [Description("Duplicate Photo")]
        [EnumMember]
        DuplicatePhoto,
        [Description("No facial picture")] 
        [EnumMember]
        Nofacialpicture,
        [Description("Too many Graphics")]
        [EnumMember]
        ToomanyGraphics,   
        [Description("Copy Right Infringement")]
        [EnumMember]
        CopyRightInfringement,
        [Description("Photo shows a Minor")]
        [EnumMember]
        PhotoshowsaMinor	
        
    }


//    1	Pornographic material	Your photo could not be approved due to it being Pornographic in nature
//2	Duplicate Photo	Your photo could not be approved becase it is a duplicate
//3	No facial picture	Your photo could not be approved becase you had no facial features showing
//4	Not A real Picture of a Person	Your photo could not be approved becase it is a stock photo, and copyright infringement
//5	Too many Graphics	Your photo could not be approved becase there are too many grapical changes on it
//6	Copy Right Infringement	Your photo could not be approved becase it is from another 
//7	Photo shows a Minor	Your photo could not be approved becase it contains a minor
  
}

