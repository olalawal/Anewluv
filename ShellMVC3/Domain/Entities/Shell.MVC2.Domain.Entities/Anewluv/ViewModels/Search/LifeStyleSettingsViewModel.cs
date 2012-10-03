using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    public class LifeStyleSettingsViewModel
    {
        //10-3-2012 all these are from model now
        //#region "Checkbox classes"
        //public class EducationLevelCheckBox
        //{
        //    public int EducationLevelID { get; set; }
        //    public string EducationLevelName { get; set; }
        //    public bool selected { get; set; }
        //}
        //public class EmploymentStatusCheckBox
        //{
        //    public int EmploymentStatusID { get; set; }
        //    public string EmploymentStatusName { get; set; }
        //    public bool selected { get; set; }
        //}

        //public class HaveKidsCheckBox
        //{
        //    public int HaveKidsID { get; set; }
        //    public string HaveKidsName { get; set; }
        //    public bool selected { get; set; }
        //}
        //public class IncomeLevelCheckBox
        //{
        //    public int IncomeLevelID { get; set; }
        //    public string IncomeLevelName { get; set; }
        //    public bool selected { get; set; }
        //}
        //public class LivingSituationCheckBox
        //{
        //    public int LivingSituationID { get; set; }
        //    public string LivingSituationName { get; set; }
        //    public bool selected { get; set; }
        //}
        //public class MaritalStatusCheckBox
        //{
        //    public int MaritalStatusID { get; set; }
        //    public string MaritalStatusName { get; set; }
        //    public bool selected { get; set; }
        //}
        //public class ProfessionCheckBox
        //{
        //    public int ProfessionID { get; set; }
        //    public string ProfessionName { get; set; }
        //    public bool selected { get; set; }
        //}
        //public class WantsKidsCheckBox
        //{
        //    public int WantsKidsID { get; set; }
        //    public string WantsKidsName { get; set; }
        //    public bool selected { get; set; }
        //}
        //public class LookingForCheckBox
        //{
        //    public int LookingForID { get; set; }
        //    public string LookingForName { get; set; }
        //    public bool selected { get; set; }
        //}

        //#endregion


        public List<lu_educationlevel> educationlevellist = new List<lu_educationlevel>();
        public List<lu_employmentstatus> employmentstatuslist = new List<lu_employmentstatus>();
        public List<lu_incomelevel> incomelevellist = new List<lu_incomelevel>();     
        public List<lu_lookingfor> lookingforlist = new List<lu_lookingfor>();
        public List<lu_wantskids> wantskidslist = new List<lu_wantskids>();
        public List<lu_profession> professionlist = new List<lu_profession>();
        public List<lu_maritalstatus> maritalstatuslist = new List<lu_maritalstatus>();
        public List<lu_livingsituation> livingsituationlist = new List<lu_livingsituation>();       
        public List<lu_havekids> havekidslist = new List<lu_havekids>();
       
      
    }
}
