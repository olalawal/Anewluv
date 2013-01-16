using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
      [DataContract]
    public class SearchLifeStyleSettings
    {
        //Lifestyle setting here
       [DataMember] public List<lu_educationlevel> educationlevellist  { get; set; } // = new List<string>();
       [DataMember]
       public List<lu_lookingfor> lookingforlist { get; set; } // = new List<string>();
       [DataMember]
       public List<lu_employmentstatus> employmentstatuslist { get; set; } // = new List<string>();
       [DataMember]
       public List<lu_havekids> havekidslist { get; set; } // = new List<string>();
       [DataMember]
       public List<lu_incomelevel> incomelevellist { get; set; } // = new List<string>();
       [DataMember]
       public List<lu_livingsituation> livingsituationlist { get; set; } // = new List<string>();
       [DataMember]
       public List<lu_maritalstatus> maritalstatuslist { get; set; } // = new List<string>();
       [DataMember]
       public List<lu_profession> professionlist { get; set; } // = new List<string>();
       [DataMember]
       public List<lu_wantskids> wantskidslist { get; set; } // = new List<string>();
    }
}
