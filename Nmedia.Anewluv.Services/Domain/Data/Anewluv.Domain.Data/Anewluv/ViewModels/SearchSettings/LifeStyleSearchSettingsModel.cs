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
