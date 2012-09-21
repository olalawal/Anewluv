using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;


using Shell.MVC2.Interfaces;
using Shell.MVC2.Infrastructure;

namespace Shell.MVC2.Data
{
   public  class SearcRepository : MemberRepositoryBase , IMemberRepository 
    {

       
       
        private  AnewluvContext db; // = new AnewluvContext();
       //private  PostalData2Entities postaldb; //= new PostalData2Entities();



        public SearcRepository(AnewluvContext datingcontext)
            : base(datingcontext)
        {
        } 
                    // constructor
       public BasicSettingsViewModel getsearchbasicsettingsviewmodel(searchsetting p)
            {

                //populate values here ok ?
                if (p != null)


                SearchName  = p.SearchName  == null ? "Unamed Search" : p.SearchName;
                DistanceFromMe = p.DistanceFromMe == null ? 500 : p.DistanceFromMe.GetValueOrDefault();
                SearchRank = p.SearchRank == null ? 0 : p.SearchRank.GetValueOrDefault();

                //populate ages select list here I guess
                //TODO get from app fabric
               // SharedRepository sharedrepository = new SharedRepository();
                //Ages = sharedrepository.AgesSelectList;

                LookingForAgeMax = p.AgeMax  == null ? 99 : p.AgeMax.GetValueOrDefault();
                LookingForAgeMin  = p.AgeMin == null ? 18 : p.AgeMin.GetValueOrDefault();

             
               
              
                MyPerfectMatch = p.MyPerfectMatch == null ? false : p.MyPerfectMatch.Value ;
                SystemMatch = p.SystemMatch == null ? false : p.SystemMatch.Value;
                SavedSearch = p.SavedSearch == null ? false : p.SavedSearch.Value;

                //pilot how to show the rest of the values 
                //sample of doing string values
                var allShowMe = db.ShowMes;
                var ShowMeValues = new HashSet<int>(p.SearchSettings_ShowMe.Select(c => c.ShowMeID.GetValueOrDefault() ));                
                //foreach (var item in p.SearchSettings_ShowMe )
                //{
                //    (item.ShowMe.ShowMeName);
                //}
                foreach (var _ShowMe in allShowMe)
                {
                   ShowMeList.Add(new ShowMeCheckBox
                    {
                        ShowMeID = _ShowMe.ShowMeID,
                        ShowMeName = _ShowMe.ShowMeName,
                        Selected = ShowMeValues.Contains(_ShowMe.ShowMeID)
                    });
                }

                var allSortByTypes = db.SortByTypes;
                var SortByTypeValues = new HashSet<int>(p.SearchSettings_SortByType.Select(c => c.SortByTypeID.GetValueOrDefault()));

                foreach (var _SortByType in allSortByTypes)
                {
                     SortByList.Add(new SortByTypeCheckBox 
                    {
                        SortByTypeName = _SortByType.SortByName,
                        SortByTypeID  =  _SortByType.SortByTypeID,
                        Selected = SortByTypeValues.Contains(_SortByType.SortByTypeID)
                    });
                }


                //populate BodyTypes uings other test value type
                //TEST how this works vs other method
                //foreach (var item in p.SearchSettings_SortByType)
                //{
                //    SortBy.Add(item.SortByType);
                //}


                //if pr profileData is Null retive the genderID 
                LookingForGendersList = new List<GenderCheckBox>();
                //= new List<GenderCheckBox>();
                var allGenders = db.genders;
                var GenderValues = new HashSet<int>(p.SearchSettings_Genders.Select(c => c.GenderID.GetValueOrDefault()));
               
                 //set default if non selected
                // logic is if its a female then show them the male checked and vice versa
                if ( GenderValues.Count == 0)
                {                
                     if (p.profiledata.GenderID  == 1)
                    {
                       GenderValues.Add(2);
                    }
                    else
                    {
                         GenderValues.Add(1);
                    }               
                }


                foreach (var _Gender in allGenders)
                {
                    LookingForGendersList.Add(new GenderCheckBox
                    {
                        GenderName = _Gender.GenderName ,
                        GenderID = _Gender.GenderID,
                        //set default if non selected
                        Selected = GenderValues.Contains(_Gender.GenderID)
                    });
                }

                 



                //for the gender only select one , the oposite of the members 
                //set defualts
                //if (p.SearchSettings_Genders  == 1)
                //{
                //    LookingForGender = "Male";
                //}
                //else
                //{
                //    LookingForGender = "Female";
                //}
          
            }
       
