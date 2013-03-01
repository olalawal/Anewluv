using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;

namespace Shell.MVC2.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMembersService" in both code and config file together.
    [ServiceContract]
    public interface IMemberActionsService
    {
        #region "Interest Methods"

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/getmyrelationshipsfiltered/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]       
        List<MemberSearchViewModel> getmyrelationshipsfiltered(string profileid, List<profilefiltertypeEnum> types);


        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getwhoiaminterestedincount/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        int getwhoiaminterestedincount(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getwhoisinterestedinmecount/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        int getwhoisinterestedinmecount(string profileid);
       
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getwhoisinterestedinmenewcount/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        int getwhoisinterestedinmenewcount(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getwhoiaminterestedin/{profileid}/{page}/{numberperpage}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<MemberSearchViewModel> getwhoiaminterestedin(string profileid, string page, string numberperpage);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getwhoisinterestedinme/{profileid}/{page}/{numberperpage}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<MemberSearchViewModel> getwhoisinterestedinme(string profileid, string page, string numberperpage);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getwhoisinterestedinmenew/{profileid}/{page}/{numberperpage}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<MemberSearchViewModel> getwhoisinterestedinmenew(string profileid, string page, string numberperpage);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getmutualinterests/{profileid}/{targetprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<MemberSearchViewModel> getmutualinterests(string profileid, string targetprofileid);
       
        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/checkinterest/{profileid}/{targetprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool checkinterest(string profileid, string targetprofileid);
        
        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/addinterest/{profileid}/{targetprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool addinterest(string profileid, string targetprofileid);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/removeinterestbyprofileid/{profileid}/{interestprofile_id}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool removeinterestbyprofileid(string profileid, string interestprofile_id);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/removeinterestbyinterestprofileid/{interestprofile_id}/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool removeinterestbyinterestprofileid(string interestprofile_id, string profileid);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/restoreinterestbyprofileid/{profileid}/{interestprofile_id}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool restoreinterestbyprofileid(string profileid, string interestprofile_id);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/restoreinterestbyinterestprofileid/{interestprofile_id}/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool restoreinterestbyinterestprofileid(string interestprofile_id, string profileid);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/removeinterestsbyprofileidandscreennames/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool removeinterestsbyprofileidandscreennames(string profileid, List<String> screennames);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/restoreinterestsbyprofileidandscreennames/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool restoreinterestsbyprofileidandscreennames(string profileid, List<String> screennames);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/updateinterestviewstatus/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool updateinterestviewstatus(string profileid, string targetprofileid);



        #endregion

        #region "peek methods"

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getwhoipeekedatcount/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        int getwhoipeekedatcount(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getwhopeekedatmecount/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        int getwhopeekedatmecount(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getwhopeekedatmenewcount/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        int getwhopeekedatmenewcount(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getwhopeekedatme/{profileid}/{page}/{numberperpage}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<MemberSearchViewModel> getwhopeekedatme(string profileid, string page, string numberperpage);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getwhopeekedatmenew/{profileid}/{page}/{numberperpage}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<MemberSearchViewModel> getwhopeekedatmenew(string profileid, string page, string numberperpage);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getwhoipeekedat/{profileid}/{page}/{numberperpage}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<MemberSearchViewModel> getwhoipeekedat(string profileid, string page, string numberperpage);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getmutualpeeks/{profileid}/{targetprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<MemberSearchViewModel> getmutualpeeks(string profileid, string targetprofileid);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/checkpeek/{profileid}/{targetprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool checkpeek(string profileid, string targetprofileid);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/addpeek/{profileid}/{targetprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool addpeek(string profileid, string targetprofileid);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/removepeekbyprofileid/{profileid}/{peekprofile_id}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool removepeekbyprofileid(string profileid, string peekprofile_id);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/removepeekbypeekprofileid/{peekprofile_id}/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool removepeekbypeekprofileid(string peekprofile_id, string profileid);
        
        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/restorepeekbyprofileid/{profileid}/{peekprofile_id}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool restorepeekbyprofileid(string profileid, string peekprofile_id);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/restorepeekbypeekprofileid/{peekprofile_id}/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool restorepeekbypeekprofileid(string peekprofile_id, string profileid);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/removepeeksbyprofileidandscreennames/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool removepeeksbyprofileidandscreennames(string profileid, List<String> screennames);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/restorepeeksbyprofileidandscreennames/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool restorepeeksbyprofileidandscreennames(string profileid, List<String> screennames);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/updatepeekviewstatus/{profileid}/{targetprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool updatepeekviewstatus(string profileid, string targetprofileid);

        #endregion

        #region "block methods"

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getwhoiblockedcount/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        int getwhoiblockedcount(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getwhoiblocked/{profileid}/{page}/{numberperpage}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<MemberSearchViewModel> getwhoiblocked(string profileid, string page, string numberperpage);


        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getmutualblocks/{profileid}/{targetprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<MemberSearchViewModel> getmutualblocks(string profileid, string targetprofileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/checkblock/{profileid}/{targetprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool checkblock(string profileid, string targetprofileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/addblock/{profileid}/{targetprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool addblock(string profileid, string targetprofileid);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/removeblock/{profileid}/{blockprofile_id}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool removeblock(string profileid, string blockprofile_id);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/restoreblock/{profileid}/{blockprofile_id}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool restoreblock(string profileid, string blockprofile_id);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/removeblocksbyscreennames/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool removeblocksbyscreennames(string profileid, List<String> screennames);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/restoreblocksbyscreennames/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool restoreblocksbyscreennames(string profileid, List<String> screennames);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/updateblockreviewstatus/{profileid}/{targetprofileid}/{reviewerid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool updateblockreviewstatus(string profileid, string targetprofileid, string reviewerid);

        #endregion

        #region "Like methods"

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getwhoilikecount/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        int getwhoilikecount(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getwholikesmecount/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        int getwholikesmecount(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getwhoislikesmenewcount/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        int getwhoislikesmenewcount(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getwholikesmenew/{profileid}/{page}/{numberperpage}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<MemberSearchViewModel> getwholikesmenew(string profileid, string page, string numberperpage);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getwholikesme/{profileid}/{page}/{numberperpage}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<MemberSearchViewModel> getwholikesme(string profileid, string page, string numberperpage);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getwhoilike/{profileid}/{page}/{numberperpage}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<MemberSearchViewModel> getwhoilike(string profileid, string page, string numberperpage);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getmutuallikes/{profileid}/{targetprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<MemberSearchViewModel> getmutuallikes(string profileid, string targetprofileid);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/checklike/{profileid}/{targetprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool checklike(string profileid, string targetprofileid);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/addlike/{profileid}/{targetprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool addlike(string profileid, string targetprofileid);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/removelikebyprofileid/{profileid}/{likeprofile_id}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool removelikebyprofileid(string profileid, string likeprofile_id);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/removelikebylikeprofileid/{likeprofile_id}/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool removelikebylikeprofileid(string likeprofile_id, string profileid);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/restorelikebyprofileid/{profileid}/{likeprofile_id}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool restorelikebyprofileid(string profileid, string likeprofile_id);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/restorelikebylikeprofileid/{likeprofile_id}/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool restorelikebylikeprofileid(string likeprofile_id, string profileid);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/removelikesbyprofileidandscreennames/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool removelikesbyprofileidandscreennames(string profileid, List<String> screennames);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/restorelikesbyprofileidandscreennames/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool restorelikesbyprofileidandscreennames(string profileid, List<String> screennames);

        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/updatelikeviewstatus/{profileid}/{targetprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool updatelikeviewstatus(string profileid, string targetprofileid);


        #endregion

    }
}
