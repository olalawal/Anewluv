using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels.Email;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
 

    
        public class AdminViewModel
        {
            public AdminViewModel()
            {
                AdminEmailModel = new EmailViewModel();
                UserProfileDatas = new List<profiledata>();
                LastActivity = DateTime.Now;
            }

            public List<profiledata> UserProfileDatas { get;  set; }
            public EmailViewModel AdminEmailModel { get;  set; }           
            public DateTime LastActivity { get;  set; }
        }
    
}