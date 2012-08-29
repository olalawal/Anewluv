using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using Dating.Server.Data;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;

//using RiaServicesContrib.Mvc;
//using RiaServicesContrib.Mvc.Services;

using System.Text;

using System.Web.Mvc;

using Common;
using Ninject.Web.Mvc;
using Ninject;


namespace Shell.MVC2.Models
{
    public class SharedRepository
    {

        //TO DO
        //Get these initalized
        private DatingService datingservicecontext;
        private PostalDataService postaldataservicecontext;
        private AnewLuvFTSEntities db;
        private PostalData2Entities postaldb;


        public SharedRepository()
       {
           IKernel kernel = new StandardKernel();
          //Get these initalized
           datingservicecontext = kernel.Get<DatingService>(); 
           postaldataservicecontext = kernel.Get<PostalDataService>();
            db =  kernel.Get<AnewLuvFTSEntities>();
            postaldb = kernel.Get<PostalData2Entities>();
        }



        #region "Load Other misc stuff such as list of pages etc"

        public List<SystemPageSetting> GetSystemPageSettingList
        {

            get
            {
                List<SystemPageSetting> temp = new List<SystemPageSetting>();

            #if DISCONECTED
            
                SystemPageSetting virtualpage = new SystemPageSetting();
                virtualpage.Titile = "MembersHome";
                virtualpage.BodyCssSyleName = "StandardWhiteBackground";

                temp.Add(virtualpage);
                          
                            return temp;

            #else  //load list from database
                temp = datingservicecontext.GetSystemPageSettings().ToList();
                            return temp;
            #endif
            }
        }

        #endregion

        #region "Shared Collections retrived here"

        //public SelectList GendersSelectList()
        //{
        //    SelectList genders = new SelectList(datingservicecontext.GetGenders().ToList(), "GenderID", "GenderName");
        //    return genders;
        //}


        public List<SelectListItem> GendersSelectList
        {
            get
            {
                List<SelectListItem> temp = new List<SelectListItem>() ;

                #if DISCONECTED
                temp.Add(new SelectListItem() { Text = "Male", Value = "1", Selected = false });
                temp.Add(new SelectListItem() { Text = "Female", Value = "2", Selected = true });
                return temp;

                #else 
                              
               

                temp =  datingservicecontext.GetGenders().OrderBy(x => x.GenderName).ToSelectList(x => x.GenderName, x => x.GenderID.ToString(),null
                                                     );


                return temp;
                #endif

            }
        }


        public List<SelectListItem> VisibilityMailSettingsList
        {
            get
            {
                List<SelectListItem> temp = new List<SelectListItem>();



                temp.Add(new SelectListItem() { Text = "Yes", Value = "1", Selected = true });
                temp.Add(new SelectListItem() { Text = "No", Value = "0", Selected = false  });
                return temp;


            }
        }

        public List<SelectListItem> VisibilityStealthSettingsList
        {
            get
            {
                List<SelectListItem> temp = new List<SelectListItem>();

                temp.Add(new SelectListItem() { Text = "Unhide", Value = "1", Selected = true });
                temp.Add(new SelectListItem() { Text = "Hide", Value = "0", Selected = false });
                return temp;


            }
        }

        public List<SelectListItem> SecurityQuestionSelectList
        {

           
            get
            {
#if DISCONECTED
               List<SelectListItem> temp = new List<SelectListItem>() ;

         
                temp.Add(new SelectListItem() { Text = "Mothers maiden name", Value = "1", Selected = false });
                temp.Add(new SelectListItem() { Text = "Favorite book", Value = "2", Selected = true });

                return temp;

        

#else
                return datingservicecontext.GetSecurityQuestions().OrderBy(x => x.SecurityQuestionText).ToSelectList(x => x.SecurityQuestionText, x => x.SecurityQuestionID.ToString(),
                                                                   "Select A Security Question");
#endif


              
            }
        }


               

        public List<SelectListItem> AgesSelectList
        {
        
            get
            {
                List<Age> tmplist = new List<Age>();
                // Loop over the int List and modify it.

                for (int i = 18; i < 100; i++)
                {

                    var CurrentAge = new Age { AgeIndex = i.ToString(), AgeValue = i.ToString() };
                    tmplist.Add(CurrentAge);
                }

                return tmplist.OrderBy(x => x.AgeValue).ToSelectList(x => x.AgeValue  , x => x.AgeIndex.ToString(),
                                                     null);
            }
        }


    



        #region "Criteria Appearance dropdowns"

