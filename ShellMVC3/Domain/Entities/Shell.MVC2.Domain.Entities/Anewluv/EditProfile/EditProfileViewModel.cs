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
    public static class Constants
    {

        public const int EditProfileStartPage = 0;
        public const int BasicSettingsPage1 = 1;
        public const int BasicSettingsPage2 = 2;
        public const int AppearanceSettingsPage1 = 3;
        public const int AppearanceSettingsPage2 = 4;
        public const int AppearanceSettingsPage3 = 5;
        public const int AppearanceSettingsPage4 = 6;
        public const int LifeStyleSettingsPage1 = 7;
        public const int LifeStyleSettingsPage2 = 8;
        public const int LifeStyleSettingsPage3 = 9;
        public const int LifeStyleSettingsPage4 = 10;
        public const int CharacterSettingsPage1 = 11;
        public const int CharacterSettingsPage2 = 12;
        public const int CharacterSettingsPage3 = 13;
        public const int CharacterSettingsPage4 = 14;
        public const int CharacterSettingsPage5 = 15;
        public const int CharacterSettingsPage6 = 16;


    }
        

    public class EditProfileSettingsViewModel
    {
        //basic settings
        public EditProfileBasicSettingsModel BasicProfileSettings { get; set; }
        public SearchModelBasicSettings BasicSearchSettings { get; set; }

        public EditProfileAppearanceSettingsModel AppearanceSettings { get; set; }
        public SearchModelAppearanceSettings AppearanceSearchSettings { get; set; }

        public  EditProfileLifeStyleSettingsModel  LifeStyleSettings { get; set; }
        public  SearchModelLifeStyleSettings  LifeStyleSearchSettings { get; set; }

        public EditProfileCharacterSettingsModel  CharacterSettings { get; set; }
        public SearchModelCharacterSettings   CharacterSearchSettings { get; set; }

        //index of what page we are looking at i.e we want to split up this model into diff partial views
        public int ViewIndex { get; set; }
        public bool IsFullEditing { get; set; }
        public  List<string> CurrentErrors = new List<string>();

       
    }
}
