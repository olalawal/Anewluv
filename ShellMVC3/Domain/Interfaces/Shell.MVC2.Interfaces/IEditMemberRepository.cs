using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels ;
using System.Web;



namespace Shell.MVC2.Interfaces
{
    public interface IEditMemberRepository
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

         AnewluvMessages editmemberbasicsettings(BasicSettingsModel newmodel, int profileid)  ; 
     
         AnewluvMessages editmemberappearancesettings(AppearanceSettingsModel newmodel, int profileid);
     
         AnewluvMessages editmemberlifestylesettings(LifeStyleSettingsModel newmodel, int profileid);

         AnewluvMessages editmembercharactersettings(CharacterSettingsModel newmodel, int profileid);
       
    
    }
}