        public List<SelectListItem> HeightMetricSelectList()
        {

            List<MetricHeights> temp = new List<MetricHeights>();

           
            // Loop over the int List and modify it.

            for (int i = 48; i < 89; i++)
            {
              
                var CurrentHeight = new MetricHeights { HeightIndex = i.ToString(),  HeightValue = Extensions.ToFeetInches(i) };
                temp.Add(CurrentHeight);
                              
            }

            List<SelectListItem> temp2;
            temp2 =  temp.ToSelectList(x =>  x.HeightValue.ToString() , x => x.HeightIndex,
                                                 "Any");
            temp2.Insert (0, new SelectListItem
            {
                Text = "Any",
                Value = "-1",
                Selected = true
            });

            return temp2;

        }

        public List<SelectListItem> BodyTypesSelectList
          {
              get
              {
                  List<SelectListItem> temp;

                  temp = datingservicecontext.GetCriteriaAppearance_Bodytypes().OrderBy(x => x.BodyTypeName).ToSelectList(x => x.BodyTypeName, x => x.BodyTypesID.ToString(), "Any"
                                                       );

                  return temp;


              }
        }

        public List<SelectListItem> EyeColorSelectList
        {
            get
            {
                List<SelectListItem> temp;

                temp = datingservicecontext.GetCriteriaAppearance_EyeColor().OrderBy(x => x.EyeColorName).ToSelectList(x => x.EyeColorName, x => x.EyeColorID.ToString(), "Any"
                                                     );
                return temp;
            }
        }

        public List<SelectListItem> HairColorSelectList
        {
            get
            {
                List<SelectListItem> temp;

                temp = datingservicecontext.GetCriteriaAppearance_HairColor().OrderBy(x => x.HairColorName ).ToSelectList(x => x.HairColorName, x => x.HairColorID.ToString(), "Any"
                                                     );

                return temp;


            }
        }
        #endregion


        #region "Criteria Character Dropdowns"

        public List<SelectListItem> DietSelectList
        {
            get
            {
                List<SelectListItem> temp;

                temp = datingservicecontext.GetCriteriaCharacter_Diet().OrderBy(x => x.DietID ).ToSelectList(x => x.DietName, x => x.DietID.ToString(),"Any"
                                                     );

                return temp;


            }
        }

        public List<SelectListItem> DrinksSelectList
        {
            get
            {
                List<SelectListItem> temp;

                temp = datingservicecontext.GetCriteriaCharacter_Drinks().OrderBy(x => x.DrinksID ).ToSelectList(x => x.DrinksName, x => x.DrinksID.ToString(), "Any"
                                                     );

                return temp;


            }
        }

        public List<SelectListItem> ExerciseSelectList
        {
            get
            {
                List<SelectListItem> temp;

                temp = datingservicecontext.GetCriteriaCharacter_Exercise().OrderBy(x => x.ExerciseID ).ToSelectList(x => x.ExerciseName, x => x.ExerciseID .ToString(), "Any"
                                                     );

                return temp;
            }
        }

        public List<SelectListItem> HobbySelectList
        {
            get
            {
                List<SelectListItem> temp;

                temp = datingservicecontext.GetCriteriaCharacter_Hobby().OrderBy(x => x.HobbyID ).ToSelectList(x => x.HobbyName, x => x.HobbyID .ToString(), "Any"
                                                     );

                return temp;
            }
        }

        public List<SelectListItem> HumorSelectList
        {
            get
            {
                List<SelectListItem> temp;

                temp = datingservicecontext.GetCriteriaCharacter_Humor().OrderBy(x => x.HumorID ).ToSelectList(x => x.HumorName, x => x.HumorID.ToString(), "Any"
                                                     );

                return temp;
            }
        }

        public List<SelectListItem> PoliticalViewSelectList
        {
            get
            {
                List<SelectListItem> temp;

                temp = datingservicecontext.GetCriteriaCharacter_PoliticalView().OrderBy(x => x.PoliticalViewID).ToSelectList(x => x.PoliticalViewName, x => x.PoliticalViewID.ToString(), "Any"
                                                     );

                return temp;
            }
        }

        public List<SelectListItem> ReligionSelectList
        {
            get
            {
                List<SelectListItem> temp;

                temp = datingservicecontext.GetCriteriaCharacter_Religion().OrderBy(x => x.religionID ).ToSelectList(x => x.religionName, x => x.religionID .ToString(), "Any"
                                                     );

                return temp;
            }
        }

