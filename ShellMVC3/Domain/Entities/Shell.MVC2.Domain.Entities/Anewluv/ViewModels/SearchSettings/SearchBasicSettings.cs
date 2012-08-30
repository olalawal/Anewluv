using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
      [DataContract]
    public class SearchBasicSettings
    {

        //Your Search Settings Values
        //basic settings
        public int? MaxDistanceFromMe { get; set; }
        public int? SeekingAgeMin { get; set; }
        public int? SeekingAgeMax { get; set; }
        public string Country { get; set; }
        public string CityStateProvince { get; set; }
        public string PostalCode { get; set; }
        //gender is now allowing multiple selections
        public List<string> GendersList = new List<string>();

       

    }
}
