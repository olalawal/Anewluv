using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels

{
    public class BasicSettingsViewModel
    {

        public class ShowMeCheckBox
        {
            public int ShowMeID { get; set; }
            public string ShowMeName { get; set; }
            public bool Selected { get; set; }
        }

        public class GenderCheckBox
        {
            public int GenderID { get; set; }
            public string GenderName { get; set; }
            public bool Selected { get; set; }
        }

        public class SortByTypeCheckBox
        {
            public int SortByTypeID { get; set; }
            public string SortByTypeName { get; set; }
            public bool Selected { get; set; }
        }

        public string ProfileID { get; set; }

        public string SearchName { get; set; }

        public int SearchRank { get; set; }

        [Range(0, 5000, ErrorMessage = "DistanceFrom must be a postive number no greater than 5000")]
        public int DistanceFromMe { get; set; }

        public int LookingForAgeMax { get; set; }
        public int LookingForAgeMin { get; set; }

        //TODO remove this and populate from app fabric 
        //bound list items here
        // public List<SelectListItem> Ages { get; set; }


        // [DataType(DataType.Text)]
        // [DisplayName("Gender")]
        // public string LookingForGender { get; set; }

        public bool MyPerfectMatch { get; set; }
        public bool SystemMatch { get; set; }
        public bool SavedSearch { get; set; }
        public int CountryId { get; set; }
        public string CityStateProvince { get; set; }
        public string PostalCode { get; set; }


      

        //add the propeties for bound litsts here
        public IList<ShowMeCheckBox> ShowMeList = new List<ShowMeCheckBox>();
        public IList<SortByTypeCheckBox> SortByList = new List<SortByTypeCheckBox>();
        //gender is now allowing multiple selections

        public IList<GenderCheckBox> LookingForGendersList { get; set; }

        // public SelectList Genders { get; set; }

        //9-16-2011 need to KILL the parametered contructor for the postbacks not to be messed up
        //MVC viewstate needs a paramerless contrsctuor ,  see 

    }
}
