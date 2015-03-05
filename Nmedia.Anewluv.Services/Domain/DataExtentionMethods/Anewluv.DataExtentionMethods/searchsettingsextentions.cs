using Anewluv.Domain.Data;
using Anewluv.Domain.Data.ViewModels;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anewluv.DataExtentionMethods
{
    public static class searchsettingsextentions
    {

        //generic filtering function we can reuse, filters all search settings using profileid,searchname and other data
        public static searchsetting filtersearchsettings(this IRepository<searchsetting> repo, SearchSettingsModel searchmodel)
        {

            try
            {
                //This query assumes that one search is always called default and cannot be deleted dont like that
                List<searchsetting> allsearchsettings = new List<searchsetting>();
                searchsetting p = new searchsetting();

                //default handling for empty profile ID and other search data
                if (searchmodel == null) return p;

                allsearchsettings = repo.Query
                (z => (searchmodel.searchid != 0 && z.id == searchmodel.searchid) ||
                (searchmodel.profileid != 0 && (z.profile_id == searchmodel.profileid)))
                .Include(x => x.profilemetadata)
                .Include(y => y.searchsetting_bodytype)
                .Include(y => y.searchsetting_diet)
                .Include(y => y.searchsetting_drink)
                .Include(y => y.searchsetting_educationlevel)
                .Include(y => y.searchsetting_employmentstatus)
                .Include(y => y.searchsetting_ethnicity)
                .Include(y => y.searchsetting_exercise)
                .Include(y => y.searchsetting_eyecolor)
                .Include(y => y.searchsetting_gender)
                .Include(y => y.searchsetting_haircolor)
                .Include(y => y.searchsetting_havekids)
                .Include(y => y.searchsetting_hobby)
                .Include(y => y.searchsetting_hotfeature)
                .Include(y => y.searchsetting_humor)
                .Include(y => y.searchsetting_incomelevel)
                .Include(y => y.searchsetting_livingstituation)
                .Include(y => y.searchsetting_location)
                .Include(y => y.searchsetting_lookingfor)
                .Include(y => y.searchsetting_maritalstatus)
                .Include(y => y.searchsetting_politicalview)
                .Include(y => y.searchsetting_profession)
                .Include(y => y.searchsetting_religion)
                .Include(y => y.searchsetting_religiousattendance)
                .Include(y => y.searchsetting_showme)
                .Include(y => y.searchsetting_sign)
                .Include(y => y.searchsetting_smokes)
                .Include(y => y.searchsetting_sortbytype)
                .Include(y => y.searchsetting_wantkids)
                .Include(y => y.systemmatch)

                .Select().ToList();

                if (allsearchsettings.Count() > 0 & searchmodel.searchname != null)//|searchmodel.searchname != ""  )
                {
                    p = allsearchsettings.Where(z => z.searchname == searchmodel.searchname).FirstOrDefault();
                }
                else if (allsearchsettings.Count() > 0)
                {
                    p = allsearchsettings.OrderByDescending(z => z.creationdate).FirstOrDefault();  //get the first one thats probbaly the default.
                }

                return p;
            }
            catch (Exception ex)
            { throw ex; }
        }

        //generic filtering function we can reuse
        public static searchsetting getsearchsettingsbysearchid(this IRepository<searchsetting> repo,int? searchid)
        {

            try
            {
                //This query assumes that one search is always called default and cannot be deleted dont like that
                searchsetting mysearchsettings = new searchsetting();

                //default handling for empty profile ID and other search data

                mysearchsettings = repo.Query(z => z.id == searchid)
                .Include(x => x.profilemetadata)
                .Include(y => y.searchsetting_bodytype)
                .Include(y => y.searchsetting_diet)
                .Include(y => y.searchsetting_drink)
                .Include(y => y.searchsetting_educationlevel)
                .Include(y => y.searchsetting_employmentstatus)
                .Include(y => y.searchsetting_ethnicity)
                .Include(y => y.searchsetting_exercise)
                .Include(y => y.searchsetting_eyecolor)
                .Include(y => y.searchsetting_gender)
                .Include(y => y.searchsetting_haircolor)
                .Include(y => y.searchsetting_havekids)
                .Include(y => y.searchsetting_hobby)
                .Include(y => y.searchsetting_hotfeature)
                .Include(y => y.searchsetting_humor)
                .Include(y => y.searchsetting_incomelevel)
                .Include(y => y.searchsetting_livingstituation)
                .Include(y => y.searchsetting_location)
                .Include(y => y.searchsetting_lookingfor)
                .Include(y => y.searchsetting_maritalstatus)
                .Include(y => y.searchsetting_politicalview)
                .Include(y => y.searchsetting_profession)
                .Include(y => y.searchsetting_religion)
                .Include(y => y.searchsetting_religiousattendance)
                .Include(y => y.searchsetting_showme)
                .Include(y => y.searchsetting_sign)
                .Include(y => y.searchsetting_smokes)
                .Include(y => y.searchsetting_sortbytype)
                .Include(y => y.searchsetting_wantkids)
                .Select().FirstOrDefault();



                return mysearchsettings;
            }
            catch (Exception ex)
            { throw ex; }
        }

    }
}
