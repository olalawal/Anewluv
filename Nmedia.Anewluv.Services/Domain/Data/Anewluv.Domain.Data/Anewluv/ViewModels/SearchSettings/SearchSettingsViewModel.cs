using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Collections;
using System.Runtime.Serialization;


namespace Anewluv.Domain.Data.ViewModels
{

    //constants for page indexes
    
        
    //populates and updates a search page similar to edit
    [DataContract]
    public class SearchSettingsViewModel
    {
        //basic settings  
        public BasicSearchSettingsModel basicsearchsettings { get; set; }     
        public AppearanceSearchSettingsModel appearancesearchsettings { get; set; }      
        public LifeStyleSearchSettingsModel lifestylesearchsettings { get; set; }      
        public CharacterSearchSettingsModel   charactersearchsettings { get; set; }
        //index of what page we are looking at i.e we want to split up this model into diff partial views
        [DataMember]
        public int profileid { get; set; }
        [DataMember]
        public int searchid { get; set; }
        [DataMember]
        public string searchname { get; set; }
        [DataMember]
        public int searchrank { get; set; }
        [DataMember]
        public int viewindex { get; set; }
        public bool postalcodestatus { get; set; }
        [DataMember]
        public bool isfullediting { get; set; }
        public  List<string> currenterrors = new List<string>();
    }
}
