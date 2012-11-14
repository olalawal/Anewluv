using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Infrastructure.Entities.NotificationModel;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels.Email;

namespace Shell.MVC2.Infrastructure.Interfaces
{
   public  interface IInfoNotificationRepository
    {


        //temporary method for use by designer to get the message information formated for them
         EmailViewModel getgenericemailviewmodel();        

         EmailViewModel getcontactusemailviewmodel(string from);       

         EmailViewModel getemailmatchesviewmodelbyprofileid(int profileid);

         EmailModel getemailviewmodelbytemplateid(templateenum template);
       

    }
}
