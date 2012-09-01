using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Domain.Entities.Anewluv ;

namespace Shell.MVC2.Interfaces
{
    interface ILookupRepository
    {




        #region "Load Other misc stuff such as list of pages etc"

         List<systempagesetting> GetSystemPageSettingList();



        #endregion

        #region "Shared Collections retrived here"

         List<string> GendersSelectList();

         List<string> VisibilityMailSettingsList();

         List<string> VisibilityStealthSettingsList();

         List<string> SecurityQuestionSelectList();

         List<string> AgesSelectList();
        #endregion

         #region "Criteria Appearance dropdowns"

         List<string> HeightMetricSelectList();

         List<string> BodyTypesSelectList();

         List<string> EyeColorSelectList();

         List<string> HairColorSelectList();

        #endregion

        #region "Criteria Character Dropdowns"

         List<string> DietSelectList();

         List<string> DrinksSelectList();

         List<string> ExerciseSelectList();

         List<string> HobbySelectList();

         List<string> HumorSelectList();

         List<string> PoliticalViewSelectList();

         List<string> ReligionSelectList();

         List<string> ReligiousAttendanceSelectList();

         List<string> SignSelectList();

         List<string> SmokesSelectList();


        #endregion

        #region "Criteria Lifestyle Dropdowns"


         List<string> EducationLevelSelectList();

         List<string> EmploymentStatusSelectList();

         List<string> HaveKidsSelectList();

         List<string> IncomeLevelSelectList();

         List<string> LivingSituationSelectList();

         List<string> MaritalStatusSelectList();

         List<string> LookingForSelectList();

         List<string> ProfessionSelectList();

         List<string> WantsKidsSelectList();

        #endregion

        #region "location based lists"
         List<string> CountrySelectList(string CountryName);

         List<string> CountrySelectList();

         List<string> GetFilteredCitiesOld(string filter, string Country, int offset);

         List<string> GetFilteredCities(string filter, string Country, int offset);

         List<string> GetFilteredPostalCodes(string filter, string Country, string City, int offset);


        #endregion







    }
}