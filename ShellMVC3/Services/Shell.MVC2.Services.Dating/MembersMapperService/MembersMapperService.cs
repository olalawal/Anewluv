using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Dating.Server.Data.ViewModels;
using Shell.MVC2.Interfaces;
using Shell.MVC2.Services.Contracts;

namespace Shell.MVC2.Services.Dating
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersMapperService" in both code and config file together.
    public class MembersMapperService : IMembersMapperService
    {
        private IMembersMapperRepository _mapmembersRepo;

        public MembersMapperService(IMembersMapperRepository mapmembersRepo)
            {
                _mapmembersRepo = mapmembersRepo;
            }


        public MemberSearchViewModel MapMemberSearchViewModel(string profileId)
        {
          return   _mapmembersRepo.GetMemberSearchViewModel(profileId);
        }

      
        public List<MemberSearchViewModel> MapMemberSearchViewModels(List<string> profileIds)
        {

            return _mapmembersRepo.GetMemberSearchViewModels(profileIds);
        }

 
        public ProfileBrowseModel MapProfileBrowseModel(string viewerprofileId, string profileId)
        {
            return _mapmembersRepo.GetProfileBrowseModel(viewerprofileId, profileId);

        }



        public List<ProfileBrowseModel> MapProfileBrowseModels(string viewerprofileId, List<string> profileIds)
        {
            return _mapmembersRepo.GetProfileBrowseModels(viewerprofileId, profileIds);

        }

    
        public ProfileCriteriaModel MapProfileCriteriaModel(MemberSearchViewModel p)
        {
            return _mapmembersRepo.GetProfileCriteriaModel(p);
        }

          
        public   ProfileCriteriaModel MapProfileCriteriaModel()
        {

            return _mapmembersRepo.GetProfileCriteriaModel();
        }
    


    }
}
