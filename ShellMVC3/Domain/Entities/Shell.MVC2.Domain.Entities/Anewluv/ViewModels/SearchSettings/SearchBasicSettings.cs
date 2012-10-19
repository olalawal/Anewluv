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
        public int? maxdistancefromme { get; set; }
        public int? seekingagemin { get; set; }
        public int? seekingagemax { get; set; }
        public string country { get; set; }
        public string citystateprovince { get; set; }
        public string postalcode { get; set; }
        //gender is now allowing multiple selections
        public List<string> genderslist = new List<string>();

       

    }
}
