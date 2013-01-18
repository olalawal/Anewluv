using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
      [DataContract]
    public class CharacterSearchSettingsModel

    {

        //populate character settings here
          [DataMember]
          public List<lu_diet> dietlist  { get; set; } // = new List<string>();
          [DataMember]
          public List<lu_humor> humorlist { get; set; } // = new List<string>();
          [DataMember]
          public List<lu_hobby> hobbylist { get; set; } // = new List<string>();
          [DataMember]
          public List<lu_drinks> drinkslist { get; set; } // = new List<string>();
          [DataMember]
          public List<lu_exercise> exerciselist { get; set; } // = new List<string>();
          [DataMember]
          public List<lu_smokes> smokeslist { get; set; } // = new List<string>();
          [DataMember]
          public List<lu_sign> signlist { get; set; } // = new List<string>();
          [DataMember]
          public List<lu_politicalview> politicalviewlist { get; set; } // = new List<string>();
          [DataMember]
          public List<lu_religion> religionlist { get; set; } // = new List<string>();
          [DataMember]
          public List<lu_religiousattendance> religiousattendancelist { get; set; } // = new List<string>();
    }
}
