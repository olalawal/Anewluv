using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Nmedia.Infrastructure.DTOs;

namespace Anewluv.Domain.Data.ViewModels
{
      [DataContract]
    public class AppearanceSearchSettingsModel
    {
          public AppearanceSearchSettingsModel()
          { 
          
        this.metricheightlist = new List<metricheight>();
      this.ethnicitylist  = new List<listitem>();
       this.bodytypelist  = new List<listitem>();
       this. eyecolorlist  = new List<listitem>();
    this.haircolorlist  = new List<listitem>();
    this.hotfeaturelist = new List<listitem>();
          }


          [DataMember]
          public int profileid { get; set; }
        //appereance search settings 
        [DataMember]
        public int searchid { get; set; }
        [DataMember]
        public string searchname { get; set; }
        [DataMember]
        public int? heightmax { get; set; }
        [DataMember]
        public string heightminbyculture { get; set; }
        [DataMember]
        public string heightmaxbyculture { get; set; }
        [DataMember]
        public int? heightmin { get; set; }

       [DataMember]     
        public List<metricheight> metricheightlist { get; set; } // = new List<string>();
       [DataMember]
        public List<listitem> ethnicitylist { get; set; } // = new List<string>();
       [DataMember]
       public List<listitem> bodytypelist { get; set; } // = new List<string>();
       [DataMember]
       public List<listitem> eyecolorlist { get; set; } // = new List<string>();
       [DataMember]
       public List<listitem> haircolorlist { get; set; } // = new List<string>();
       [DataMember]
       public List<listitem> hotfeaturelist { get; set; } // = new List<string>();
    }
}
