using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Collections;


namespace Anewluv.Domain.Data.ViewModels
{

    //constants for page indexes
    
        
    //populates and updates a search page similar to edit
    public class SearchSettingsViewModel
    {
        //basic settings  
        public BasicSearchSettingsModel basicsearchsettings { get; set; }     
        public AppearanceSearchSettingsModel appearancesearchsettings { get; set; }      
        public LifeStyleSearchSettingsModel lifestylesearchsettings { get; set; }      
        public CharacterSearchSettingsModel   charactersearchsettings { get; set; }
        //index of what page we are looking at i.e we want to split up this model into diff partial views
        public int viewindex { get; set; }
        public bool postalcodestatus { get; set; }
        public bool isfullediting { get; set; }
        public  List<string> currenterrors = new List<string>();
    }
}
