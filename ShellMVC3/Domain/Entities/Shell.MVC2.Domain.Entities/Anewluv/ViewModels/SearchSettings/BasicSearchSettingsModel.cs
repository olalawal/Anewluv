using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    [DataContract]
    public class BasicSearchSettingsModel
    {
           
       //Match settings i.e search
       [DataMember]
       [Range(0, 5000, ErrorMessage = "DistanceFrom must be a postive number no greater than 5000")]
       public int? maxdistancefromme { get; set; }
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
       //add the propeties for bound litsts here
       [DataMember]
       public IList<lu_showme> showmelist = new List<lu_showme>();
       [DataMember]
       public IList<lu_sortbytype> sortbytypelist = new List<lu_sortbytype>();
       //gender is now allowing multiple selections
       [DataMember]
       public IList<lu_gender> genderslist = new List<lu_gender>();

        // public SelectList Genders { get; set; }

    }
}
