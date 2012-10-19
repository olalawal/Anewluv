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
        MemberSearchViewModel getmembersearchviewmodel(int profileid);
       
        [WebInvoke ]
        [OperationContract]
        List<MemberSearchViewModel> getmembersearchviewmodels(List<int> profileIds);
        [WebGet]
        [OperationContract]
        ProfileBrowseModel getprofilebrowsemodel(int viewerprofileId, int profileid);
        [WebInvoke]
        [OperationContract]
        List<ProfileBrowseModel> getprofilebrowsemodels(int viewerprofileId, List<int> profileIds);
        [WebGet]
        [OperationContract]
        ProfileCriteriaModel getprofilecriteriamodel(int profileid);       //overload for above
        [WebGet]
        [OperationContract]
        ProfileCriteriaModel getprofilecriteriamodel();
        [WebInvoke]
        [OperationContract]
        MembersViewModel getdefaultquicksearchsettingsmembers(MembersViewModel Model);
        //populate search settings for guests 
        [WebInvoke]
        [OperationContract]
        MembersViewModel getdefaultsearchsettingsguest(MembersViewModel Model);




        //registration model mapping
        [WebInvoke]
        [OperationContract]
        RegisterModel getregistermodel(MembersViewModel membersmodel);
        [WebInvoke]
        [OperationContract]
        //TO DO this should be using profileID or a composite model , add rpxprofile to members model
        RegisterModel getregistermodelopenid(MembersViewModel membersmodel);
        //exposed methods that use cache, we should prpbbaly hdie the other methods that are not cached
        [WebGet]
        [OperationContract]
        RegisterModel getregistermodeltest();



        // MembersViewModel updatemembersviewmodel(MembersViewModel model);
        //send the full model to app fapbric   
        [WebInvoke]
        [OperationContract]
        MembersViewModel updatememberdata(MembersViewModel model);
        [WebInvoke]
        [OperationContract]
        MembersViewModel updatememberdatabyprofileid(int profileid);
        [WebInvoke]
        [OperationContract]
        bool updateguestdata(MembersViewModel model);
        [WebInvoke]
        [OperationContract]
        bool removeguestdata(string sessionid);
        //cacheing of search stuff
        [WebGet]
        [OperationContract]
        MembersViewModel getguestdata(string sessionid);
        [WebGet]
        [OperationContract]
        MembersViewModel getmemberdata(int profileid);

        //mapping functions for members VM
        [WebGet]
        [OperationContract]
        MembersViewModel mapmember(int profileid);
        [WebGet]
        [OperationContract]
        MembersViewModel mapguest();
    }

}
