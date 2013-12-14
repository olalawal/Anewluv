using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Anewluv.Services.Contracts;
using Shell.MVC2.Interfaces;



using System.Web;
using System.Net;


using System.ServiceModel.Activation;
using Anewluv.Domain.Data;
using Anewluv.Domain.Data.ViewModels;


namespace Shell.MVC2.Services.Edit
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
   [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MemberEditService : IMemberEditService  
    {


        private IMemberEditRepository _membereditrepository;
       // private string _apikey;

        public MemberEditService(IMemberEditRepository membereditrepository)
            {
                _membereditrepository = membereditrepository;
              //  _apikey  = HttpContext.Current.Request.QueryString["apikey"];
             //   throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);
               
            }

     

       public bool updateprofilevisibilitysettings(visiblitysetting model)
        {
            return _membereditrepository.updatemembervisibilitysettings(model);
        }
        // constructor
        public BasicSettingsModel getbasicsettingsmodel(string profileid)
        {
            return _membereditrepository.getbasicsettingsmodel(Convert.ToInt32(profileid));
        }

        //The actual values will bind to viewmodel I think
        public AppearanceSettingsModel getappearancesettingsmodel(string profileid)
        {
            return _membereditrepository.getappearancesettingsmodel(Convert.ToInt32(profileid));
        }

        //populate the enities
        public LifeStyleSettingsModel getlifestylesettingsmodel(string profileid)
        {
            return _membereditrepository.getlifestylesettingsmodel(Convert.ToInt32(profileid));
        }

           //The actual values will bind to viewmodel I think
        public CharacterSettingsModel getcharactersettingsmodel(string profileid)
        {
            return _membereditrepository.getcharactersettingsmodel(Convert.ToInt32(profileid));
        }


        public AnewluvMessages membereditbasicsettings(BasicSettingsModel newmodel, string profileid)
        {
            return _membereditrepository.membereditbasicsettings(newmodel, Convert.ToInt32(profileid));
        }

        public AnewluvMessages membereditappearancesettings(AppearanceSettingsModel newmodel, string profileid)
        {
            return _membereditrepository.membereditappearancesettings(newmodel, Convert.ToInt32(profileid));
        }


        public AnewluvMessages membereditlifestylesettings(LifeStyleSettingsModel newmodel, string profileid)
        {
            return _membereditrepository.membereditlifestylesettings(newmodel, Convert.ToInt32(profileid));

        }

        public AnewluvMessages membereditcharactersettings(CharacterSettingsModel newmodel, string profileid)
        {
            return _membereditrepository.membereditcharactersettings(newmodel, Convert.ToInt32(profileid));
        }


    }
}
