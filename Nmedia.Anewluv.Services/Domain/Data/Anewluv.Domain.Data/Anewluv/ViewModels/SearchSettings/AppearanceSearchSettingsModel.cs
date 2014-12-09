using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data.ViewModels
{
      [DataContract]
    public class AppearanceSearchSettingsModel
    {
        //appereance search settings 

        [DataMember]
        public int? heightmax { get; set; }
        [DataMember]
        public int? heightmin { get; set; }
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
