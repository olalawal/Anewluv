using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    [DataContract]
    public class LifeStyleSettingsModel
    {
       [DataMember]
       public lu_educationlevel educationlevel { get; set; }
       [DataMember]
       public lu_employmentstatus employmentstatus { get; set; }
       [DataMember]
       public lu_incomelevel incomelevel { get; set; }
       [DataMember]
       public List<lu_lookingfor> lookingforlist { get; set; }
       [DataMember]
       public lu_wantskids wantskids { get; set; }
       [DataMember]
       public lu_profession profession { get; set; }
       [DataMember]
       public lu_maritalstatus maritalstatus { get; set; }
       [DataMember]
       public lu_livingsituation livingsituation { get; set; }
       [DataMember]
       public lu_havekids havekids { get; set; }
       
      
    }
}
