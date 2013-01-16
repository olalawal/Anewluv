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
       [DataMember ] public int? maxdistancefromme { get; set; }
       [DataMember]
       public int? seekingagemin { get; set; }
       [DataMember]
       public int? seekingagemax { get; set; }
       [DataMember]
       public string country { get; set; }
       [DataMember]
       public string citystateprovince { get; set; }
       [DataMember]
       public string postalcode { get; set; }
        //gender is now allowing multiple selections
       [DataMember]
       public List<lu_gender> genderslist { get; set; } // = new List<string>();

       

    }
}
