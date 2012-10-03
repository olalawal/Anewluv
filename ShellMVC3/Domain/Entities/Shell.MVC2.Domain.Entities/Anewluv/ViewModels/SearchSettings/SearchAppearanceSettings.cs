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
        public List<string> ethnicitylist = new List<string>();
        public List<string> bodytypeslist = new List<string>();

        public List<string> eyecolorlist = new List<string>();
        public List<string> haircolorlist = new List<string>();
        public List<string> hotfeaturelist = new List<string>();
    }
}
