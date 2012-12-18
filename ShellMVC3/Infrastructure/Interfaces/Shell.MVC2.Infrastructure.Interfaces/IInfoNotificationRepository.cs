using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Infrastructure.Entities.NotificationModel;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels.Email;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;

namespace Shell.MVC2.Infrastructure.Interfaces
{
   public  interface IInfoNotificationRepository
    {

       EmailModel senderrormessage(CustomErrorLog error, addresstypeenum addresstype);

       EmailViewModel sendcontactusemail(ContactUsModel model);
        
       EmailViewModel getmembercreatedemail(RegisterModel model);       
    
       EmailViewModel getmembercreatedopenidemail(RegisterModel model);
     
       EmailViewModel getmemberpasswordchangedemail(LogonViewModel model);
       
       EmailViewModel getmemberprofileactivatedemailbyprofileid(int profileid);
       
       EmailViewModel getmemberactivationcoderecoveredemailbyprofileid(int profileid);
      
       EmailViewModel getmemberemailmemssagereceivedemailbyprofileid(int recipientprofileid, int senderprofileid);
  
       EmailViewModel getmemberpeekreceivedemailbyprofileid(int recipientprofileid, int senderprofileid);

       EmailViewModel getmemberlikereceivedemailbyprofileid(int recipientprofileid, int senderprofileid);
  
       EmailViewModel getmemberinterestreceivedemailbyprofileid(int recipientprofileid, int senderprofileid);
 
       EmailViewModel getmemberchatrequestreceivedemailbyprofileid(int recipientprofileid, int senderprofileid);
     
       EmailViewModel getmemberofflinechatmessagereceivedemailbyprofileid(int recipientprofileid, int senderprofileid);
 
       EmailViewModel getmemberphotorejectedemailbyprofileid(int profileid, int adminprofileid, photorejectionreasonEnum reason);
      
       EmailViewModel getmemberphotouploadedemailbyprofileid(int profileid);
    
       EmailViewModel getadminspamblockedemailbyprofileid(int blockedprofileid);
 
       EmailViewModel getemailmatchesemailbyprofileid(int profileid);
  
       EmailViewModel getadminmemberspamblockedemailbyprofileid(int spamblockedprofileid, string reason, string blockedby);
      
       EmailViewModel getadminmemberblockedemailbyprofileid(int blockedprofileid, int blockerprofileid);
       //There is no message for Members to know when photos are approved btw
       
       EmailViewModel getadminmemberphotoapprovedemailbyprofileid(int approvedprofileid, int adminprofileid);
 
       EmailModel getemailbytemplateid(templateenum template);
       
       

    }
}
