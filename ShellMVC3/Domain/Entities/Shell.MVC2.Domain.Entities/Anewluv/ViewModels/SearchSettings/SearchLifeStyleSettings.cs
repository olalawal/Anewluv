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
        public List<string> EducationLevelList = new List<string>();
        public List<string> LookingForList = new List<string>();
        public List<string> EmploymentStatusList = new List<string>();
        public List<string> HaveKidsList = new List<string>();
        public List<string> IncomeLevelList = new List<string>();
        public List<string> LivingSituationList = new List<string>();
        public List<string> MaritalStatusList = new List<string>();
        public List<string> ProfessionList = new List<string>();
        public List<string> WantsKidsList = new List<string>();
    }
}
