using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    [Serializable]
    [DataContract]
    public class QuickSearchModel
    {

        //current values selected from User Interface , we bind them to the user 
        //viewmodel for now unless we run into memory issues and effecinery, then they will expire with the view

        public int MySelectedIamGenderID { get; set; }

        public int MySelectedSeekingGenderID { get; set; }
        public int MySelectedFromAge { get; set; }
        public int MyselectedToAge { get; set; }


        public string MySelectedCountryName { get; set; }
        public int MySelectedCountryID { get; set; }
        public string MySelectedPostalCode { get; set; }
        //added 10/17/20011 so we can toggle postalcode box similar to register
        public Boolean MySelectedPostalCodeStatus { get; set; }

        public string MySelectedCity { get; set; }
        public Boolean MySelectedPhotoStatus { get; set; }
        public string MySelectedCityStateProvince { get; set; }
        public double? MySelectedMaxDistanceFromMe { get; set; }

        //gps data added 10/17/2011
        public double? MySelectedLongitude { get; set; }
        public double? MySelectedLatitude { get; set; }


        // private MembersRepository membersrepository;
        public int mySelectedPageSize { get; set; }
        public int? mySelectedCurrentPage { get; set; }
        //add flag to let us know that the data comes from geocoinding ?
        public bool geocodeddata { get; set; }

    }
}
