using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    public class AppearanceSettingsViewModel
    {
        //using from the model now
        //#region "Checkbox classes"
        //public class EthnicityCheckBox
        //{
        //    public int EthnicityID { get; set; }
        //    public string EthnicityName { get; set; }
        //    public bool selected { get; set; }
        //}
        //public class EyeColorCheckBox
        //{
        //    public int EyeColorID { get; set; }
        //    public string EyeColorName { get; set; }
        //    public bool selected { get; set; }
        //}
        //public class HairColorCheckBox
        //{
        //    public int HairColorID { get; set; }
        //    public string HairColorName { get; set; }
        //    public bool selected { get; set; }
        //}
        //public class HotFeatureCheckBox
        //{
        //    public int HotFeatureID { get; set; }
        //    public string HotFeatureName { get; set; }
        //    public bool selected { get; set; }
        //}
        //public class BodyTypesCheckBox
        //{
        //    public int BodyTypesID { get; set; }
        //    public string BodyTypesName { get; set; }
        //    public bool selected { get; set; }
        //}
        //#endregion

        public int heightmax { get; set; }
        public int heightmin { get; set; }

        public List<lu_bodytype> bodytypeslist = new List<lu_bodytype>();
        public List<lu_ethnicity> ethnicitylist = new List<lu_ethnicity>();
        
        public List<lu_eyecolor> eyecolorlist = new List<lu_eyecolor>();
        public List<lu_haircolor> haircolorlist = new List<lu_haircolor>();
        public List<lu_hotfeature> hotfeaturelist = new List<lu_hotfeature>();

        //9-16-2011 need to KILL the parametered contructor for the postbacks not to be messed up
        //MVC viewstate needs a paramerless contrsctuor ,  see 
    }
}
