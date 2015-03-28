using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Nmedia.Infrastructure.DTOs;

namespace Anewluv.Domain.Data.ViewModels
{
      [DataContract]
    public class CharacterSearchSettingsModel

    {
          [DataMember]
          public int profileid { get; set; }
        [DataMember]
        public int searchid { get; set; }
        [DataMember]
        public string searchname { get; set; }
        //populate character settings here
          [DataMember]
        public List<listitem> dietlist { get; set; } // = new List<string>();
          [DataMember]
          public List<listitem> humorlist { get; set; } // = new List<string>();
          [DataMember]
          public List<listitem> hobbylist { get; set; } // = new List<string>();
          [DataMember]
          public List<listitem> drinkslist { get; set; } // = new List<string>();
          [DataMember]
          public List<listitem> exerciselist { get; set; } // = new List<string>();
          [DataMember]
          public List<listitem> smokeslist { get; set; } // = new List<string>();
          [DataMember]
          public List<listitem> signlist { get; set; } // = new List<string>();
          [DataMember]
          public List<listitem> politicalviewlist { get; set; } // = new List<string>();
          [DataMember]
          public List<listitem> religionlist { get; set; } // = new List<string>();
          [DataMember]
          public List<listitem> religiousattendancelist { get; set; } // = new List<string>();
    }
}