        public List<SelectListItem> ReligiousAttendanceSelectList
        {
            get
            {
                List<SelectListItem> temp;

                temp = datingservicecontext.GetCriteriaCharacter_ReligiousAttendance().OrderBy(x => x.ReligiousAttendanceID ).ToSelectList(x => x.ReligiousAttendanceName , x => x.ReligiousAttendanceID.ToString(), "Any"
                                                     );

                return temp;
            }
        }

        public List<SelectListItem> SignSelectList
        {
            get
            {
                List<SelectListItem> temp;

                temp = datingservicecontext.GetCriteriaCharacter_Sign().OrderBy(x => x.SignName ).ToSelectList(x => x.SignName , x => x.SignID.ToString(), "Any"
                                                     );

                return temp;
            }
        }

        public List<SelectListItem> SmokesSelectList
        {
            get
            {
                List<SelectListItem> temp;

                temp = datingservicecontext.GetCriteriaCharacter_Smokes().OrderBy(x => x.SmokesID ).ToSelectList(x => x.SmokesName, x => x.SmokesID .ToString(), "Any"
                                                     );

                return temp;
            }
        }


        #endregion

        #region "Criteria Lifestyle Dropdowns"


        public List<SelectListItem> EducationLevelSelectList
        {
            get
            {
                List<SelectListItem> temp;

                temp = datingservicecontext.GetCriteriaLife_EducationLevel().OrderBy(x => x.EducationLevelID).ToSelectList(x => x.EducationLevelName, x => x.EducationLevelID.ToString(), "Any"
                                                     );

                return temp;
            }
        }

        public List<SelectListItem> EmploymentStatusSelectList
        {
            get
            {
                List<SelectListItem> temp;

                temp = datingservicecontext.GetCriteriaLife_EmploymentStatus().OrderBy(x => x.EmploymentSatusID ).ToSelectList(x => x.EmploymentStatusName, x => x.EmploymentSatusID.ToString(), "Any"
                                                     );

                return temp;


            }
        }

        public List<SelectListItem> HaveKidsSelectList
        {
            get
            {
                List<SelectListItem> temp;

                temp = datingservicecontext.GetCriteriaLife_HaveKids().OrderBy(x => x.HaveKidsName).ToSelectList(x => x.HaveKidsName, x => x.HaveKidsId.ToString(), "Any"
                                                     );

                return temp;
            }
        }

        public List<SelectListItem> IncomeLevelSelectList
        {
            
            get
            {
                List<SelectListItem> temp;

                temp = datingservicecontext.GetCriteriaLife_IncomeLevel().OrderBy(x => x.IncomeLevelID ).ToSelectList(x => x.IncomeLevelName, x => x.IncomeLevelID.ToString(), "Any"
                                                     );

                return temp;


            }
        }

        public List<SelectListItem> LivingSituationSelectList
        {
            get
            {
                List<SelectListItem> temp;

                temp = datingservicecontext.GetCriteriaLife_LivingSituation().OrderBy(x => x.LivingSituationID).ToSelectList(x => x.LivingSituationName, x => x.LivingSituationID.ToString(), "Any"
                                                     );

                return temp;


            }
        }

        public List<SelectListItem> MaritalStatusSelectList
        {
            get
            {
                List<SelectListItem> temp;

                temp = datingservicecontext.GetCriteriaLife_MaritalStatus().OrderBy(x => x.MaritalStatusID ).ToSelectList(x => x.MaritalStatusName, x => x.MaritalStatusID.ToString(), "Any"
                                                     );

                return temp;


            }
        }


        public List<SelectListItem> LookingForSelectList
        {
            get
            {
                List<SelectListItem> temp;

                temp = datingservicecontext.GetCriteriaLife_LookingFor().OrderBy(x => x.LookingForID).ToSelectList(x => x.LookingForName, x => x.LookingForID.ToString(), "Any"
                                                     );

                return temp;


            }
        }

        public List<SelectListItem> ProfessionSelectList
        {
            get
            {
                List<SelectListItem> temp;

                temp = datingservicecontext.GetCriteriaLife_Profession().OrderBy(x => x.ProfessionID ).ToSelectList(x => x.ProfiessionName, x => x.ProfessionID.ToString(), "Any"
                                                     );

                return temp;


            }
        }
       

