using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
      [DataContract]
    public class AppearanceSearchSettingsModel
    {
        //appereance search settings 
       [DataMember] 
       public string heightmax { get; set; }
       [DataMember]
       public string heightmin { get; set; }
       [DataMember]
       public List<lu_ethnicity> ethnicitylist { get; set; } // = new List<string>();
       [DataMember]
       public List<lu_bodytype> bodytypeslist { get; set; } // = new List<string>();
       [DataMember]
       public List<lu_eyecolor > eyecolorlist { get; set; } // = new List<string>();
       [DataMember]
       public List<lu_haircolor> haircolorlist { get; set; } // = new List<string>();
       [DataMember]
       public List<lu_hotfeature> hotfeaturelist { get; set; } // = new List<string>();
    }
}
