using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Nmedia.Infrastructure.DTOs;

namespace Anewluv.Domain.Data.ViewModels
{
    [DataContract]
    public class CharacterSettingsModel
    {
         [DataMember ]
        public List<listitem> dietlist { get; set; }
         [DataMember]
         public List<listitem> humorlist { get; set; }                 
         [DataMember]
         public List<listitem> drinkinglist { get; set; }
         [DataMember]
         public List<listitem> excerciselist { get; set; }
         [DataMember]
         public List<listitem> smokinglist { get; set; }
         [DataMember]
         public List<listitem> signlist { get; set; }
         [DataMember]
         public List<listitem> politicalviewlist { get; set; }
         [DataMember]
         public List<listitem> religionlist { get; set; }
         [DataMember]
         public List<listitem> religiousattendance { get; set; }
         [DataMember]
         public List<listitem> hobbylist { get; set; }
    }
}
