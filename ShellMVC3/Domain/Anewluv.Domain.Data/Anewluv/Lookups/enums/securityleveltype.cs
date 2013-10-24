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
    public enum securityquestionEnum : int
    {
        [Description("Your Mothers Maiden Name")]
        [EnumMember]
        YourMothersMaidenName,
        [Description("What State Were you Born In")] 
        [EnumMember]
        WhatStateWereyouBornIn,
        [Description("Your Favorite Food")]
        [EnumMember]
        YourFavoriteFood,
        [Description("Your Favorite Movie")]
        [EnumMember]
        YourFavoriteMovie,
     
     
        
    }


  
}

