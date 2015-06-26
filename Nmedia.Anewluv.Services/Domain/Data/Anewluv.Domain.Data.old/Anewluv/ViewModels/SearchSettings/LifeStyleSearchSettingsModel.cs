using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Nmedia.Infrastructure.DTOs;

namespace Anewluv.Domain.Data.ViewModels
{
      [DataContract]
    public class LifeStyleSearchSettingsModel
    {
          public LifeStyleSearchSettingsModel()
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
          public int profileid { get; set; }
        [DataMember]
        public int searchid { get; set; }
        [DataMember]
        public string searchname { get; set; }
        //Lifestyle setting here
        [DataMember]
        public List<listitem> educationlevellist { get; set; } // = new List<string>();
       [DataMember]
        public List<listitem> lookingforlist { get; set; } // = new List<string>();
       [DataMember]
       public List<listitem> employmentstatuslist { get; set; } // = new List<string>();
       [DataMember]
       public List<listitem> havekidslist { get; set; } // = new List<string>();
       [DataMember]
       public List<listitem> incomelevellist { get; set; } // = new List<string>();
       [DataMember]
       public List<listitem> livingsituationlist { get; set; } // = new List<string>();
       [DataMember]
       public List<listitem> maritalstatuslist { get; set; } // = new List<string>();
       [DataMember]
       public List<listitem> professionlist { get; set; } // = new List<string>();
       [DataMember]
       public List<listitem> wantskidslist { get; set; } // = new List<string>();
    }
}
