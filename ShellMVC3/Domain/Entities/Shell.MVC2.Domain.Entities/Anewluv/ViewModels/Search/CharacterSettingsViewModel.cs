using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    public class CharacterSettingsViewModel
    {


        #region "Checkbox classes"
        public class DietCheckBox
        {
            public int DietID { get; set; }
            public string DietName { get; set; }
            public bool Selected { get; set; }
        }
        public class HumorCheckBox
        {
            public int HumorID { get; set; }
            public string HumorName { get; set; }
            public bool Selected { get; set; }
        }
        public class HobbyCheckBox
        {
            public int HobbyID { get; set; }
            public string HobbyName { get; set; }
            public bool Selected { get; set; }
        }
        public class DrinksCheckBox
        {
            public int DrinksID { get; set; }
            public string DrinksName { get; set; }
            public bool Selected { get; set; }
        }
        public class ExerciseCheckBox
        {
            public int ExerciseID { get; set; }
            public string ExerciseName { get; set; }
            public bool Selected { get; set; }
        }
        public class SmokesCheckBox
        {
            public int SmokesID { get; set; }
            public string SmokesName { get; set; }
            public bool Selected { get; set; }
        }
        public class SignCheckBox
        {
            public int SignID { get; set; }
            public string SignName { get; set; }
            public bool Selected { get; set; }
        }
        public class PoliticalViewCheckBox
        {
            public int PoliticalViewID { get; set; }
            public string PoliticalViewName { get; set; }
            public bool Selected { get; set; }
        }
        public class ReligionCheckBox
        {
            public int ReligionID { get; set; }
            public string ReligionName { get; set; }
            public bool Selected { get; set; }
        }
        public class ReligiousAttendanceCheckBox
        {
            public int ReligiousAttendanceID { get; set; }
            public string ReligiousAttendanceName { get; set; }
            public bool Selected { get; set; }
        }
        #endregion

        public List<DietCheckBox> DietList = new List<DietCheckBox>();
        public List<HumorCheckBox> HumorList = new List<HumorCheckBox>();
        public List<HobbyCheckBox> HobbyList = new List<HobbyCheckBox>();
        public List<DrinksCheckBox> DrinksList = new List<DrinksCheckBox>();
        public List<ExerciseCheckBox> ExerciseList = new List<ExerciseCheckBox>();
        public List<SmokesCheckBox> SmokesList = new List<SmokesCheckBox>();
        public List<SignCheckBox> SignList = new List<SignCheckBox>();
        public List<PoliticalViewCheckBox> PoliticalViewList = new List<PoliticalViewCheckBox>();
        public List<ReligionCheckBox> ReligionList = new List<ReligionCheckBox>();
        public List<ReligiousAttendanceCheckBox> ReligiousAttendanceList = new List<ReligiousAttendanceCheckBox>();
    }
}
