using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Nmedia.Infrastructure.DTOs;

namespace Anewluv.Domain.Data.ViewModels
{
    [DataContract]
    public class LifeStyleSettingsModel
    {
       [DataMember]
       public  List<listitem> educationlevellist { get; set; }
       [DataMember]
       public  List<listitem> employmentstatuslist{ get; set; }
       [DataMember]
       public List<listitem> incomelevellist { get; set; }
       [DataMember]
       public List<listitem> lookingforlist { get; set; }
       [DataMember]
       public List<listitem> wantskidslist { get; set; }
       [DataMember]
       public List<listitem> professionlist { get; set; }
       [DataMember]
       public List<listitem> maritalstatus { get; set; }
       [DataMember]
       public List<listitem> livingsituationlist { get; set; }
       [DataMember]
       public List<listitem> havekidslist { get; set; }
       
      
    }
}
