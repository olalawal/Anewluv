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
                .Include(x => x.profilemetadata.profile) 
                  .Include(x => x.profilemetadata.profile.profiledata)   
                .Include(y => y.details  )             
                .Include(y => y.locations)
                
               

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
                .Include(x => x.profilemetadata.profile)
                  .Include(x => x.profilemetadata.profile.profiledata)
                .Include(y => y.details)
                .Include(y => y.locations)
                .Select().FirstOrDefault();



                return mysearchsettings;
            }
            catch (Exception ex)
            { throw ex; }
        }

    }
}
