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

using Shell.MVC2.Infrastructure;

using Shell.MVC2.Interfaces;
using Shell.MVC2.AppFabric ;







namespace Shell.MVC2.Data
{
    public class LookupRepository : MemberRepositoryBase, ILookupRepository
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

        #region "generic lookup and collections"

        public List<lu_gender> genderlist
        {
            get
            {


#if DISCONECTED
                List<lu_gender> genderlist = new List<lu_gender>();
                genderlist.Add(new lu_gender { description = "Male",  id  = 1, selected   = false });
                genderlist.Add(new lu_gender { description = "Female", id = 2, selected = false });
                return genderlist;
                
#else



                return _datingcontext.lu_gender.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }
       
        public List<age> agelist
        {
            get
            {
               return  CachingFactory.SharedObjectHelper.getagelist();
            }
        }

        public List<age> createagelist
        {
    
        get
        {
    
        List<age> tmplist = new List<age>();
                    // Loop over the int List and modify it.
                    //insert the first one as ANY
                    tmplist.Add(new age { ageindex = "0", agevalue = "Any" });

                    for (int i = 18; i < 100; i++)
                    {

                        var CurrentAge = new age { ageindex = i.ToString(), agevalue = i.ToString() };
                        tmplist.Add(CurrentAge);
                    }
                    return tmplist;
            }

         }

        #endregion

        #region "Shared Collections retrived here"

        //public SelectList GendersSelectList()
        //{
        //    SelectList genders = new SelectList(_datingcontext.GetGenders().ToList(), "GenderID", "GenderName");
        //    return genders;
        //}
                
        #region "Criteria Appearance dropdowns"

        public List<MetricHeights> heightmetricselectlist
        {

            get
            {
                List<MetricHeights> templist = new List<MetricHeights>();
                // Loop over the int List and modify it.
                //insert the first one as ANY
                templist.Add(new MetricHeights { heightindex = "0", heightvalue = "Any" });

                for (int i = 48; i < 89; i++)
                {

                    var CurrentHeight = new MetricHeights { heightindex = i.ToString(), heightvalue = Extensions.ToFeetInches(i) };
                    templist.Add(CurrentHeight);

                }

                return templist;
            }

        }

        public List<lu_ethnicity> ethnicitylist
        {
            get
            {


#if DISCONECTED
                List<lu_ethnicity> ethnicitylist = new List<lu_ethnicity>();
                ethnicitylist.Add(new lu_ethnicity { description = "Male",  id  = 1, selected   = false });
                ethnicitylist.Add(new lu_ethnicity { description = "Female", id = 2, selected = false });
                return ethnicitylist;
                
#else



                return _datingcontext.lu_ethnicity.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }
        public List<lu_bodytype> bodytypelist
        {
            get
            {


#if DISCONECTED
                List<lu_bodytype> bodytypelist = new List<lu_bodytype>();
                bodytypelist.Add(new lu_bodytype { description = "Male",  id  = 1, selected   = false });
                bodytypelist.Add(new lu_bodytype { description = "Female", id = 2, selected = false });
                return bodytypelist;
                
#else



                return _datingcontext.lu_bodytype.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }
        public List<lu_eyecolor> eyecolorlist
        {
            get
            {


#if DISCONECTED
                List<lu_eyecolor> eyecolorlist = new List<lu_eyecolor>();
                eyecolorlist.Add(new lu_eyecolor { description = "Male",  id  = 1, selected   = false });
                eyecolorlist.Add(new lu_eyecolor { description = "Female", id = 2, selected = false });
                return eyecolorlist;
                
#else



                return _datingcontext.lu_eyecolor.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }      
        public List<lu_haircolor> haircolorlist
        {
            get
            {


#if DISCONECTED
                List<lu_haircolor> haircolorlist = new List<lu_haircolor>();
                haircolorlist.Add(new lu_haircolor { description = "Male",  id  = 1, selected   = false });
                haircolorlist.Add(new lu_haircolor { description = "Female", id = 2, selected = false });
                return haircolorlist;
                
#else



                return _datingcontext.lu_haircolor.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }
        public List<lu_gender> genderlist
        {
            get
            {


#if DISCONECTED
                List<lu_gender> genderlist = new List<lu_gender>();
                genderlist.Add(new lu_gender { description = "Male",  id  = 1, selected   = false });
                genderlist.Add(new lu_gender { description = "Female", id = 2, selected = false });
                return genderlist;
                
#else



                return _datingcontext.lu_gender.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }
        #endregion

        #region "Criteria Character Dropdowns"

        public List<lu_diet> dietlist
        {
            get
            {


#if DISCONECTED
                List<lu_diet> dietlist = new List<lu_diet>();
                dietlist.Add(new lu_diet { description = "Male",  id  = 1, selected   = false });
                dietlist.Add(new lu_diet { description = "Female", id = 2, selected = false });
                return dietlist;
                
#else



                return _datingcontext.lu_diet.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }
        public List<lu_drinks> drinkslist
        {
            get
            {


#if DISCONECTED
                List<lu_drinks> drinkslist = new List<lu_drinks>();
                drinkslist.Add(new lu_drinks { description = "Male",  id  = 1, selected   = false });
                drinkslist.Add(new lu_drinks { description = "Female", id = 2, selected = false });
                return drinkslist;
                
#else



                return _datingcontext.lu_drinks.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }
        public List<lu_exercise> exerciselist
        {
            get
            {


#if DISCONECTED
                List<lu_exercise> exerciselist = new List<lu_exercise>();
                exerciselist.Add(new lu_exercise { description = "Male",  id  = 1, selected   = false });
                exerciselist.Add(new lu_exercise { description = "Female", id = 2, selected = false });
                return exerciselist;
                
#else



                return _datingcontext.lu_exercise.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }
        public List<lu_hobby> hobbylist
        {
            get
            {


#if DISCONECTED
                List<lu_hobby> hobbylist = new List<lu_hobby>();
                hobbylist.Add(new lu_hobby { description = "Male",  id  = 1, selected   = false });
                hobbylist.Add(new lu_hobby { description = "Female", id = 2, selected = false });
                return hobbylist;
                
#else



                return _datingcontext.lu_hobby.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }
        public List<lu_humor> humorlist
        {
            get
            {


#if DISCONECTED
                List<lu_humor> humorlist = new List<lu_humor>();
                humorlist.Add(new lu_humor { description = "Male",  id  = 1, selected   = false });
                humorlist.Add(new lu_humor { description = "Female", id = 2, selected = false });
                return humorlist;
                
#else



                return _datingcontext.lu_humor.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }
        public List<lu_politicalview> politicalviewlist
        {
            get
            {


#if DISCONECTED
                List<lu_politicalview> politicalviewlist = new List<lu_politicalview>();
                politicalviewlist.Add(new lu_politicalview { description = "Male",  id  = 1, selected   = false });
                politicalviewlist.Add(new lu_politicalview { description = "Female", id = 2, selected = false });
                return politicalviewlist;
                
#else



                return _datingcontext.lu_politicalview.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }
        public List<lu_religion> religionlist
        {
            get
            {


#if DISCONECTED
                List<lu_religion> religionlist = new List<lu_religion>();
                religionlist.Add(new lu_religion { description = "Male",  id  = 1, selected   = false });
                religionlist.Add(new lu_religion { description = "Female", id = 2, selected = false });
                return religionlist;
                
#else



                return _datingcontext.lu_religion.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }
        public List<lu_religiousattendance> religiousattendancelist
        {
            get
            {


#if DISCONECTED
                List<lu_religiousattendance> religiousattendancelist = new List<lu_religiousattendance>();
                religiousattendancelist.Add(new lu_religiousattendance { description = "Male",  id  = 1, selected   = false });
                religiousattendancelist.Add(new lu_religiousattendance { description = "Female", id = 2, selected = false });
                return religiousattendancelist;
                
#else



                return _datingcontext.lu_religiousattendance.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }
        public List<lu_sign> signlist
        {
            get
            {


#if DISCONECTED
                List<lu_sign> signlist = new List<lu_sign>();
                signlist.Add(new lu_sign { description = "Male",  id  = 1, selected   = false });
                signlist.Add(new lu_sign { description = "Female", id = 2, selected = false });
                return signlist;
                
#else



                return _datingcontext.lu_sign.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }
        public List<lu_smokes> smokeslist
        {
            get
            {


#if DISCONECTED
                List<lu_smokes> smokeslist = new List<lu_smokes>();
                smokeslist.Add(new lu_smokes { description = "Male",  id  = 1, selected   = false });
                smokeslist.Add(new lu_smokes { description = "Female", id = 2, selected = false });
                return smokeslist;
                
#else



                return _datingcontext.lu_smokes.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }


        #endregion

        #region "Criteria Lifestyle Dropdowns"

        public List<lu_educationlevel> educationlevellist
        {
            get
            {


#if DISCONECTED
                List<lu_educationlevel> educationlevellist = new List<lu_educationlevel>();
                educationlevellist.Add(new lu_educationlevel { description = "Male",  id  = 1, selected   = false });
                educationlevellist.Add(new lu_educationlevel { description = "Female", id = 2, selected = false });
                return educationlevellist;
                
#else



                return _datingcontext.lu_educationlevel.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }
        public List<lu_employmentstatus> employmentstatuslist
        {
            get
            {


#if DISCONECTED
                List<lu_employmentstatus> employmentstatuslist = new List<lu_employmentstatus>();
                employmentstatuslist.Add(new lu_employmentstatus { description = "Male",  id  = 1, selected   = false });
                employmentstatuslist.Add(new lu_employmentstatus { description = "Female", id = 2, selected = false });
                return employmentstatuslist;
                
#else



                return _datingcontext.lu_employmentstatus.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }
        public List<lu_havekids> havekidslist
        {
            get
            {


#if DISCONECTED
                List<lu_havekids> havekidslist = new List<lu_havekids>();
                havekidslist.Add(new lu_havekids { description = "Male",  id  = 1, selected   = false });
                havekidslist.Add(new lu_havekids { description = "Female", id = 2, selected = false });
                return havekidslist;
                
#else



                return _datingcontext.lu_havekids.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }
        public List<lu_incomelevel> incomelevellist
        {
            get
            {


#if DISCONECTED
                List<lu_incomelevel> incomelevellist = new List<lu_incomelevel>();
                incomelevellist.Add(new lu_incomelevel { description = "Male",  id  = 1, selected   = false });
                incomelevellist.Add(new lu_incomelevel { description = "Female", id = 2, selected = false });
                return incomelevellist;
                
#else



                return _datingcontext.lu_incomelevel.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }
        public List<lu_livingsituation> livingsituationlist
        {
            get
            {


#if DISCONECTED
                List<lu_livingsituation> livingsituationlist = new List<lu_livingsituation>();
                livingsituationlist.Add(new lu_livingsituation { description = "Male",  id  = 1, selected   = false });
                livingsituationlist.Add(new lu_livingsituation { description = "Female", id = 2, selected = false });
                return livingsituationlist;
                
#else



                return _datingcontext.lu_livingsituation.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }
        public List<lu_lookingfor> lookingforlist
        {
            get
            {


#if DISCONECTED
                List<lu_lookingfor> lookingforlist = new List<lu_lookingfor>();
                lookingforlist.Add(new lu_lookingfor { description = "Male",  id  = 1, selected   = false });
                lookingforlist.Add(new lu_lookingfor { description = "Female", id = 2, selected = false });
                return lookingforlist;
                
#else



                return _datingcontext.lu_lookingfor.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }
        public List<lu_maritalstatus> maritalstatuslist
        {
            get
            {


#if DISCONECTED
                List<lu_maritalstatus> maritalstatuslist = new List<lu_maritalstatus>();
                maritalstatuslist.Add(new lu_maritalstatus { description = "Male",  id  = 1, selected   = false });
                maritalstatuslist.Add(new lu_maritalstatus { description = "Female", id = 2, selected = false });
                return maritalstatuslist;
                
#else



                return _datingcontext.lu_maritalstatus.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }
        public List<lu_profession> professionlist
        {
            get
            {


#if DISCONECTED
                List<lu_profession> professionlist = new List<lu_profession>();
                professionlist.Add(new lu_profession { description = "Male",  id  = 1, selected   = false });
                professionlist.Add(new lu_profession { description = "Female", id = 2, selected = false });
                return professionlist;
                
#else



                return _datingcontext.lu_profession.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            }
        }
        public List<lu_wantskids> wantskidslist
        {
            get
            {


#if DISCONECTED
                List<lu_wantskids> wantskidslist = new List<lu_wantskids>();
                wantskidslist.Add(new lu_wantskids { description = "Male",  id  = 1, selected   = false });
                wantskidslist.Add(new lu_wantskids { description = "Female", id = 2, selected = false });
                return wantskidslist;
                
#else



                return _datingcontext.lu_wantskids.OrderBy(x => x.description).ToList();



                // return temp;
#endif

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

                temp = ((from s in customers.ToList() select new CityStateProvince { stateprovince = s.City + "," + s.State_Province, city = s.City }).ToSelectList(x => x.stateprovince, x => x.stateprovince, null
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

       

        #endregion


    }
}
