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
        public List<string> dietlist = new List<string>();
        public List<string> humorlist = new List<string>();
        public List<string> hobbylist = new List<string>();
        public List<string> drinkslist = new List<string>();
        public List<string> exerciselist = new List<string>();
        public List<string> smokeslist = new List<string>();
        public List<string> signlist = new List<string>();
        public List<string> politicalviewlist = new List<string>();
        public List<string> religionlist = new List<string>();
        public List<string> religiousattendancelist = new List<string>();
    }
}
