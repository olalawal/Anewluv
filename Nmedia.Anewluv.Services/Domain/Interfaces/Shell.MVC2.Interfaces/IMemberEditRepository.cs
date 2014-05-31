using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using Anewluv.Domain.Data;
using Anewluv.Domain.Data.ViewModels;



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
