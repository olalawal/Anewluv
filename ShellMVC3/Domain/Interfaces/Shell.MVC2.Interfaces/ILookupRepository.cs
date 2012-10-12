using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Domain.Entities.Anewluv ;

namespace Shell.MVC2.Interfaces
{
    public interface ILookupRepository
    {



        

            public List<systempagesetting> GetSystemPageSettingList;
            
          

          

            #region "generic lookup and collections"

            public List<lu_gender> genderlist;
           
            public List<age> ageslist;

            public List<age> createagelist;
           

            #endregion

            #region "Shared Collections retrived here"

            

            #region "Criteria Appearance dropdowns"

            public List<metricheights> heightmetriclist;    
        
            public List<metricheights> createheightmetriclist; 

            public List<lu_ethnicity> ethnicitylist;
           
            public List<lu_bodytype> bodytypelist;
           
            public List<lu_eyecolor> eyecolorlist;
           
            public List<lu_haircolor> haircolorlist;
            
            public List<lu_gender> genderlist;
           
            #endregion

            #region "Criteria Character Dropdowns"

            public List<lu_diet> dietlist;
            
            public List<lu_drinks> drinkslist;
           
            public List<lu_exercise> exerciselist;
           
            public List<lu_hobby> hobbylist;
            
            public List<lu_humor> humorlist;
           
            public List<lu_politicalview> politicalviewlist;
            
            public List<lu_religion> religionlist;
            
            public List<lu_religiousattendance> religiousattendancelist;
           
            public List<lu_sign> signlist;
            
            public List<lu_smokes> smokeslist;
            


            #endregion

            #region "Criteria Lifestyle Dropdowns"

            public List<lu_educationlevel> educationlevellist;
            
            public List<lu_employmentstatus> employmentstatuslist;
            
            public List<lu_havekids> havekidslist;
            
            public List<lu_incomelevel> incomelevellist;
            
            public List<lu_livingsituation> livingsituationlist;
           
            public List<lu_lookingfor> lookingforlist;
            
            public List<lu_maritalstatus> maritalstatuslist;
            
            public List<lu_profession> professionlist;
            
            public List<lu_wantskids> wantskidslist;
           

            #endregion

            public List<string> CountrySelectList(string CountryName);
            
            public List<string> CountrySelectList();
           
            #region GetFilteredCitiesOld
            

            public List<string> GetFilteredCitiesOld(string filter, string Country, int offset);
            

            #endregion

            #region GetFilteredCities
            
            public List<string> GetFilteredCities(string filter, string Country, int offset);
            
            #endregion

            #region GetFilteredPostalCodes
            

            public List<string> GetFilteredPostalCodes(string filter, string Country, string City, int offset);
            
            #endregion

            #endregion

            #region "Search Settings Collections Here"



            #endregion


        }





    }
}