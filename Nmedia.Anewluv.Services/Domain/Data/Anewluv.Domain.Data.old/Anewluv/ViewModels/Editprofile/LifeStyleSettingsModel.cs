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
        public LifeStyleSettingsModel()
          { 
            
            this. educationlevellist =new List<listitem>();      
            this. lookingforlist =new List<listitem>();      
           this. employmentstatuslist =new List<listitem>();      
           this. havekidslist =new List<listitem>();      
           this. incomelevellist =new List<listitem>();      
           this. livingsituationlist =new List<listitem>();      
           this. maritalstatuslist =new List<listitem>();      
           this. professionlist =new List<listitem>();      
           this. wantskidslist =new List<listitem>();
          
          }


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
       public List<listitem> maritalstatuslist { get; set; }
       [DataMember]
       public List<listitem> livingsituationlist { get; set; }
       [DataMember]
       public List<listitem> havekidslist { get; set; }
       
      
    }
}
