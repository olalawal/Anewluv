using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Dating.Server.Data;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;
using Shell.MVC2.Models;

namespace Shell.MVC2.Models
{
 

    
        public class AdminViewModel
        {
            public AdminViewModel()
            {
                AdminEmailModel = new EmailModel();
                UserProfileDatas = new List<profiledata>();
                LastActivity = DateTime.Now;
            }

            public List<profiledata> UserProfileDatas { get;  set; }
            public EmailModel AdminEmailModel { get;  set; }           
            public DateTime LastActivity { get;  set; }
        }
    
}