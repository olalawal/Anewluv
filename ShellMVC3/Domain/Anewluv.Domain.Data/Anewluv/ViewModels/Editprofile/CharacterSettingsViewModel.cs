using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data.ViewModels
{
     [DataContract]
    public class CharacterSettingsViewModel
    {
        [DataMember]
        public CharacterSettingsModel charactersettings { get; set; }
        [DataMember]
        public CharacterSearchSettingsModel   charactersearchsettings { get; set; }

        //public List<lu_diet> dietlist = new List<lu_diet>();
        //public List<lu_humor> humorlist = new List<lu_humor>();
        //public List<lu_hobby> hobbylist = new List<lu_hobby>();
        //public List<lu_drinks> drinkslist = new List<lu_drinks>();
        //public List<lu_exercise> exerciselist = new List<lu_exercise>();
        //public List<lu_smokes> smokeslist = new List<lu_smokes>();
        //public List<lu_sign> signlist = new List<lu_sign>();
        //public List<lu_politicalview> politicalviewlist = new List<lu_politicalview>();
        //public List<lu_religion> religionlist = new List<lu_religion>();
        //public List<lu_religiousattendance> religiousattendancelist = new List<lu_religiousattendance>();
    }
}