       //Using a contstructor populate the current values I suppose
       //The actual values will bind to viewmodel I think
       public AppearanceSettingsViewModel getappearancesettingsviewmodel(searchsetting p)
            {
                AnewLuvFTSEntities db = new AnewLuvFTSEntities();

                //hight in inches max defualt is 7"6 min default is 4"0
                LookingForHeightMax = p.HeightMax == null ? -1 : p.HeightMax.GetValueOrDefault();
                LookingForHeightMin = p.HeightMin == null ? -1 : p.HeightMin.GetValueOrDefault();

                var allBodyTypes = db.CriteriaAppearance_Bodytypes;
                var BodyTypesValues = new HashSet<int>(p.SearchSettings_BodyTypes.Select(c => c.BodyTypesID.GetValueOrDefault()));
                foreach (var _BodyTypes in allBodyTypes)
                {
                    BodyTypesList.Add(new BodyTypesCheckBox
                    {
                        BodyTypesName = _BodyTypes.BodyTypeName ,
                        BodyTypesID = _BodyTypes.BodyTypesID,
                        Selected = BodyTypesValues.Contains(_BodyTypes.BodyTypesID)
                    });
                }

                //ethnicities
                var allEthnicity = db.CriteriaAppearance_Ethnicity;
                var EthnicityValues = new HashSet<int>(p.SearchSettings_Ethnicity.Select(c => c.EthicityID .GetValueOrDefault()));

                foreach (var _Ethnicity in allEthnicity)
                {
                    EthnicityList.Add(new EthnicityCheckBox
                    {
                        EthnicityName = _Ethnicity.EthnicityName ,
                       EthnicityID  = _Ethnicity.EthnicityID,
                        Selected = EthnicityValues.Contains(_Ethnicity.EthnicityID)
                    });
                }

                //eye color 

                var allEyeColor = db.CriteriaAppearance_EyeColor;
                var EyeColorValues = new HashSet<int>(p.SearchSettings_EyeColor.Select(c => c.EyeColorID.GetValueOrDefault()));
                foreach (var _EyeColor in allEyeColor)
                {
                    EyeColorList.Add(new EyeColorCheckBox 
                    {
                        EyeColorName = _EyeColor.EyeColorName ,
                        EyeColorID = _EyeColor.EyeColorID,
                        Selected = EyeColorValues.Contains(_EyeColor.EyeColorID)
                    });
                }

                // hair color
                var allHairColor = db.CriteriaAppearance_HairColor;
                var HairColorValues = new HashSet<int>(p.SearchSettings_HairColor.Select(c => c.HairColorID.GetValueOrDefault()));
                foreach (var _HairColor in allHairColor)
                {
                    HairColorList.Add(new HairColorCheckBox
                    {
                        HairColorName = _HairColor.HairColorName ,
                        HairColorID = _HairColor.HairColorID,
                        Selected = HairColorValues.Contains(_HairColor.HairColorID)
                    });
                }

                //hot feature list
                //actually list character but appearance makes more sense to me? can always change later
                //TO DO change to character maybe
                var allHotFeature = db.CriteriaCharacter_HotFeature ;
                var HotFeatureValues = new HashSet<int>(p.SearchSettings_HotFeature.Select(c => c.HotFeatureID.GetValueOrDefault()));
                foreach (var _HotFeature in allHotFeature)
                {
                    HotFeatureList.Add(new HotFeatureCheckBox
                    {
                        HotFeatureName = _HotFeature.HotFeatureName ,
                        HotFeatureID = _HotFeature.HotFeatureID,
                        Selected = HotFeatureValues.Contains(_HotFeature.HotFeatureID)
                    });
                }

            }
        
           
       //populate the enities
       public LifeStyleSettingsViewModel getlifestylesettingsviewmodel(searchsetting p)
            {
                 AnewLuvFTSEntities db = new AnewLuvFTSEntities();

                #region "EducationLevel"
                //EducationLevel checkbox values populated here
                var allEducationLevel = db.CriteriaLife_EducationLevel ;
                var EducationLevelValues = new HashSet<int>(p.SearchSettings_EducationLevel.Select(c => c.EducationLevelID.GetValueOrDefault()));

                foreach (var _EducationLevel in allEducationLevel)
                {
                    EducationLevelList.Add(new EducationLevelCheckBox
                    {
                        EducationLevelName = _EducationLevel.EducationLevelName,
                        EducationLevelID = _EducationLevel.EducationLevelID,
                        Selected = EducationLevelValues.Contains(_EducationLevel.EducationLevelID)
                    });
                }
#endregion
                #region "EmploymentStatus"
                //EmploymentStatus checkbox values populated here
                var allEmploymentStatus = db.CriteriaLife_EmploymentStatus;
                var EmploymentStatusValues = new HashSet<int>(p.SearchSettings_EmploymentStatus.Select(c => c.EmploymentStatusID.GetValueOrDefault()));

                foreach (var _EmploymentStatus in allEmploymentStatus)
                {
                    EmploymentStatusList.Add(new EmploymentStatusCheckBox
                    {
                        EmploymentStatusName = _EmploymentStatus.EmploymentStatusName,
                        EmploymentStatusID = _EmploymentStatus.EmploymentSatusID, 
                        Selected = EmploymentStatusValues.Contains(_EmploymentStatus.EmploymentSatusID )
                    });
                }
                #endregion
                #region "IncomeLevel"
                //IncomeLevel checkbox values populated here
                var allIncomeLevel = db.CriteriaLife_IncomeLevel;
                var IncomeLevelValues = new HashSet<int>(p.SearchSettings_IncomeLevel.Select(c => c.ImcomeLevelID.GetValueOrDefault()));

                foreach (var _IncomeLevel in allIncomeLevel)
                {
                    IncomeLevelList.Add(new IncomeLevelCheckBox
                    {
                        IncomeLevelName = _IncomeLevel.IncomeLevelName,
                        IncomeLevelID = _IncomeLevel.IncomeLevelID ,
                        Selected = IncomeLevelValues.Contains(_IncomeLevel.IncomeLevelID )
                    });
                }
                #endregion
                #region "LookingFor"
                //LookingFor checkbox values populated here
                var allLookingFor = db.CriteriaLife_LookingFor;
                var LookingForValues = new HashSet<int>(p.SearchSettings_LookingFor.Select(c => c.LookingForID.GetValueOrDefault()));

                foreach (var _LookingFor in allLookingFor)
                {
                    LookingForList.Add(new LookingForCheckBox
                    {
                        LookingForName = _LookingFor.LookingForName,
                        LookingForID = _LookingFor.LookingForID,
                        Selected = LookingForValues.Contains(_LookingFor.LookingForID)
                    });
                }

                #endregion
                #region "WantsKids"

                //WantsKids checkbox values populated here
                var allWantsKids = db.CriteriaLife_WantsKids;
                var WantsKidsValues = new HashSet<int>(p.SearchSettings_WantKids.Select(c => c.WantKidsID.GetValueOrDefault()));

                foreach (var _WantsKids in allWantsKids)
                {
                    WantsKidsList.Add(new WantsKidsCheckBox
                    {
                        WantsKidsName = _WantsKids.WantsKidsName,
                        WantsKidsID = _WantsKids.WantsKidsID,
                        Selected = WantsKidsValues.Contains(_WantsKids.WantsKidsID)
                    });
                }
                #endregion
                #region "Profession"

                //Profession checkbox values populated here
                var allProfession = db.CriteriaLife_Profession;
                var ProfessionValues = new HashSet<int>(p.SearchSettings_Profession.Select(c => c.ProfessionID.GetValueOrDefault()));

                foreach (var _Profession in allProfession)
                {
                    ProfessionList.Add(new ProfessionCheckBox
                    {
                        ProfessionName = _Profession.ProfiessionName ,
                        ProfessionID = _Profession.ProfessionID,
                        Selected = ProfessionValues.Contains(_Profession.ProfessionID)
                    });
                }
#endregion
                #region "Marital STatus"

                //MaritalStatus checkbox values populated here
                var allMaritalStatus = db.CriteriaLife_MaritalStatus;
                var MaritalStatusValues = new HashSet<int>(p.SearchSettings_MaritalStatus.Select(c => c.MaritalStatusID.GetValueOrDefault()));

                foreach (var _MaritalStatus in allMaritalStatus)
                {
                    MaritalStatusList.Add(new MaritalStatusCheckBox
                    {
                        MaritalStatusName = _MaritalStatus.MaritalStatusName,
                        MaritalStatusID = _MaritalStatus.MaritalStatusID,
                        Selected = MaritalStatusValues.Contains(_MaritalStatus.MaritalStatusID)
                    });
                }
#endregion
                #region "Living Situation"

                //LivingSituation checkbox values populated here
                var allLivingSituation = db.CriteriaLife_LivingSituation;
                var LivingSituationValues = new HashSet<int>(p.SearchSettings_LivingStituation.Select(c => c.LivingStituationID.GetValueOrDefault()));

                foreach (var _LivingSituation in allLivingSituation)
                {
                    LivingSituationList.Add(new LivingSituationCheckBox
                    {
                        LivingSituationName = _LivingSituation.LivingSituationName,
                        LivingSituationID = _LivingSituation.LivingSituationID,
                        Selected = LivingSituationValues.Contains(_LivingSituation.LivingSituationID)
                    });
                }

#endregion
                #region "HaveKids"


                //HaveKids checkbox values populated here
                var allHaveKids = db.CriteriaLife_HaveKids;
                var HaveKidsValues = new HashSet<int>(p.SearchSettings_HaveKids.Select(c => c.HaveKidsID.GetValueOrDefault()));

                foreach (var _HaveKids in allHaveKids)
                {
                    HaveKidsList.Add(new HaveKidsCheckBox
                    {
                        HaveKidsName = _HaveKids.HaveKidsName,
                        HaveKidsID = _HaveKids.HaveKidsId,
                        Selected = HaveKidsValues.Contains(_HaveKids.HaveKidsId)
                    });
                }

#endregion
             }
     
