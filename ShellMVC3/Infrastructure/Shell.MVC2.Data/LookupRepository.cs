using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;

//TO DO move these services to a new WCF service for now use it from the domain service
using Dating.Server.Data.Services;

using System.Data;
using System.Data.Entity;
using System.Web;

using Shell.MVC2.Interfaces;







namespace Shell.MVC2.Data
{
    public class LookupRepository : MemberRepositoryBase
    {
        // private AnewluvContext _db;
        //TO DO move from ria servives
        private IGeoRepository _georepository;
        private IMemberRepository _memberrepository;


        public LookupRepository(AnewluvContext datingcontext, IMemberRepository memberrepository, IGeoRepository georepository)
            : base(datingcontext)
        {
            _georepository = georepository;
            _memberrepository = memberrepository;
        }





        #region "Load Other misc stuff such as list of pages etc"

        public List<systempagesetting> GetSystemPageSettingList
        {

            get
            {
                List<systempagesetting> temp = new List<systempagesetting>();

#if DISCONECTED
            
                SystemPageSetting virtualpage = new SystemPageSetting();
                virtualpage.Titile = "MembersHome";
                virtualpage.BodyCssSyleName = "StandardWhiteBackground";

                temp.Add(virtualpage);
                          
                            return temp;

#else  //load list from database
                temp = _datingcontext.systempagesettings.ToList();
                return temp;
#endif
            }
        }

        #endregion

        #region "Shared Collections retrived here"

        //public SelectList GendersSelectList()
        //{
        //    SelectList genders = new SelectList(_datingcontext.GetGenders().ToList(), "GenderID", "GenderName");
        //    return genders;
        //}


        public List<lu_gender> GendersSelectList
        {
            get
            {
                List<string> temp = new List<string>();

#if DISCONECTED
                temp.Add(new string() { Text = "Male", Value = "1", Selected = false });
                temp.Add(new string() { Text = "Female", Value = "2", Selected = true });
                return temp;

#else



              return _datingcontext.lu_gender.OrderBy(x => x.description).ToList();
                                                     


               // return temp;
#endif

            }
        }


        public List<string> VisibilityMailSettingsList
        {
            get
            {
                List<string> temp = new List<string>();



                temp.Add(new string { Text = "Yes", Value = "1", Selected = true });
                temp.Add(new string { Text = "No", Value = "0", Selected = false });
                return temp;


            }
        }

        public List<string> VisibilityStealthSettingsList
        {
            get
            {
                List<string> temp = new List<string>();

                temp.Add(new string() { Text = "Unhide", Value = "1", Selected = true });
                temp.Add(new string() { Text = "Hide", Value = "0", Selected = false });
                return temp;


            }
        }

        public List<string> SecurityQuestionSelectList
        {


            get
            {
#if DISCONECTED
               List<string> temp = new List<string>() ;

         
                temp.Add(new string() { Text = "Mothers maiden name", Value = "1", Selected = false });
                temp.Add(new string() { Text = "Favorite book", Value = "2", Selected = true });

                return temp;

        

#else
                return _datingcontext.GetSecurityQuestions().OrderBy(x => x.SecurityQuestionText).ToSelectList(x => x.SecurityQuestionText, x => x.SecurityQuestionID.ToString(),
                                                                   "Select A Security Question");
#endif



            }
        }




        public List<string> AgesSelectList
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