        public List<SelectListItem> WantsKidsSelectList
        {
            get
            {
                List<SelectListItem> temp;

                temp = datingservicecontext.GetCriteriaLife_WantsKids().OrderBy(x => x.WantsKidsName).ToSelectList(x => x.WantsKidsName, x => x.WantsKidsID.ToString(), null
                                                     );

                return temp;


            }
        }
        #endregion

      
              

   
        public List<SelectListItem> CountrySelectList(string CountryName)
        {
            List<SelectListItem> temp;

            temp =  postaldataservicecontext.GetCountry_PostalCode_ListAndOrderByCountry().ToSelectList(x => x.CountryName, x => x.CountryName,
                                                      "Any");

            return temp;
           
        }
        public List<SelectListItem> CountrySelectList()
        {


            List<SelectListItem> temp = new List<SelectListItem>();
            #if DISCONECTED
                        temp.Add(new SelectListItem() { Text = "United States", Value = "44", Selected = false });
                        temp.Add(new SelectListItem() { Text = "Canada", Value = "43", Selected = true });
                        return temp;

            #else 
            
                           temp =  postaldataservicecontext.GetCountry_PostalCode_ListAndOrderByCountry().OrderBy(x => x.CountryName).ToSelectList(x => x.CountryName, x => x.CountryName,
                                                                 "Any");
                           return temp;
            #endif



            

            
        }

        

        #region GetFilteredCitiesOld
        //public SelectList GetFilteredCitiesOld(string filter, string Country, int offset)
        //{
        //    var customers = postaldataservicecontext.GetCityListDynamic(Country, filter, "");

        //    SelectList customersList = new SelectList((from s in customers.Take(50).ToList() select new { CityProvince = s.City + "," + s.State_Province }), "CityProvince", "CityProvince", "ALL");

        //    return customersList;
        //}


        public List<SelectListItem> GetFilteredCitiesOld(string filter, string Country, int offset)
        {

            var customers = postaldataservicecontext.GetCityListDynamic(Country, filter, "");                      

            return ((from s in customers.Take(50).ToList() select new {  CityProvince = s.City + "," + s.State_Province }).ToSelectList(x => x.CityProvince, x => x.CityProvince,
                                                     "ALL"));
            
        }


        #endregion

        #region GetFilteredCities
        //public SelectList GetFilteredCities(string filter, string Country, int offset)
        //{
        //    //public CityPostalCode MySelectedCityPostalCode

        //    var customers = postaldataservicecontext.GetCityListDynamic(Country, filter, "").Take(50);

        //    SelectList customersList = new SelectList((from s in customers.ToList() select new CityStateProvince { StateProvince = s.City + "," + s.State_Province, City = s.City }), "StateProvince", "StateProvince", false);

        //    return customersList;
        //}


        public List<SelectListItem> GetFilteredCities(string filter, string Country, int offset)
        {

            List<SelectListItem> temp;
            try
            {
                var customers = postaldataservicecontext.GetCityListDynamic(Country, filter, "").Take(50);

                temp = ((from s in customers.ToList() select new CityStateProvince { StateProvince = s.City + "," + s.State_Province, City = s.City }).ToSelectList(x => x.StateProvince, x => x.StateProvince, null
                                                                ));
                return temp;
            
            }
            catch (Exception ex)
            {
                // status = MembershipCreateStatus.ProviderError;
                //  newUser = null;
                //throw ex;

                return null;
            }

        }





        // ViewData["accountlist"] = 
        //new SelectList((from s in time.Anagrafica_Dipendente.ToList() select new { ID_Dipendente=s.ID_Dipendente,FullName = s.Surname + " " + s.Name}), "ID_Dipendente", "FullName", null);

        #endregion

        #region GetFilteredPostalCodes
        //public SelectList GetFilteredPostalCodes(string filter, string Country, string City, int offset)
        //{
        //    var customers = postaldataservicecontext.GetPostalCodesByCountryAndCityPrefixDynamic(Country, City, filter);

        //   // SelectList customersList = new SelectList(customers.Skip(offset).Take(25), "PostalCode", "PostalCode", false);

        //    SelectList customersList = new SelectList(customers.ToList(), "PostalCode", "PostalCode", false);


        //    return customersList;
        //}


        public List<SelectListItem> GetFilteredPostalCodes(string filter, string Country, string City, int offset)
        {

            var customers = postaldataservicecontext.GetPostalCodesByCountryAndCityPrefixDynamic(Country, City, filter);

            return (customers.Skip(offset).Take(25).ToSelectList(x => x.PostalCode, x => x.PostalCode, ""));

        }

        #endregion

        #endregion

        #region "Search Settings Collections Here"

        //public IList<ShowMe> ShowMeCheckBoxList()
        //{
        //    IList<ShowMe> test;
        //    test = datingservicecontext.GetShowMes().ToList();
        //    return test;
        //}

        //public  List<SortByType> SortByCheckBoxList()
        //{
        //    List<SortByType> test;
        //    test = datingservicecontext.GetSortByTypes().ToList();
        //    return test;
        //}



        #endregion



    }
}