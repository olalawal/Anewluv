using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;

using Shell.MVC2.Interfaces;
using Shell.MVC2.Services.Contracts;
using System.Web;
using System.ServiceModel.Activation;

namespace Shell.MVC2.Services.Dating
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersMapperService" in both code and config file together.

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]  
    public class MembersMapperService : IMembersMapperService
    {
        private IMembersMapperRepository _mapmembermapperrepo;
       

        public MembersMapperService(IMembersMapperRepository mapmembermapperrepo)
            {
                _mapmembermapperrepo = mapmembermapperrepo;
    
               
            }


        // constructor
        public MemberSearchViewModel getmembersearchviewmodel(string profileid)
        {
            return _mapmembermapperrepo.getmembersearchviewmodel(Convert.ToInt32(profileid));
        }
        public List<MemberSearchViewModel> getmembersearchviewmodels(List<int> profileIds)
        {
            return _mapmembermapperrepo.getmembersearchviewmodels(profileIds);
        }
        public ProfileBrowseModel getprofilebrowsemodel(string viewerprofileId, string profileid)
        {
            return _mapmembermapperrepo.getprofilebrowsemodel(Convert.ToInt32(viewerprofileId), (Convert.ToInt32(profileid)));
        }
        //returns a list of profile browsemodles for a given user
        public List<ProfileBrowseModel> getprofilebrowsemodels(string viewerprofileId, List<int> profileIds)
        {
            return _mapmembermapperrepo.getprofilebrowsemodels(Convert.ToInt32(viewerprofileId), profileIds);
        }
        // constructor
        //4-12-2012 added screen name
        //4-18-2012 added search settings
        public ProfileCriteriaModel getprofilecriteriamodel(string profileid)
        {
            return _mapmembermapperrepo.getprofilecriteriamodel(Convert.ToInt32(profileid));          

        }
        //use an overload to return values if a user is not logged in i.e
        //no current profiledata exists to retrive
        public ProfileCriteriaModel getprofilecriteriamodel()
        {

            return _mapmembermapperrepo.getprofilecriteriamodel();
        }
        //gets search settings
        //TO DO this function is just setting temp values for now
        //9 -21- 2011 added code to get age at least from search settings , more values to follow
        public MembersViewModel getdefaultquicksearchsettingsmembers(MembersViewModel Model)
        {
            return _mapmembermapperrepo.getdefaultquicksearchsettingsmembers(Model);
        }
        //populate search settings for guests 
        public MembersViewModel getdefaultsearchsettingsguest(MembersViewModel Model)
        {
            return _mapmembermapperrepo.getdefaultsearchsettingsguest(Model);
        }

        //registration model update and mapping
        public RegisterModel getregistermodel(MembersViewModel membersmodel)
        {
            return _mapmembermapperrepo.getregistermodel(membersmodel);          
        }
        public RegisterModel getregistermodelopenid(MembersViewModel membersmodel)
        {
            return _mapmembermapperrepo.getregistermodelopenid(membersmodel);
        }
        public RegisterModel getregistermodeltest()
        {
            return _mapmembermapperrepo.getregistermodeltest();
        }

        //TOD modifiy client to not bind from this model but load values asycnh
        //other member viewmodl methods
        public MembersViewModel updatememberdata(MembersViewModel model)
        {
            return _mapmembermapperrepo.updatememberdata(model);
        }
        public MembersViewModel updatememberdatabyprofileid(string profileid)
        {
            return _mapmembermapperrepo.updatememberdatabyprofileid(Convert.ToInt32(profileid));
        }
        public bool updateguestdata(MembersViewModel model)
        {
            return _mapmembermapperrepo.updateguestdata(model);
        }
        public bool removeguestdata(string sessionid)
        {
            return _mapmembermapperrepo.removeguestdata(sessionid);
        }
        //cacheing of search stuff
        public MembersViewModel getguestdata(string sessionid)
        {
            return _mapmembermapperrepo.getguestdata(sessionid);
        }
        public MembersViewModel getmemberdata(string profileid)
        {
            return _mapmembermapperrepo.getmemberdata(Convert.ToInt32(profileid));
        }

        //functions not exposed via WCF or otherwise
        public MembersViewModel mapmember(string profileid)
        {
            return _mapmembermapperrepo.mapmember(Convert.ToInt32(profileid));
           

        }

        public MembersViewModel mapguest()
        {
            return _mapmembermapperrepo.mapguest();
        }

    }
}
