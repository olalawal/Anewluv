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
       public lu_educationlevel myeducationlevel { get; set; }
       [DataMember]
       public lu_employmentstatus myemploymentstatus { get; set; }
       [DataMember]
       public lu_incomelevel myincomelevel { get; set; }
       [DataMember]
       public List<lu_lookingfor> mylookingforlist { get; set; }
       [DataMember]
       public lu_wantskids mywantskids { get; set; }
       [DataMember]
       public lu_profession myprofession { get; set; }
       [DataMember]
       public lu_maritalstatus mymaritalstatus { get; set; }
       [DataMember]
       public lu_livingsituation mylivingsituation { get; set; }
       [DataMember]
       public lu_havekids myhavekids { get; set; }
       
      
    }
}
