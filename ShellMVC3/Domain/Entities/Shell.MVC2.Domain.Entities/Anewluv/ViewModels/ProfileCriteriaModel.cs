using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Shell.MVC2.Domain.Entities.Anewluv;


using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;



namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
      [Serializable]

    //model is the actual values for a members detailts 
          [DataContract]
    public class ProfileCriteriaModel
    {
      //
        

        //proprperies from profileDetail I think
          public string ScreenName { get; set; }
       public string AboutMe { get; set; }
      // public string MyCatchyIntroLine { get; set; }
   

        // properties        
        public string HeightByCulture { get; set; }
        public string BodyType { get; set; }
        public string EyeColor { get; set; }   
        public string HairColor { get; set; }
        public string Exercise { get; set; }
        public string Religion { get; set; }
        public string ReligiousAttendance { get; set; }
        public string Drinks { get; set; }
        public string Smokes { get; set; }
        public string Humor { get; set; }
              
         
        public string PoliticalView { get; set; }
        public string Diet { get; set; }
        public string Sign { get; set; }
        public string IncomeLevel { get; set; }
        public string HaveKids { get; set; }
        public string WantsKids { get; set; }
        public string EmploymentSatus { get; set; }
        public string EducationLevel { get; set; }
        public string Profession { get; set; }
        public string MaritalStatus { get; set; }
        public string LivingSituation { get; set; }

       public List<string>  Ethnicity  = new List<string>();
       [DataMember]
       public List<string> Hobbies = new List<string>();
       [DataMember]
       public List<string> LookingFor = new List<string>();
       [DataMember]
       public List<string> HotFeature = new List<string>();

       public SearchBasicSettings  BasicSearchSettings { get; set; }
       public SearchAppearanceSettings AppearanceSearchSettings { get; set; }
       public SearchLifeStyleSettings LifeStyleSearchSettings { get; set; }
       public SearchCharacterSettings CharacterSearchSettings { get; set; }
          
       

       

     }
    
}

