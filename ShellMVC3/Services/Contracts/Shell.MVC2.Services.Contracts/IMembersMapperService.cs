using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using System.ServiceModel.Web;

namespace Shell.MVC2.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IMembersMapperService
    {
          [WebGet]
        [OperationContract]
        MemberSearchViewModel MapMemberSearchViewModel(string profileId);
         [WebGet]
        [OperationContract]
        List<MemberSearchViewModel> MapMemberSearchViewModels(List<string> profileIds);
         
        [WebGet]
        [OperationContract]
        ProfileBrowseModel MapProfileBrowseModel(string viewerprofileId, string profileId);

          [WebGet]
        [OperationContract]
        List<ProfileBrowseModel> MapProfileBrowseModels(string viewerprofileId, List<string> profileIds);

          [WebGet]
        [OperationContract]
        ProfileCriteriaModel MapProfileCriteriaModel(MemberSearchViewModel p);

          [WebGet]
        [OperationContract]
        ProfileCriteriaModel MapProfileCriteriaModel();
        // TODO: Add your service operations here
    }

}
