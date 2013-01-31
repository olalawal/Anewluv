using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Shell.MVC2.Services.Contracts;
using Shell.MVC2.Interfaces;



using System.Web;
using System.Net;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using System.ServiceModel.Activation;


namespace Shell.MVC2.Services.Edit
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "searchsService" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class EditsearchService : ISearchEditService 
    {


        private ISearchEditRepository _editsearchrepository;
        // private string _apikey;

        public EditsearchService(ISearchEditRepository editsearchrepository)
        {
            _editsearchrepository = editsearchrepository;
            //  _apikey  = HttpContext.Current.Request.QueryString["apikey"];
            //   throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);

        }


        public searchsetting getsearchsetting(string profileid, string searchname, string searchrank)
        {
            return _editsearchrepository.getsearchsetting(Convert.ToInt32(profileid), searchname, Convert.ToInt32(searchrank));
        }


        public List<searchsetting> getsearchsettings(string profileid)
        {
            return _editsearchrepository.getsearchsettings(Convert.ToInt32(profileid));
        }

        public SearchSettingsViewModel getsearchsettingsviewmodel(string profileid, string searchname, string searchrank)
        {
         return _editsearchrepository.getsearchsettingsviewmodel((Convert.ToInt32(profileid)), searchname, Convert.ToInt32(searchrank));
        }

        public BasicSearchSettingsModel getbasicsearchsettings(string searchid)
        {
            return _editsearchrepository.getbasicsearchsettings(Convert.ToInt32(searchid));
        }

        public AppearanceSearchSettingsModel getappearancesearchsettings(string searchid)
        {
            return _editsearchrepository.getappearancesearchsettings(Convert.ToInt32(searchid));
        }

        public CharacterSearchSettingsModel getcharactersearchsettings(string searchid)
        {
            return _editsearchrepository.getcharactersearchsettings(Convert.ToInt32(searchid));
        }

        public LifeStyleSearchSettingsModel getlifestylesearchsettings(string searchid)
        {
            return _editsearchrepository.getlifestylesearchsettings(Convert.ToInt32(searchid));
        }

        public AnewluvMessages editbasicsearchsettings(BasicSearchSettingsModel newmodel, string searchid)
        {
            return _editsearchrepository.editbasicsearchsettings(newmodel, Convert.ToInt32(searchid));
        }

        public AnewluvMessages editappearancesettings(AppearanceSearchSettingsModel newmodel, string searchid)
        {
            return _editsearchrepository.editappearancesettings(newmodel, Convert.ToInt32(searchid));
        }
                
        public AnewluvMessages editlifestylesettings(LifeStyleSearchSettingsModel newmodel, string searchid)
        {
            return _editsearchrepository.editlifestylesettings(newmodel, Convert.ToInt32(searchid));
        }

        public AnewluvMessages editcharactersettings(CharacterSearchSettingsModel newmodel, string searchid)
        {
            return _editsearchrepository.editcharactersettings(newmodel, Convert.ToInt32(searchid));
        }



    }

}
