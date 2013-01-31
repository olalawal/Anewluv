using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels ;
using System.Web;



namespace Shell.MVC2.Interfaces
{
    public interface ISearchEditRepository
    {


         searchsetting getsearchsetting(int profileid, string searchname, int? searchrank);
       
         List<searchsetting> getsearchsettings(int profileid);
       
         SearchSettingsViewModel getsearchsettingsviewmodel(int profileid, string searchname, int? searchrank) ;  

         BasicSearchSettingsModel getbasicsearchsettings(int searchid)     ;     

         AppearanceSearchSettingsModel getappearancesearchsettings(int searchid);
           
         CharacterSearchSettingsModel getcharactersearchsettings(int searchid) ;     

         LifeStyleSearchSettingsModel getlifestylesearchsettings(int searchid);

         AnewluvMessages editbasicsearchsettings(BasicSearchSettingsModel newmodel, int searchid) ;   

         AnewluvMessages editappearancesettings(AppearanceSearchSettingsModel newmodel, int searchid);
  
         AnewluvMessages editlifestylesettings(LifeStyleSearchSettingsModel newmodel, int searchid);

         AnewluvMessages editcharactersettings(CharacterSearchSettingsModel newmodel, int searchid);
       
      

    
    }
}
