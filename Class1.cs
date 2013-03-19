using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Class1
{

    public List<MemberSearchViewModel> getwhoiaminterestedin(int profileid, int? Page, int? NumberPerPage)
    {
        //gets all  interestets from the interest table based on if the person's profiles are stil lvalid tho
        try
        {


            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileid && p.removedate == null)
                                 select new
                                 {
                                     ProfilesBlockedId = c.blockprofile_id
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
            var interests = (from p in _datingcontext.interests.Where(p => p.profile_id == profileid && p.deletedbymemberdate == null)
                             join f in _datingcontext.profiledata on p.interestprofile_id equals f.profile_id
                             join z in _datingcontext.profiles on p.interestprofile_id equals z.id
                             where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profile_id))
                             select new MemberSearchViewModel
                             {
                                 interestdate = p.creationdate,
                                 id = f.profile_id
                                 // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                             }).ToList();
            // var dd2 = 0;
            //var dd = 2 /  dd2;

            bool allowpaging = (interests.Count >= (Page.GetValueOrDefault() * NumberPerPage.GetValueOrDefault()) ? true : false);
            var pageData = Page.GetValueOrDefault() > 1 & allowpaging ?
                new PaginatedList<MemberSearchViewModel>().GetCurrentPages(interests, Page ?? 1, NumberPerPage ?? 4) : interests.Take(NumberPerPage.GetValueOrDefault());
            //this.AddRange(pageData.ToList());
            // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

            //return interests.ToList();
            return _membermapperrepository.mapmembersearchviewmodels(profileid, pageData.ToList(), false).OrderByDescending(f => f.interestdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();

            // return data2.OrderByDescending(f => f.interestdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();;
            //.OrderByDescending(f => f.interestdate ?? DateTime.MaxValue).ToList();

        }
        catch (Exception ex)
        {
            //instantiate logger here so it does not break anything else.
            new ErroLogging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
            //logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
            //log error mesasge
            //handle logging here
            var message = ex.Message;
            throw;
        }


    }


    public Class1()
    {
        try
        {





        }
        catch (Exception ex)
        {
            //instantiate logger here so it does not break anything else.
            new ErroLogging(applicationEnum.LookupService).WriteSingleEntry(logseverityEnum.CriticalError, ex, null, null);
            //logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
            //log error mesasge
            //handle logging here
            var message = ex.Message;
            throw;
        }
    }


	public Class1()
	{
        try
        {


            
            bool allowpaging = (interests.Count >= (Page.GetValueOrDefault() * NumberPerPage.GetValueOrDefault()) ? true : false);
            var pageData = Page.GetValueOrDefault() > 1 & allowpaging ?
                new PaginatedList<MemberSearchViewModel>().GetCurrentPages(interests, Page ?? 1, NumberPerPage ?? 4) : interests.Take(NumberPerPage.GetValueOrDefault());
            //this.AddRange(pageData.ToList());
            // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

            //return interests.ToList();
            return _membermapperrepository.mapmembersearchviewmodels(profileid, pageData.ToList(), false).OrderByDescending(f => f.interestdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();

            // return data2.OrderByDescending(f => f.interestdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();;
            //.OrderByDescending(f => f.interestdate ?? DateTime.MaxValue).ToList();

        }
        catch (Exception ex)
        {
            //instantiate logger here so it does not break anything else.
            new ErroLogging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
            //logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
            //log error mesasge
            //handle logging here
            var message = ex.Message;
            throw;
        }
	}

    public class2()
    {
        try
        {
        
        
        }
    
      catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }



    }
}
