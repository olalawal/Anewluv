using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

using System.Web.Security;





//using RiaServicesContrib.Mvc;


using System.Collections;


//using RiaServicesContrib.Mvc.Services;








namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{

    //constants for page indexes
    
        
    //populates and updates a search page similar to edit
  [Data]
    public class EditProfileViewModel
    {
        //basic settings
  
        public BasicSettingsViewModel BasicSearchSettings { get; set; }     
        public AppearanceSettingsViewModel AppearanceSearchSettings { get; set; }      
        public  LifeStyleSettingsViewModel  LifeStyleSearchSettings { get; set; }      
        public CharacterSettingsViewModel   CharacterSearchSettings { get; set; }
        //index of what page we are looking at i.e we want to split up this model into diff partial views
        public int ViewIndex { get; set; }
        public bool PostalCodeStatus { get; set; }
        public bool IsFullEditing { get; set; }
        public  List<string> CurrentErrors = new List<string>();
    }
}
