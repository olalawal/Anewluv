using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
      [DataContract]
     public class SearchAppearanceSettings
    {
        //appereance search settings 
        public string HeightMax { get; set; }
        public string HeightMin { get; set; }
        public List<string> EthnicityList = new List<string>();
        public List<string> BodyTypesList = new List<string>();

        public List<string> EyeColorList = new List<string>();
        public List<string> HairColorList = new List<string>();
        public List<string> HotFeatureList = new List<string>();
    }
}
