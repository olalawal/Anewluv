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
         public lu_diet mydiet { get; set; }
         [DataMember]
         public lu_humor myhumor { get; set; }
         [DataMember]
         public List<lu_hobby> myhobbylist { get; set; }
         [DataMember]
         public lu_drinks mydrinking { get; set; }
         [DataMember]
         public lu_exercise myexcercise { get; set; }
         [DataMember]
         public lu_smokes mysmoking { get; set; }
         [DataMember]
         public lu_sign mysign { get; set; }
         [DataMember]
         public lu_politicalview mypoliticalview { get; set; }
         [DataMember]
         public lu_religion myreligion { get; set; }
         [DataMember]
         public lu_religiousattendance myreligiousattendance { get; set; }
    }
}
