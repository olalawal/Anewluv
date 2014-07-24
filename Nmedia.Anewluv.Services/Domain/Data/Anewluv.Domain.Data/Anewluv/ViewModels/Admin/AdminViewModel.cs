using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Anewluv.Domain.Data.ViewModels.Email;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data.ViewModels
{
 

    
      [DataContract] public class AdminViewModel
        {
            public AdminViewModel()
            {
              //  AdminEmailModel = new EmailViewModel();
                UserProfileDatas = new List<profiledata>();
                LastActivity = DateTime.Now;
            }

           [DataMember]   public List<profiledata> UserProfileDatas { get;  set; }
          // [DataMember]
         //  public EmailViewModel AdminEmailModel { get; set; }
           [DataMember]
           public DateTime LastActivity { get; set; }
        }
    
}