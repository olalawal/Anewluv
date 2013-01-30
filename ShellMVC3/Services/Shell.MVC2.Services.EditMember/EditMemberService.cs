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


namespace Shell.MVC2.Services.EditMember
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class EditMemberService : IEditMemberService  
    {


        private IEditMemberRepository _editmemberrepository;
       // private string _apikey;

        public EditMemberService(IEditMemberRepository editmemberrepository)
            {
                _editmemberrepository = editmemberrepository;
              //  _apikey  = HttpContext.Current.Request.QueryString["apikey"];
             //   throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);
               
            }

     

       public bool updateprofilevisibilitysettings(visiblitysetting model)
        {
            return _editmemberrepository.updatemembervisibilitysettings(model);
        }
        // constructor
        public BasicSettingsModel getbasicsettingsmodel(string profileid)
        {
            return _editmemberrepository.getbasicsettingsmodel(Convert.ToInt32(profileid));
        }

        //The actual values will bind to viewmodel I think
        public AppearanceSettingsModel getappearancesettingsmodel(string profileid)
        {
            return _editmemberrepository.getappearancesettingsmodel(Convert.ToInt32(profileid));
        }

        //populate the enities
        public LifeStyleSettingsModel getlifestylesettingsmodel(string profileid)
        {
            return _editmemberrepository.getlifestylesettingsmodel(Convert.ToInt32(profileid));
        }

           //The actual values will bind to viewmodel I think
        public CharacterSettingsModel getcharactersettingsmodel(string profileid)
        {
            return _editmemberrepository.getcharactersettingsmodel(Convert.ToInt32(profileid));
        }


        public AnewluvMessages editmemberbasicsettings(BasicSettingsModel newmodel, string profileid)
        {
            return _editmemberrepository.editmemberbasicsettings(newmodel, Convert.ToInt32(profileid));
        }

        public AnewluvMessages editmemberappearancesettings(AppearanceSettingsModel newmodel, string profileid)
        {
            return _editmemberrepository.editmemberappearancesettings(newmodel, Convert.ToInt32(profileid));
        }


        public AnewluvMessages editmemberlifestylesettings(LifeStyleSettingsModel newmodel, string profileid)
        {
            return _editmemberrepository.editmemberlifestylesettings(newmodel, Convert.ToInt32(profileid));

        }

        public AnewluvMessages editmembercharactersettings(CharacterSettingsModel newmodel, string profileid)
        {
            return _editmemberrepository.editmembercharactersettings(newmodel, Convert.ToInt32(profileid));
        }


    }
}
