using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
      [DataContract]
    public class SearchCharacterSettings

    {

        //populate character settings here
        public List<string> DietList = new List<string>();
        public List<string> HumorList = new List<string>();
        public List<string> HobbyList = new List<string>();
        public List<string> DrinksList = new List<string>();
        public List<string> ExerciseList = new List<string>();
        public List<string> SmokesList = new List<string>();
        public List<string> SignList = new List<string>();
        public List<string> PoliticalViewList = new List<string>();
        public List<string> ReligionList = new List<string>();
        public List<string> ReligiousAttendanceList = new List<string>();
    }
}
