using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;

using Shell.MVC2.Domain.Entities.Anewluv;



//for RIA services contrib
//using RiaServicesContrib.Mvc.Services;


           
namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
  


        #region "Profile Edit Models"

        
        public class EditProfileBasicSettingsModel
        {
           //         

            //male would be default looking for ale etc
            [Required]
            [StringLength(100, ErrorMessage = "Catchy Intro must be 100 characters or less.")]
            public string MyCatchyIntroLine { get; set; }

            [Required]
            [StringLength(500, ErrorMessage = "About Me must be 500 characters or less.")]
            public string AboutMe { get; set; }                      
       
            public DateTime MyBirthDate { get; set; }
        
            string MyGender { get; set;}

            string MyPostalCode { get; set; }

      

        }

        public class EditProfileAppearanceSettingsModel
        {

            //checkbox values for many to many properties
            public class MyEthnicityCheckBox
            {
                public int? EthnicityID { get; set; }
                public string EthnicityName { get; set; }
                public bool Selected { get; set; }
            }
         

            public class MyHotFeatureCheckBox
            {
                public int? HotFeatureID { get; set; }
                public string HotFeatureName { get; set; }
                public bool Selected { get; set; }
            }
            
            public int? EyeColorID { get; set; }
            public int? HairColorID { get; set; }
            public int? BodyTypesID { get; set; }

            [Required]
            public Double Height { get; set; }
            
            //TO DO move to appfabric list ? or maybe not
            public List<string> HeightListMetric { get; set; }
            public List<string> HairColorList { get; set; }
            public  List<string> EyeColorList { get; set; }
            public List<string> BodyTypesList { get; set; }  
                      
            //lists for check boxes
            public IList<MyEthnicityCheckBox> MyEthnicityList  = new List<MyEthnicityCheckBox>();
            public IList<MyHotFeatureCheckBox> MyHotFeatureList = new List<MyHotFeatureCheckBox>();
            //TODO move the character
            //public IList<MyHobbyCheckBox> MyHobbyList = new List<MyHobbyCheckBox>();
            //TO DO move to lifestyle or something
            //public IList<MyLookingForCheckBox> MyLookingForList = new List<MyLookingForCheckBox>(); // { get; set; } 

           
         


        }

        public class EditProfileLifeStyleSettingsModel
        {

           

            public class MyLookingForCheckBox
            {
                public int? MyLookingForID { get; set; }
                public string MyLookingForName { get; set; }
                public bool Selected { get; set; }
            }

            public int? EducationLevelID { get; set; }
            public int? EmploymentStatusID { get; set; }
            public int? HaveKidsId { get; set; }
            public int? IncomeLevelID { get; set; }
            public int? LivingSituationID { get; set; }
            public int? MaritalStatusID { get; set; }
            public int? ProfessionID { get; set; }
            public int? WantsKidsID { get; set; }

            List<string> EducationLevelList { get; set; }
            List<string> HaveKidsList { get; set; }
            List<string> IncomeLevelList { get; set; }
            List<string> MaritalStatusList { get; set; }
            List<string> ProfessionList { get; set; }
            List<string> ReligionList { get; set; }
            List<string> WantsKidsList { get; set; }

            //TO DO move to lifestyle or something
            public IList<MyLookingForCheckBox> MyLookingForList = new List<MyLookingForCheckBox>(); // { get; set; } 


  


        }

        public class EditProfileCharacterSettingsModel
        {
           


            public int? DietID { get; set; }
            public int? DrinksID { get; set; }
            public int? ExerciseID { get; set; }          
            public int? SmokesID { get; set; }
            public int? HumorID { get; set; }
            public int? SignID { get; set; }
            public int? PoliticalViewID { get; set; }
            public int? ReligionID { get; set; }
            public int? ReligiousAttendanceID { get; set; }
          
       


            public class MyHobbyCheckBox
            {
                public int? MyHobbyID { get; set; }
                public string MyHobbyName { get; set; }
                public bool Selected { get; set; }
            }
          
                      

            List<string> SmokesList { get; set; }          
            List<string> DietList { get; set; }
            List<string> DrinksList { get; set; }
            List<string> HumorList { get; set; }
            List<string> ExerciseList { get; set; }
            List<string> PoliticalViewList { get; set; }
            List<string> RelisionList { get; set; }
            List<string> SignList { get; set; }
            List<string> ReligiousAttendanceList { get; set; }

            //lists for check boxes
            //public IList<MyEthnicityCheckBox> MyEthnicityList = new List<MyEthnicityCheckBox>();
            //public IList<MyHotFeatureCheckBox> MyHotFeatureList = new List<MyHotFeatureCheckBox>();
            //TODO move the character
            public IList<MyHobbyCheckBox> MyHobbyList = new List<MyHobbyCheckBox>();
         




        }

            #region " model items for the My Matches stuff, similar to how it will be with CUstom searches"


        #endregion



    //10-29-2011 moved the  edit photo stuff here but it is not edit only, probably should be in photomodel
        //[Serializable]
        //public class EditProfilePhotoModel
        //{
        //    public Guid PhotoID { get; set; }
        //    public string ScreenName { get; set; }
        //    public string ProfileID { get; set; }
        //    public string Aproved { get; set; }
        //    public string ProfileImageType { get; set; }
        //    public bool checkedPrimary { get; set; }
        //    public DateTime? PhotoDate { get; set; }
        //    public string ImageCaption { get; set; }
        //    public int PhotoStatusID { get; set; }
        //    public bool BoolImageType(string ProfileImageType)
        //    {
        //        bool check = false;

        //        if (ProfileImageType == "Gallery")//"NoStatus vs Gallery"
        //        {
        //            check = true;
        //        }

        //        return check;
        //    }

        //}  

        //[Serializable]
        //public class EditProfileViewPhotoModel
        //{
        //    public Guid PhotoID { get; set; }
        //    public string ScreenName { get; set; }
        //    public bool CheckedPhoto { get; set; }
        //    public DateTime? PhotoDate { get; set; }
        //    public string ProfileId { get; set; }
        //}

        #endregion 





    }