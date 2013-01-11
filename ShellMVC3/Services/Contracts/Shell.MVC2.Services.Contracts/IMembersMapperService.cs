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

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getmembersearchviewmodel/{viewerprofileid}/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        MemberSearchViewModel getmembersearchviewmodel(string viewerprofileid,string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/getmembersearchviewmodels/{viewerprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<MemberSearchViewModel> getmembersearchviewmodels(string viewerprofileid,List<int> profileids);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getprofilebrowsemodel/{viewerprofileid}/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ProfileBrowseModel getprofilebrowsemodel(string viewerprofileid, string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/getprofilebrowsemodels/{viewerprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<ProfileBrowseModel> getprofilebrowsemodels(string viewerprofileid, List<int> profileids);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getprofilecriteriamodel/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ProfileCriteriaModel getprofilecriteriamodel(string profileid);       //overload for above

        [OperationContract(Name = "getprofilecriteriamodelempty"), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getprofilecriteriamodel", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ProfileCriteriaModel getprofilecriteriamodel();

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/getdefaultquicksearchsettingsmembers", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        MembersViewModel getdefaultquicksearchsettingsmembers(MembersViewModel Model);
        //populate search settings for guests 

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/createmyperfectmatchsearchsettingsbyprofileid", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        MembersViewModel getdefaultsearchsettingsguest(MembersViewModel Model);

        //registration model mapping
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/getregistermodel", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        registermodel getregistermodel(MembersViewModel membersmodel);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/getregistermodelopenid", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        //TO DO this should be using profileID or a composite model , add rpxprofile to members model
        registermodel getregistermodelopenid(MembersViewModel membersmodel);
      
        //exposed methods that use cache, we should prpbbaly hdie the other methods that are not cached
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getregistermodeltest", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        registermodel getregistermodeltest();

        // MembersViewModel updatemembersviewmodel(MembersViewModel model);
        //send the full model to app fapbric   
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/updatememberdata", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        MembersViewModel updatememberdata(MembersViewModel model);
        
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/updatememberdatabyprofileid/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        MembersViewModel updatememberdatabyprofileid(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/updateguestdata", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool updateguestdata(MembersViewModel model);
        
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/removeguestdata", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool removeguestdata(string sessionid);
        //cacheing of search stuff
        
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getguestdata/{sessionid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        MembersViewModel getguestdata(string sessionid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getmemberdata/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        MembersViewModel getmemberdata(string profileid);

        //mapping functions for members VM
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/mapmember/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        MembersViewModel mapmember(string profileid);
      
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/mapguest", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        MembersViewModel mapguest();


        //TO DO move these functions to a Matches or Search Services Area and repoisitory eventually
        //***********************************************************************************************
        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/getquickmatches", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<MemberSearchViewModel> getquickmatches(MembersViewModel model);

        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/getemailmatches", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<MemberSearchViewModel> getemailmatches(MembersViewModel model);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/getquickmatcheswhenquickmatchesempty", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<MemberSearchViewModel> getquickmatcheswhenquickmatchesempty(MembersViewModel model);

    }

}
