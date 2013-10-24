using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Anewluv.Domain.Data.ViewModels.Email;

namespace Anewluv.Domain.Data.ViewModels
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