                return tmplist.OrderBy(x => x.AgeValue).ToSelectList(x => x.AgeValue, x => x.AgeIndex.ToString(),
                                                     null);
            }
        }






        #region "Criteria Appearance dropdowns"

        public List<string> HeightMetricSelectList()
        {

            List<MetricHeights> temp = new List<MetricHeights>();


            // Loop over the int List and modify it.

            for (int i = 48; i < 89; i++)
            {

                var CurrentHeight = new MetricHeights { HeightIndex = i.ToString(), HeightValue = Extensions.ToFeetInches(i) };
                temp.Add(CurrentHeight);

            }

            List<string> temp2;
            temp2 = temp.ToSelectList(x => x.HeightValue.ToString(), x => x.HeightIndex,
                                                 "Any");
            temp2.Insert(0, new string
            {
                Text = "Any",
                Value = "-1",
                Selected = true
            });

            return temp2;

        }

        public List<string> BodyTypesSelectList
        {
            get
            {
                List<string> temp;

                temp = _datingcontext.GetCriteriaAppearance_Bodytypes().OrderBy(x => x.BodyTypeName).ToSelectList(x => x.BodyTypeName, x => x.BodyTypesID.ToString(), "Any"
                                                     );

                return temp;


            }
        }

        public List<string> EyeColorSelectList
        {
            get
            {
                List<string> temp;

                temp = _datingcontext.GetCriteriaAppearance_EyeColor().OrderBy(x => x.EyeColorName).ToSelectList(x => x.EyeColorName, x => x.EyeColorID.ToString(), "Any"
                                                     );
                return temp;
            }
        }

        public List<string> HairColorSelectList
        {
            get
            {
                List<string> temp;

                temp = _datingcontext.GetCriteriaAppearance_HairColor().OrderBy(x => x.HairColorName).ToSelectList(x => x.HairColorName, x => x.HairColorID.ToString(), "Any"
                                                     );

                return temp;


            }
        }
        #endregion


        #region "Criteria Character Dropdowns"

        public List<string> DietSelectList
        {
            get
            {
                List<string> temp;

                temp = _datingcontext.GetCriteriaCharacter_Diet().OrderBy(x => x.DietID).ToSelectList(x => x.DietName, x => x.DietID.ToString(), "Any"
                                                     );

                return temp;


            }
        }

        public List<string> DrinksSelectList
        {
            get
            {
                List<string> temp;

                temp = _datingcontext.GetCriteriaCharacter_Drinks().OrderBy(x => x.DrinksID).ToSelectList(x => x.DrinksName, x => x.DrinksID.ToString(), "Any"
                                                     );

                return temp;


            }
        }

        public List<string> ExerciseSelectList
        {
            get
            {
                List<string> temp;

                temp = _datingcontext.GetCriteriaCharacter_Exercise().OrderBy(x => x.ExerciseID).ToSelectList(x => x.ExerciseName, x => x.ExerciseID.ToString(), "Any"
                                                     );

                return temp;
            }
        }

        public List<string> HobbySelectList
        {
            get
            {
                List<string> temp;

                temp = _datingcontext.GetCriteriaCharacter_Hobby().OrderBy(x => x.HobbyID).ToSelectList(x => x.HobbyName, x => x.HobbyID.ToString(), "Any"
                                                     );

                return temp;
            }
        }

        public List<string> HumorSelectList
        {
            get
            {
                List<string> temp;

                temp = _datingcontext.GetCriteriaCharacter_Humor().OrderBy(x => x.HumorID).ToSelectList(x => x.HumorName, x => x.HumorID.ToString(), "Any"
                                                     );

                return temp;
            }
        }

        public List<string> PoliticalViewSelectList
        {
            get
            {
                List<string> temp;

                temp = _datingcontext.GetCriteriaCharacter_PoliticalView().OrderBy(x => x.PoliticalViewID).ToSelectList(x => x.PoliticalViewName, x => x.PoliticalViewID.ToString(), "Any"
                                                     );

                return temp;
            }
        }

        public List<string> ReligionSelectList
        {
            get
            {
                List<string> temp;

                temp = _datingcontext.GetCriteriaCharacter_Religion().OrderBy(x => x.religionID).ToSelectList(x => x.religionName, x => x.religionID.ToString(), "Any"
                                                     );

                return temp;
            }
        }

        public List<string> ReligiousAttendanceSelectList
        {
            get
            {
                List<string> temp;

                temp = _datingcontext.GetCriteriaCharacter_ReligiousAttendance().OrderBy(x => x.ReligiousAttendanceID).ToSelectList(x => x.ReligiousAttendanceName, x => x.ReligiousAttendanceID.ToString(), "Any"
                                                     );

                return temp;
            }
        }

        public List<string> SignSelectList
        {
            get
            {
                List<string> temp;

                temp = _datingcontext.GetCriteriaCharacter_Sign().OrderBy(x => x.SignName).ToSelectList(x => x.SignName, x => x.SignID.ToString(), "Any"
                                                     );

                return temp;
            }
        }

        public List<string> SmokesSelectList
        {
            get
            {
                List<string> temp;

                temp = _datingcontext.GetCriteriaCharacter_Smokes().OrderBy(x => x.SmokesID).ToSelectList(x => x.SmokesName, x => x.SmokesID.ToString(), "Any"
                                                     );

                return temp;
            }
        }


        #endregion

        #region "Criteria Lifestyle Dropdowns"


        public List<string> EducationLevelSelectList
        {
            get
            {
                List<string> temp;

                temp = _datingcontext.GetCriteriaLife_EducationLevel().OrderBy(x => x.EducationLevelID).ToSelectList(x => x.EducationLevelName, x => x.EducationLevelID.ToString(), "Any"
                                                     );

                return temp;
            }
        }

        public List<string> EmploymentStatusSelectList
        {
            get
            {
                List<string> temp;

                temp = _datingcontext.GetCriteriaLife_EmploymentStatus().OrderBy(x => x.EmploymentSatusID).ToSelectList(x => x.EmploymentStatusName, x => x.EmploymentSatusID.ToString(), "Any"
                                                     );

                return temp;


            }
        }

        public List<string> HaveKidsSelectList
        {
            get
            {
                List<string> temp;

                temp = _datingcontext.GetCriteriaLife_HaveKids().OrderBy(x => x.HaveKidsName).ToSelectList(x => x.HaveKidsName, x => x.HaveKidsId.ToString(), "Any"
                                                     );

                return temp;
            }
        }

        public List<string> IncomeLevelSelectList
        {

            get
            {
                List<string> temp;

                temp = _datingcontext.GetCriteriaLife_IncomeLevel().OrderBy(x => x.IncomeLevelID).ToSelectList(x => x.IncomeLevelName, x => x.IncomeLevelID.ToString(), "Any"
                                                     );

                return temp;


            }
        }

        public List<string> LivingSituationSelectList
        {
            get
            {
                List<string> temp;

                temp = _datingcontext.GetCriteriaLife_LivingSituation().OrderBy(x => x.LivingSituationID).ToSelectList(x => x.LivingSituationName, x => x.LivingSituationID.ToString(), "Any"
                                                     );

                return temp;


            }
        }

        public List<string> MaritalStatusSelectList
        {
            get
            {
                List<string> temp;

                temp = _datingcontext.GetCriteriaLife_MaritalStatus().OrderBy(x => x.MaritalStatusID).ToSelectList(x => x.MaritalStatusName, x => x.MaritalStatusID.ToString(), "Any"
                                                     );

                return temp;


            }
        }


        public List<string> LookingForSelectList
        {
            get
            {
                List<string> temp;

                temp = _datingcontext.GetCriteriaLife_LookingFor().OrderBy(x => x.LookingForID).ToSelectList(x => x.LookingForName, x => x.LookingForID.ToString(), "Any"
                                                     );

                return temp;


            }
        }

        public List<string> ProfessionSelectList
        {
            get
            {
                List<string> temp;

                temp = _datingcontext.GetCriteriaLife_Profession().OrderBy(x => x.ProfessionID).ToSelectList(x => x.ProfiessionName, x => x.ProfessionID.ToString(), "Any"
                                                     );

                return temp;


            }
        }


        public List<string> WantsKidsSelectList
        {
            get
            {
                List<string> temp;

                temp = _datingcontext.GetCriteriaLife_WantsKids().OrderBy(x => x.WantsKidsName).ToSelectList(x => x.WantsKidsName, x => x.WantsKidsID.ToString(), null
                                                     );

                return temp;


            }
        }
        #endregion





        public List<string> CountrySelectList(string CountryName)
        {
            List<string> temp;

            temp = _georepository.GetCountry_PostalCode_ListAndOrderByCountry().ToSelectList(x => x.CountryName, x => x.CountryName,
                                                      "Any");

            return temp;

        }
        public List<string> CountrySelectList()
        {


            List<string> temp = new List<string>();
#if DISCONECTED
                        temp.Add(new string() { Text = "United States", Value = "44", Selected = false });
                        temp.Add(new string() { Text = "Canada", Value = "43", Selected = true });
                        return temp;

#else

            temp = _georepository.GetCountry_PostalCode_ListAndOrderByCountry().OrderBy(x => x.CountryName).ToSelectList(x => x.CountryName, x => x.CountryName,
                                                  "Any");
            return temp;
#endif






        }



        #region GetFilteredCitiesOld
        //public SelectList GetFilteredCitiesOld(string filter, string Country, int offset)
        //{
        //    var customers = _georepository.GetCityListDynamic(Country, filter, "");

        //    SelectList customersList = new SelectList((from s in customers.Take(50).ToList() select new { CityProvince = s.City + "," + s.State_Province }), "CityProvince", "CityProvince", "ALL");

        //    return customersList;
        //}


        public List<string> GetFilteredCitiesOld(string filter, string Country, int offset)
        {

            var customers = _georepository.GetCityListDynamic(Country, filter, "");

            return ((from s in customers.Take(50).ToList() select new { CityProvince = s.City + "," + s.State_Province }).ToSelectList(x => x.CityProvince, x => x.CityProvince,
                                                     "ALL"));

        }


        #endregion

        #region GetFilteredCities
        //public SelectList GetFilteredCities(string filter, string Country, int offset)
        //{
        //    //public CityPostalCode MySelectedCityPostalCode

        //    var customers = _georepository.GetCityListDynamic(Country, filter, "").Take(50);

        //    SelectList customersList = new SelectList((from s in customers.ToList() select new CityStateProvince { StateProvince = s.City + "," + s.State_Province, City = s.City }), "StateProvince", "StateProvince", false);

        //    return customersList;
        //}


        public List<string> GetFilteredCities(string filter, string Country, int offset)
        {

            List<string> temp;
            try
            {
                var customers = _georepository.GetCityListDynamic(Country, filter, "").Take(50);

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
        //    var customers = _georepository.GetPostalCodesByCountryAndCityPrefixDynamic(Country, City, filter);

        //   // SelectList customersList = new SelectList(customers.Skip(offset).Take(25), "PostalCode", "PostalCode", false);

        //    SelectList customersList = new SelectList(customers.ToList(), "PostalCode", "PostalCode", false);


        //    return customersList;
        //}


        public List<string> GetFilteredPostalCodes(string filter, string Country, string City, int offset)
        {

            var customers = _georepository.GetPostalCodesByCountryAndCityPrefixDynamic(Country, City, filter);

            return (customers.Skip(offset).Take(25).ToSelectList(x => x.PostalCode, x => x.PostalCode, ""));

        }

        #endregion

        #endregion

        #region "Search Settings Collections Here"

        //public IList<ShowMe> ShowMeCheckBoxList()
        //{
        //    IList<ShowMe> test;
        //    test = _datingcontext.GetShowMes().ToList();
        //    return test;
        //}

        //public  List<SortByType> SortByCheckBoxList()
        //{
        //    List<SortByType> test;
        //    test = _datingcontext.GetSortByTypes().ToList();
        //    return test;
        //}



        #endregion


    }
}
