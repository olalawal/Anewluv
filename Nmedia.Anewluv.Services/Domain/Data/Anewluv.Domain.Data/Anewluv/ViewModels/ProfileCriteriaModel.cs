using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Anewluv.Domain.Data;


//using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;


//TO DO we should use the Editprofile model to map this I think
namespace Anewluv.Domain.Data.ViewModels
{
      [Serializable]

    //model is the actual values for a members detailts 
          [DataContract]
    public class ProfileCriteriaModel
    {

          public ProfileCriteriaModel()
          {

              this.Ethnicity = new List<string>();
                  this.Hobbies = new List<string>();
                  this.LookingFor = new List<string>();
                  this.HotFeature = new List<string>();
              this.BasicSearchSettings = new BasicSearchSettingsModel();
              this.AppearanceSearchSettings = new AppearanceSearchSettingsModel();
              this.CharacterSearchSettings = new CharacterSearchSettingsModel();
              this.LifeStyleSearchSettings = new LifeStyleSearchSettingsModel();

          
          }
        

          

        // properties        
        [DataMember]  public string HeightByCulture { get; set; }
        [DataMember]  public string BodyType { get; set; }
        [DataMember]  public string EyeColor { get; set; }   
        [DataMember]  public string HairColor { get; set; }
        [DataMember]  public string Exercise { get; set; }
        [DataMember]  public string Religion { get; set; }
        [DataMember]  public string ReligiousAttendance { get; set; }
        [DataMember]  public string Drinks { get; set; }
        [DataMember]  public string Smokes { get; set; }
        [DataMember]  public string Humor { get; set; }
              
         
        [DataMember]  public string PoliticalView { get; set; }
        [DataMember]  public string Diet { get; set; }
        [DataMember]  public string Sign { get; set; }
        [DataMember]  public string IncomeLevel { get; set; }
        [DataMember]  public string HaveKids { get; set; }
        [DataMember]  public string WantsKids { get; set; }
        [DataMember]  public string EmploymentSatus { get; set; }
        [DataMember]  public string EducationLevel { get; set; }
        [DataMember]  public string Profession { get; set; }
        [DataMember]  public string MaritalStatus { get; set; }
        [DataMember]  public string LivingSituation { get; set; }

      [DataMember]     public List<string>  Ethnicity  = new List<string>();
    
      [DataMember]     public List<string> Hobbies = new List<string>();
      
      [DataMember]     public List<string> LookingFor = new List<string>();
       [DataMember]
       public List<string> HotFeature = new List<string>();

          //1-29-2013 olawal these are for match settings 
           [DataMember]
       public BasicSearchSettingsModel  BasicSearchSettings { get; set; }
           [DataMember]
       public AppearanceSearchSettingsModel   AppearanceSearchSettings { get; set; }
           [DataMember]
       public LifeStyleSearchSettingsModel  LifeStyleSearchSettings { get; set; }
           [DataMember]
       public CharacterSearchSettingsModel  CharacterSearchSettings { get; set; }
          
       

       

     }
    
}

