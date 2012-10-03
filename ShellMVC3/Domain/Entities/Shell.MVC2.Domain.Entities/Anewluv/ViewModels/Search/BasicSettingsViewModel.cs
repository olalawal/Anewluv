using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Shell.MVC2.Domain.Entities.Anewluv;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels

{
    public class BasicSettingsViewModel
    {

        //public class ShowMeCheckBox
        //{
        //    public int ShowMeID { get; set; }
        //    public string ShowMeName { get; set; }
        //    public bool selected { get; set; }
        //}

        //public class GenderCheckBox
        //{
        //    public int GenderID { get; set; }
        //    public string GenderName { get; set; }
        //    public bool selected { get; set; }
        //}

        //public class SortByTypeCheckBox
        //{
        //    public int SortByTypeID { get; set; }
        //    public string SortByTypeName { get; set; }
        //    public bool selected { get; set; }
        //}

        public string profileid { get; set; }

        public string searchname { get; set; }

        public int searchrank { get; set; }

        [Range(0, 5000, ErrorMessage = "DistanceFrom must be a postive number no greater than 5000")]
        public int distancefromme { get; set; }

        public int agemax { get; set; }
        public int agemin { get; set; }

        //TODO remove this and populate from app fabric 
        //bound list items here
        // public List<SelectListItem> Ages { get; set; }


        // [DataType(DataType.Text)]
        // [DisplayName("Gender")]
        // public string LookingForGender { get; set; }

        public bool myperfectmatch { get; set; }
        public bool systemmatch { get; set; }
        public bool savedsearch { get; set; }
        public int countryid { get; set; }
        public string citystateprovince { get; set; }
        public string postalcode { get; set; }

        public string aboutme { get; set; }
        public string mycatchyintroline { get; set; }
      

        //add the propeties for bound litsts here
        public  IList<lu_showme> showmelist =         new List<lu_showme>();
        public  IList<lu_sortbytype> sortbytypelist = new List<lu_sortbytype>();
        //gender is now allowing multiple selections

        public IList<lu_gender> genderslist = new List<lu_gender>();

        // public SelectList Genders { get; set; }

        //9-16-2011 need to KILL the parametered contructor for the postbacks not to be messed up
        //MVC viewstate needs a paramerless contrsctuor ,  see 

    }
}