         //Using a contstructor populate the current values I suppose
            //The actual values will bind to viewmodel I think
       public CharacterSettingsViewModel getcharactersettings(searchsetting p)
            {

                  AnewLuvFTSEntities db = new AnewLuvFTSEntities();

                #region "Diet"
                //Diet checkbox values populated here
                var allDiet = db.CriteriaCharacter_Diet ;
                var DietValues = new HashSet<int>(p.SearchSettings_Diet.Select(c => c.DietID.GetValueOrDefault()));

                foreach (var _Diet in allDiet)
                {
                    DietList.Add(new DietCheckBox
                    {
                        DietName = _Diet.DietName,
                        DietID = _Diet.DietID,
                        Selected = DietValues.Contains(_Diet.DietID)
                    });
                }
#endregion
                #region "Humor"
                //Humor checkbox values populated here
                var allHumor = db.CriteriaCharacter_Humor ;
                var HumorValues = new HashSet<int>(p.SearchSettings_Humor.Select(c => c.HumorID.GetValueOrDefault()));

                foreach (var _Humor in allHumor)
                {
                    HumorList.Add(new HumorCheckBox
                    {
                        HumorName = _Humor.HumorName,
                        HumorID = _Humor.HumorID,
                        Selected = HumorValues.Contains(_Humor.HumorID)
                    });
                }
#endregion
                #region "Hobby"
                //Hobby checkbox values populated here
                var allHobby = db.CriteriaCharacter_Hobby ;
                var HobbyValues = new HashSet<int>(p.SearchSettings_Hobby.Select(c => c.HobbyID.GetValueOrDefault()));

                foreach (var _Hobby in allHobby)
                {
                    HobbyList.Add(new HobbyCheckBox
                    {
                        HobbyName = _Hobby.HobbyName,
                        HobbyID = _Hobby.HobbyID,
                        Selected = HobbyValues.Contains(_Hobby.HobbyID)
                    });
                }
#endregion
                #region "Drinks"
                //Drinks checkbox values populated here
                var allDrinks = db.CriteriaCharacter_Drinks ;
                var DrinksValues = new HashSet<int>(p.SearchSettings_Drinks.Select(c => c.DrinksID.GetValueOrDefault()));

                foreach (var _Drinks in allDrinks)
                {
                    DrinksList.Add(new DrinksCheckBox
                    {
                        DrinksName = _Drinks.DrinksName,
                        DrinksID = _Drinks.DrinksID,
                        Selected = DrinksValues.Contains(_Drinks.DrinksID)
                    });
                }
#endregion
                #region "Exercise"
                //Exercise checkbox values populated here
                var allExercise = db.CriteriaCharacter_Exercise ;
                var ExerciseValues = new HashSet<int>(p.SearchSettings_Exercise.Select(c => c.ExerciseID.GetValueOrDefault()));

                foreach (var _Exercise in allExercise)
                {
                    ExerciseList.Add(new ExerciseCheckBox
                    {
                        ExerciseName = _Exercise.ExerciseName,
                        ExerciseID = _Exercise.ExerciseID,
                        Selected = ExerciseValues.Contains(_Exercise.ExerciseID)
                    });
                }
#endregion
                #region "Smokes"
                //Smokes checkbox values populated here
                var allSmokes = db.CriteriaCharacter_Smokes ;
                var SmokesValues = new HashSet<int>(p.SearchSettings_Smokes.Select(c => c.SmokesID.GetValueOrDefault()));

                foreach (var _Smokes in allSmokes)
                {
                    SmokesList.Add(new SmokesCheckBox
                    {
                        SmokesName = _Smokes.SmokesName,
                        SmokesID = _Smokes.SmokesID,
                        Selected = SmokesValues.Contains(_Smokes.SmokesID)
                    });
                }
#endregion
                #region "Sign"
                //Sign checkbox values populated here
                var allSign = db.CriteriaCharacter_Sign ;
                var SignValues = new HashSet<int>(p.SearchSettings_Sign.Select(c => c.SignID.GetValueOrDefault()));

                foreach (var _Sign in allSign)
                {
                    SignList.Add(new SignCheckBox
                    {
                        SignName = _Sign.SignName,
                        SignID = _Sign.SignID,
                        Selected = SignValues.Contains(_Sign.SignID)
                    });
                }
#endregion
                #region "PoliticalView"
                //PoliticalView checkbox values populated here
                var allPoliticalView = db.CriteriaCharacter_PoliticalView ;
                var PoliticalViewValues = new HashSet<int>(p.SearchSettings_PoliticalView.Select(c => c.PoliticalViewID.GetValueOrDefault()));

                foreach (var _PoliticalView in allPoliticalView)
                {
                    PoliticalViewList.Add(new PoliticalViewCheckBox
                    {
                        PoliticalViewName = _PoliticalView.PoliticalViewName,
                        PoliticalViewID = _PoliticalView.PoliticalViewID,
                        Selected = PoliticalViewValues.Contains(_PoliticalView.PoliticalViewID)
                    });
                }
#endregion
                #region "Religion"
                //Religion checkbox values populated here
                var allReligion = db.CriteriaCharacter_Religion ;
                var ReligionValues = new HashSet<int>(p.SearchSettings_Religion.Select(c => c.ReligionID.GetValueOrDefault()));

                foreach (var _Religion in allReligion)
                {
                    ReligionList.Add(new ReligionCheckBox
                    {
                        ReligionName = _Religion.religionName ,
                        ReligionID = _Religion.religionID ,
                        Selected = ReligionValues.Contains(_Religion.religionID )
                    });
                }
#endregion
                #region "ReligiousAttendance"
                //ReligiousAttendance checkbox values populated here
                var allReligiousAttendance = db.CriteriaCharacter_ReligiousAttendance ;
                var ReligiousAttendanceValues = new HashSet<int>(p.SearchSettings_ReligiousAttendance.Select(c => c.ReligiousAttendanceID.GetValueOrDefault()));

                foreach (var _ReligiousAttendance in allReligiousAttendance)
                {
                    ReligiousAttendanceList.Add(new ReligiousAttendanceCheckBox
                    {
                        ReligiousAttendanceName = _ReligiousAttendance.ReligiousAttendanceName,
                        ReligiousAttendanceID = _ReligiousAttendance.ReligiousAttendanceID,
                        Selected = ReligiousAttendanceValues.Contains(_ReligiousAttendance.ReligiousAttendanceID)
                    });
                }
#endregion
                

            }

    }
}
