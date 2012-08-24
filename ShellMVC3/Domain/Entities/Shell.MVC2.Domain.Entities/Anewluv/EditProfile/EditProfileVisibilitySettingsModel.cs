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
           

    public class ProfileVisibilitySettingsModel
    {
        //basic settings
        public  Boolean? MailPeeks { get; set; }
        public Boolean? MailInterests { get; set; }
        public Boolean? MailLikes { get; set; }
        public Boolean? MailNews { get; set; }
        public Boolean MailChatRequest { get; set; }
        public Boolean? ProfileVisibility { get; set; }
        public int? CountryId { get; set; }
        public int? GenderId { get; set; }
        public Boolean? StealthPeeks { get; set; }
        public int? AgeMin { get; set; }
        public int? AgeMax { get; set; }
        public DateTime? LastUpdateTime { get; set; }
        public Boolean? ChatVisibilityToLikes { get; set; }
        public Boolean? ChatVisibilityToInterests { get; set; }
        public Boolean?ChatVisibilityToMatches { get; set; }
        public Boolean? ChatVisibilityToPeeks { get; set; }
        public Boolean? ChatVisibilityToSearch { get; set; }
        public Boolean? SaveOfflineChatMessages { get; set; }
      
    }
}
