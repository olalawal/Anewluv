using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;

namespace Shell.MVC2.Interfaces
{
   public interface IMembersMapperRepository
    {

    
        MemberSearchViewModel GetMemberSearchViewModel(string profileId);
     
        List<MemberSearchViewModel> GetMemberSearchViewModels(List<string> profileIds);
   
        ProfileBrowseModel GetProfileBrowseModel(string viewerprofileId, string profileId);
  
        List<ProfileBrowseModel> GetProfileBrowseModels(string viewerprofileId, List<string> profileIds);
   
        ProfileCriteriaModel GetProfileCriteriaModel(MemberSearchViewModel p);
       
        ProfileCriteriaModel GetProfileCriteriaModel();

    }
}
