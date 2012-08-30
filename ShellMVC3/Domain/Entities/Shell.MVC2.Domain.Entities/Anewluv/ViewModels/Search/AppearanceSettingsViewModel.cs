using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    public class AppearanceSettingsViewModel
    {

        #region "Checkbox classes"
        public class EthnicityCheckBox
        {
            public int EthnicityID { get; set; }
            public string EthnicityName { get; set; }
            public bool Selected { get; set; }
        }
        public class EyeColorCheckBox
        {
            public int EyeColorID { get; set; }
            public string EyeColorName { get; set; }
            public bool Selected { get; set; }
        }
        public class HairColorCheckBox
        {
            public int HairColorID { get; set; }
            public string HairColorName { get; set; }
            public bool Selected { get; set; }
        }
        public class HotFeatureCheckBox
        {
            public int HotFeatureID { get; set; }
            public string HotFeatureName { get; set; }
            public bool Selected { get; set; }
        }
        public class BodyTypesCheckBox
        {
            public int BodyTypesID { get; set; }
            public string BodyTypesName { get; set; }
            public bool Selected { get; set; }
        }
        #endregion

        public int LookingForHeightMax { get; set; }
        public int LookingForHeightMin { get; set; }


        public List<EthnicityCheckBox> EthnicityList = new List<EthnicityCheckBox>();
        public List<BodyTypesCheckBox> BodyTypesList = new List<BodyTypesCheckBox>();

        public List<EyeColorCheckBox> EyeColorList = new List<EyeColorCheckBox>();
        public List<HairColorCheckBox> HairColorList = new List<HairColorCheckBox>();
        public List<HotFeatureCheckBox> HotFeatureList = new List<HotFeatureCheckBox>();

        //9-16-2011 need to KILL the parametered contructor for the postbacks not to be messed up
        //MVC viewstate needs a paramerless contrsctuor ,  see 
    }
}
