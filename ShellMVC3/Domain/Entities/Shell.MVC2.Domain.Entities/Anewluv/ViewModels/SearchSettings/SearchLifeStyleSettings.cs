using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
      [DataContract]
    public class SearchLifeStyleSettings
    {
        //Lifestyle setting here
        public List<string> educationlevellist = new List<string>();
        public List<string> lookingforlist = new List<string>();
        public List<string> employmentstatuslist = new List<string>();
        public List<string> havekidslist = new List<string>();
        public List<string> incomelevellist = new List<string>();
        public List<string> livingsituationlist = new List<string>();
        public List<string> maritalstatuslist = new List<string>();
        public List<string> professionlist = new List<string>();
        public List<string> wantskidslist = new List<string>();
    }
}
