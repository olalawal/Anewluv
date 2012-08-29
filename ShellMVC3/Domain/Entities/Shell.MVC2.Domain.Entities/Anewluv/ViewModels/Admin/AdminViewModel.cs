using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
 

    
        public class AdminViewModel
        {
            public AdminViewModel()
            {
                AdminEmailModel = new MailModel();
                UserProfileDatas = new List<profiledata>();
                LastActivity = DateTime.Now;
            }

            public List<profiledata> UserProfileDatas { get;  set; }
            public MailModel AdminEmailModel { get;  set; }           
            public DateTime LastActivity { get;  set; }
        }
    
}