using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels ;
using System.Web;



namespace Shell.MVC2.Interfaces
{
    public interface IMemberEditRepository
    {

        bool updatemembervisibilitysettings(visiblitysetting model); 
        // constructor
         BasicSettingsModel getbasicsettingsmodel(int intprofileid)  ;      
        
        //The actual values will bind to viewmodel I think
         AppearanceSettingsModel getappearancesettingsmodel(int intprofileid);        
        //populate the enities
         LifeStyleSettingsModel getlifestylesettingsmodel(int intprofileid);
       
        //Using a contstructor populate the current values I suppose
        //The actual values will bind to viewmodel I think
         CharacterSettingsModel getcharactersettingsmodel(int intprofileid)  ;

         AnewluvMessages membereditbasicsettings(BasicSettingsModel newmodel, int profileid)  ; 
     
         AnewluvMessages membereditappearancesettings(AppearanceSettingsModel newmodel, int profileid);
     
         AnewluvMessages membereditlifestylesettings(LifeStyleSettingsModel newmodel, int profileid);

         AnewluvMessages membereditcharactersettings(CharacterSettingsModel newmodel, int profileid);
       
    
    }
}
