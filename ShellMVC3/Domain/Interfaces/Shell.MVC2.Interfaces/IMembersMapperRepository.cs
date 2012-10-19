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
        ProfileCriteriaModel getprofilecriteriamodel(int profileid);       //overload for above
        ProfileCriteriaModel getprofilecriteriamodel();
         MembersViewModel getdefaultquicksearchsettingsmembers(MembersViewModel Model);     
        //populate search settings for guests 
         MembersViewModel getdefaultsearchsettingsguest(MembersViewModel Model);
        
       
       
      
       //registration model mapping
        RegisterModel getregistermodel(MembersViewModel membersmodel);
       //TO DO this should be using profileID or a composite model , add rpxprofile to members model
        RegisterModel getregistermodelopenid(MembersViewModel membersmodel);
       //exposed methods that use cache, we should prpbbaly hdie the other methods that are not cached
         RegisterModel getregistermodeltest();
       
           
       
       // MembersViewModel updatemembersviewmodel(MembersViewModel model);
       //send the full model to app fapbric      
        MembersViewModel updatememberdata(MembersViewModel model);
        MembersViewModel updatememberdatabyprofileid(int profileid);  
        bool updateguestdata(MembersViewModel model);
        bool removeguestdata(string sessionid);
       //cacheing of search stuff
        MembersViewModel getguestdata(string sessionid);
        MembersViewModel getmemberdata(int profileid);

       //mapping functions for members VM
        MembersViewModel mapmember(int profileid);
        MembersViewModel mapguest();
        
    }
}
