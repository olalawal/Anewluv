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



        [WebGet]
        [OperationContract]
        int getwhoiaminterestedincount(int profileid);
        [WebGet]
        [OperationContract]
        int getwhoisinterestedinmecount(int profileid);
        [WebGet]
        [OperationContract]
        int getwhoisinterestedinmenewcount(int profileid);
        [WebGet]
        [OperationContract]
        List<MemberSearchViewModel> getinterests(int profileid, int? Page, int? NumberPerPage);
        [WebGet]
        [OperationContract]
        List<MemberSearchViewModel> getwhoisinterestedinme(int profileid, int? Page, int? NumberPerPage);
        [WebGet]
        [OperationContract]
        List<MemberSearchViewModel> getwhoisinterestedinmenew(int profileid, int? Page, int? NumberPerPage);
        [WebGet]
        [OperationContract]
        List<MemberSearchViewModel> getmutualinterests(int profileid, int targetprofileid);
        [WebGet]
        [OperationContract]
        bool checkinterest(int profileid, int targetprofileid);
        [WebInvoke ]
        [OperationContract]
        bool addinterest(int profileid, int targetprofileid);
        [WebInvoke]
        [OperationContract]
        bool removeinterestbyprofileid(int profileid, int interestprofile_id);
        [WebInvoke]
        [OperationContract]
        bool removeinterestbyinterestprofileid(int interestprofile_id, int profileid);
        [WebInvoke]
        [OperationContract]
        bool restoreinterestbyprofileid(int profileid, int interestprofile_id);
        [WebInvoke]
        [OperationContract]
        bool restoreinterestbyinterestprofileid(int interestprofile_id, int profileid);
        [WebInvoke]
        [OperationContract]
        bool removeinterestsbyprofileidandscreennames(int profileid, List<String> screennames);
        [WebInvoke]
        [OperationContract]
        bool restoreinterestsbyprofileidandscreennames(int profileid, List<String> screennames);
        [WebInvoke]
        [OperationContract]
        bool updateinterestviewstatus(int profileid, int targetprofileid);



        #endregion

        #region "peek methods"

        [WebGet]
        [OperationContract]
        int getwhoipeekedatcount(int profileid);

        [WebGet]
        [OperationContract]
        int getwhopeekedatmecount(int profileid);

        [WebGet]
        [OperationContract]
        int getwhopeekedatmenewcount(int profileid);

        [WebGet]
        [OperationContract]
        List<MemberSearchViewModel> getwhopeekedatme(int profileid, int? Page, int? NumberPerPage);

        [WebGet]
        [OperationContract]
        List<MemberSearchViewModel> getwhopeekedatmenew(int profileid, int? Page, int? NumberPerPage);

        [WebGet]
        [OperationContract]
        List<MemberSearchViewModel> getwhoipeekedat(int profileid, int? Page, int? NumberPerPage);

        [WebGet]
        [OperationContract]
        List<MemberSearchViewModel> getmutualpeeks(int profileid, int targetprofileid);

        [WebGet]
        [OperationContract]
        bool checkpeek(int profileid, int targetprofileid);

        [WebInvoke ]
        [OperationContract]
        bool addpeek(int profileid, int targetprofileid);

        [WebInvoke ]
        [OperationContract]
        bool removepeekbyprofileid(int profileid, int peekprofile_id);

        [WebInvoke]
        [OperationContract]
        bool removepeekbypeekprofileid(int peekprofile_id, int profileid);

        [WebInvoke]
        [OperationContract]
        bool restorepeekbyprofileid(int profileid, int peekprofile_id);

        [WebInvoke]
        [OperationContract]
        bool restorepeekbypeekprofileid(int peekprofile_id, int profileid);

        [WebInvoke]
        [OperationContract]
        bool removepeeksbyprofileidandscreennames(int profileid, List<String> screennames);

        [WebInvoke]
        [OperationContract]
        bool restorepeeksbyprofileidandscreennames(int profileid, List<String> screennames);

        [WebInvoke]
        [OperationContract]
        bool updatepeekviewstatus(int profileid, int targetprofileid);

        #endregion

        #region "block methods"

        [WebGet]
        [OperationContract]
        int getwhoiblockedcount(int profileid);

        [WebGet]
        [OperationContract]
        List<MemberSearchViewModel> getwhoiblocked(int profileid, int? Page, int? NumberPerPage);


        [WebGet]
        [OperationContract]
        List<MemberSearchViewModel> getmutualblocks(int profileid, int targetprofileid);

        [WebGet]
        [OperationContract]
        bool checkblock(int profileid, int targetprofileid);
        [WebInvoke]
        [OperationContract]
        bool addblock(int profileid, int targetprofileid);
        [WebInvoke]
        [OperationContract]
        bool removeblock(int profileid, int blockprofile_id);
        [WebInvoke]
        [OperationContract]
        bool restoreblock(int profileid, int blockprofile_id);
        [WebInvoke]
        [OperationContract]
        bool removeblocksbyscreennames(int profileid, List<String> screennames);
        [WebInvoke]
        [OperationContract]
        bool restoreblocksbyscreennames(int profileid, List<String> screennames);
        [WebInvoke]
        [OperationContract]
        bool updateblockreviewstatus(int profileid, int targetprofileid, int reviewerid);

        #endregion

        #region "Like methods"
        [WebGet]
        [OperationContract]
        int getwhoilikecount(int profileid);
        [WebGet]
        [OperationContract]
        int getwholikesmecount(int profileid);
        [WebGet]
        [OperationContract]
        int getwhoislikesmenewcount(int profileid);
        [WebGet]
        [OperationContract]
        List<MemberSearchViewModel> getwholikesmenew(int profileid, int? Page, int? NumberPerPage);
        [WebGet]
        [OperationContract]
        List<MemberSearchViewModel> getwholikesme(int profileid, int? Page, int? NumberPerPage);
        [WebGet]
        [OperationContract]
        List<MemberSearchViewModel> getwhoilike(int profileid, int? Page, int? NumberPerPage);
        [WebGet]
        [OperationContract]
        List<MemberSearchViewModel> getmutuallikes(int profileid, int targetprofileid);

        bool checklike(int profileid, int targetprofileid);
        [WebInvoke]
        [OperationContract]
        bool addlike(int profileid, int targetprofileid);
        [WebInvoke]
        [OperationContract]
        bool removelikebyprofileid(int profileid, int likeprofile_id);
        [WebInvoke]
        [OperationContract]
        bool removelikebylikeprofileid(int likeprofile_id, int profileid);
        [WebInvoke]
        [OperationContract]
        bool restorelikebyprofileid(int profileid, int likeprofile_id);
        [WebInvoke]
        [OperationContract]
        bool restorelikebylikeprofileid(int likeprofile_id, int profileid);
        [WebInvoke]
        [OperationContract]
        bool removelikesbyprofileidandscreennames(int profileid, List<String> screennames);
        [WebInvoke]
        [OperationContract]
        bool restorelikesbyprofileidandscreennames(int profileid, List<String> screennames);
        [WebInvoke]
        [OperationContract]
        bool updatelikeviewstatus(int profileid, int targetprofileid);


        #endregion

    }
}
