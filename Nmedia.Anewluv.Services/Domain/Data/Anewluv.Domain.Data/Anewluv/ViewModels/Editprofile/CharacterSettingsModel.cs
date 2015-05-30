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

        public CharacterSettingsModel()
          { 
           this.dietlist = new List<listitem>();         
          this.humorlist =new List<listitem>();         
          this.hobbylist =new List<listitem>();         
          this.drinkslist =new List<listitem>();         
          this.exerciselist =new List<listitem>();         
          this.smokeslist =new List<listitem>();         
          this.signlist =new List<listitem>();         
          this.politicalviewlist =new List<listitem>();         
          this.religionlist =new List<listitem>();         
          this.religiousattendancelist =new List<listitem>();
          
          }


         [DataMember ]
        public List<listitem> dietlist { get; set; }
         [DataMember]
         public List<listitem> humorlist { get; set; }                 
         [DataMember]
         public List<listitem> drinkslist { get; set; }
         [DataMember]
         public List<listitem> exerciselist { get; set; }
         [DataMember]
         public List<listitem> smokeslist { get; set; }
         [DataMember]
         public List<listitem> signlist { get; set; }
         [DataMember]
         public List<listitem> politicalviewlist { get; set; }
         [DataMember]
         public List<listitem> religionlist { get; set; }
         [DataMember]
         public List<listitem> religiousattendancelist { get; set; }
         [DataMember]
         public List<listitem> hobbylist { get; set; }
    }
}
