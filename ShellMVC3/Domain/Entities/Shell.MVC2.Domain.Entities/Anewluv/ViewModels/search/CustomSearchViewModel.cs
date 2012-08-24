using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using Dating.Server.Data;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;

//using RiaServicesContrib.Mvc;


using System.Collections;


//using RiaServicesContrib.Mvc.Services;

using Omu.Awesome.Core;

//trying MVC pagination
using MvcContrib.Pagination;
using MvcContrib.UI.Grid;




namespace Shell.MVC2.Models
{

    //constants for page indexes
    
        

    public class CustomSearchViewModel
    {
        //basic settings
  
        public SearchModelBasicSettings BasicSearchSettings { get; set; }     
        public SearchModelAppearanceSettings AppearanceSearchSettings { get; set; }      
        public  SearchModelLifeStyleSettings  LifeStyleSearchSettings { get; set; }      
        public SearchModelCharacterSettings   CharacterSearchSettings { get; set; }
        //index of what page we are looking at i.e we want to split up this model into diff partial views
        public int ViewIndex { get; set; }
        public bool PostalCodeStatus { get; set; }
        public bool IsFullEditing { get; set; }
        public  List<string> CurrentErrors = new List<string>();
    }
}
