using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    public class LifeStyleSettingsViewModel
    {
        #region "Checkbox classes"
        public class EducationLevelCheckBox
        {
            public int EducationLevelID { get; set; }
            public string EducationLevelName { get; set; }
            public bool Selected { get; set; }
        }
        public class EmploymentStatusCheckBox
        {
            public int EmploymentStatusID { get; set; }
            public string EmploymentStatusName { get; set; }
            public bool Selected { get; set; }
        }

        public class HaveKidsCheckBox
        {
            public int HaveKidsID { get; set; }
            public string HaveKidsName { get; set; }
            public bool Selected { get; set; }
        }
        public class IncomeLevelCheckBox
        {
            public int IncomeLevelID { get; set; }
            public string IncomeLevelName { get; set; }
            public bool Selected { get; set; }
        }
        public class LivingSituationCheckBox
        {
            public int LivingSituationID { get; set; }
            public string LivingSituationName { get; set; }
            public bool Selected { get; set; }
        }
        public class MaritalStatusCheckBox
        {
            public int MaritalStatusID { get; set; }
            public string MaritalStatusName { get; set; }
            public bool Selected { get; set; }
        }
        public class ProfessionCheckBox
        {
            public int ProfessionID { get; set; }
            public string ProfessionName { get; set; }
            public bool Selected { get; set; }
        }
        public class WantsKidsCheckBox
        {
            public int WantsKidsID { get; set; }
            public string WantsKidsName { get; set; }
            public bool Selected { get; set; }
        }
        public class LookingForCheckBox
        {
            public int LookingForID { get; set; }
            public string LookingForName { get; set; }
            public bool Selected { get; set; }
        }

        #endregion


        public List<EducationLevelCheckBox> EducationLevelList = new List<EducationLevelCheckBox>();
        public List<LookingForCheckBox> LookingForList = new List<LookingForCheckBox>();
        public List<EmploymentStatusCheckBox> EmploymentStatusList = new List<EmploymentStatusCheckBox>();
        public List<HaveKidsCheckBox> HaveKidsList = new List<HaveKidsCheckBox>();
        public List<IncomeLevelCheckBox> IncomeLevelList = new List<IncomeLevelCheckBox>();
        public List<LivingSituationCheckBox> LivingSituationList = new List<LivingSituationCheckBox>();
        public List<MaritalStatusCheckBox> MaritalStatusList = new List<MaritalStatusCheckBox>();
        public List<ProfessionCheckBox> ProfessionList = new List<ProfessionCheckBox>();
        public List<WantsKidsCheckBox> WantsKidsList = new List<WantsKidsCheckBox>();
    }
}
