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
    public enum searchsettingdetailtypeEnum : int
    {
        [Description("bodytype")]
        [EnumMember]
        bodytype,
        [Description("diet")] 
        [EnumMember]
        diet,
        [Description("drink")]
        [EnumMember]
        drink,
        [Description("educationlevel")]
        [EnumMember]
        educationlevel,
        [Description("employmentstatus")]
        [EnumMember]
        employmentstatus,
        [Description("ethnicity")]
        [EnumMember]
        ethnicity,
        [Description("excercise")]
        [EnumMember]
        excercise,
        [Description("eyecolor")]
        [EnumMember]
        eyecolor,
        [Description("gender")]
        [EnumMember]
        gender,
        [Description("haircolor")]
        [EnumMember]
        haircolor,
        [Description("havekids")]
        [EnumMember]
        havekids,
        [Description("hobby")]
        [EnumMember]
        hobby,       
        [Description("hotfeature")]
        [EnumMember]
        hotfeature,
        [Description("humor")]
        [EnumMember]
        humor,
        [Description("incomelevel")]
        [EnumMember]
        incomelevel,
        [Description("livingsituation")]
        [EnumMember]
        livingsituation,
        [Description("lookingfor")]
        [EnumMember]
        lookingfor,
       [Description("maritialstatus")]
        [EnumMember]
        maritialstatus,
        [Description("politicalview")]
        [EnumMember]
        politicalview,
        [Description("profession")]
        profession,
        [Description("religion")]
        religion,
        [Description("religiousattendance")]
        religiousattendance,          
        [Description("showme")]
        showme,
        [Description("sign")]
        sign,       
        [Description("smokes")]
        smokes,
        [Description("sortbytype")]
        sortbytype,
        [Description("wantskids")]
        wantskids
      
    }


  
}

