using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    public class CharacterSettingsViewModel
    {
        //10-3-2012
        //these are taken from the model now
        //#region "Checkbox classes"
        //public class DietCheckBox
        //{
        //    public int DietID { get; set; }
        //    public string DietName { get; set; }
        //    public bool selected { get; set; }
        //}
        //public class HumorCheckBox
        //{
        //    public int HumorID { get; set; }
        //    public string HumorName { get; set; }
        //    public bool selected { get; set; }
        //}
        //public class HobbyCheckBox
        //{
        //    public int HobbyID { get; set; }
        //    public string HobbyName { get; set; }
        //    public bool selected { get; set; }
        //}
        //public class DrinksCheckBox
        //{
        //    public int DrinksID { get; set; }
        //    public string DrinksName { get; set; }
        //    public bool selected { get; set; }
        //}
        //public class ExerciseCheckBox
        //{
        //    public int ExerciseID { get; set; }
        //    public string ExerciseName { get; set; }
        //    public bool selected { get; set; }
        //}
        //public class SmokesCheckBox
        //{
        //    public int SmokesID { get; set; }
        //    public string SmokesName { get; set; }
        //    public bool selected { get; set; }
        //}
        //public class SignCheckBox
        //{
        //    public int SignID { get; set; }
        //    public string SignName { get; set; }
        //    public bool selected { get; set; }
        //}
        //public class PoliticalViewCheckBox
        //{
        //    public int PoliticalViewID { get; set; }
        //    public string PoliticalViewName { get; set; }
        //    public bool selected { get; set; }
        //}
        //public class ReligionCheckBox
        //{
        //    public int ReligionID { get; set; }
        //    public string ReligionName { get; set; }
        //    public bool selected { get; set; }
        //}
        //public class ReligiousAttendanceCheckBox
        //{
        //    public int ReligiousAttendanceID { get; set; }
        //    public string ReligiousAttendanceName { get; set; }
        //    public bool selected { get; set; }
        //}
        //#endregion

        public List<lu_diet> dietlist = new List<lu_diet>();
        public List<lu_humor> humorlist = new List<lu_humor>();
        public List<lu_hobby> hobbylist = new List<lu_hobby>();
        public List<lu_drinks> drinkslist = new List<lu_drinks>();
        public List<lu_exercise> exerciselist = new List<lu_exercise>();
        public List<lu_smokes> smokeslist = new List<lu_smokes>();
        public List<lu_sign> signlist = new List<lu_sign>();
        public List<lu_politicalview> politicalviewlist = new List<lu_politicalview>();
        public List<lu_religion> religionlist = new List<lu_religion>();
        public List<lu_religiousattendance> religiousattendancelist = new List<lu_religiousattendance>();
    }
}
