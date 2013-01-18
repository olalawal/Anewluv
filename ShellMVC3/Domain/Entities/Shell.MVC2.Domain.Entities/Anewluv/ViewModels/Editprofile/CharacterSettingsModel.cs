using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    [DataContract]
    public class CharacterSettingsModel
    {
         [DataMember ]
         public lu_diet diet { get; set; }
         [DataMember]
         public lu_humor humor { get; set; }
         [DataMember]
         public List<profiledata_hobby> hobbylist { get; set; }
         [DataMember]
         public lu_drinks drinking { get; set; }
         [DataMember]
         public lu_exercise excercise { get; set; }
         [DataMember]
         public lu_smokes smoking { get; set; }
         [DataMember]
         public lu_sign sign { get; set; }
         [DataMember]
         public lu_politicalview politicalview { get; set; }
         [DataMember]
         public lu_religion religion { get; set; }
         [DataMember]
         public lu_religiousattendance myreligiousattendance { get; set; }
    }
}
