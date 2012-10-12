using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;

namespace Shell.MVC2.Interfaces
{
   public interface IMembersMapperRepository
    {

    
        MemberSearchViewModel getmembersearchviewmodel(int profileid);
     
        List<MemberSearchViewModel> getmembersearchviewmodels(List<int> profileIds);
   
        ProfileBrowseModel getprofilebrowsemodel(int viewerprofileId, int profileid);
  
        List<ProfileBrowseModel> getprofilebrowsemodels(int viewerprofileId, List<int> profileIds);
   
        ProfileCriteriaModel getprofilecriteriamodel(int profileid);
       //overload for above
        ProfileCriteriaModel getprofilecriteriamodel();

        MembersViewModel mapmember(int profileid);

        MembersViewModel mapguest();

        RegisterModel mapregistrationtest();

        RegisterModel mapregistration(MembersViewModel membersmodel);

       //TO DO this should be using profileID or a composite model , add rpxprofile to members model
        RegisterModel mapjainrainregistration(MembersViewModel membersmodel);

       //exposed methods that use cache, we should prpbbaly hdie the other methods that are not cached
        MembersViewModel getmemberdatafromcache(int profileid);

        MembersViewModel getguestdatafromcache(string sessionid);

        MembersViewModel updatememberdatafromcache(MembersViewModel model);

       //send the full model to app fapbric
        MembersViewModel updatememberprofiledatabyprofileidfromcache(int profileid);

        MembersViewModel updateguestdatafromcache(MembersViewModel model);

        bool removememberdatafromcache(string profileid);

       //cacheing of search stuff


    }
}
