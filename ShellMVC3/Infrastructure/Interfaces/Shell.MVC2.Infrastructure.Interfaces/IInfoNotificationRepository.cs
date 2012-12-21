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
        
       EmailViewModel sendmembercreatedemail(RegisterModel model);       
    
       EmailViewModel sendmembercreatedopenidemail(RegisterModel model);
     
       EmailViewModel sendmemberpasswordchangedemail(LogonViewModel model);
       
       EmailViewModel sendmemberprofileactivatedemailbyprofileid(int profileid);
       
       EmailViewModel sendmemberactivationcoderecoveredemailbyprofileid(int profileid);
      
       EmailViewModel sendmemberemailmemssagereceivedemailbyprofileid(int recipientprofileid, int senderprofileid);
  
       EmailViewModel sendmemberpeekreceivedemailbyprofileid(int recipientprofileid, int senderprofileid);

       EmailViewModel sendmemberlikereceivedemailbyprofileid(int recipientprofileid, int senderprofileid);
  
       EmailViewModel sendmemberinterestreceivedemailbyprofileid(int recipientprofileid, int senderprofileid);
 
       EmailViewModel sendmemberchatrequestreceivedemailbyprofileid(int recipientprofileid, int senderprofileid);
     
       EmailViewModel sendmemberofflinechatmessagereceivedemailbyprofileid(int recipientprofileid, int senderprofileid);
 
       EmailViewModel sendmemberphotorejectedemailbyprofileid(int profileid, int adminprofileid, photorejectionreasonEnum reason);
      
       EmailViewModel sendmemberphotouploadedemailbyprofileid(int profileid);
    
       EmailViewModel sendadminspamblockedemailbyprofileid(int blockedprofileid);
 
       EmailViewModel sendemailmatchesemailbyprofileid(int profileid);
  
       EmailViewModel sendadminmemberspamblockedemailbyprofileid(int spamblockedprofileid, string reason, string blockedby);
      
       EmailViewModel sendadminmemberblockedemailbyprofileid(int blockedprofileid, int blockerprofileid);
       //There is no message for Members to know when photos are approved btw
       
       EmailViewModel sendadminmemberphotoapprovedemailbyprofileid(int approvedprofileid, int adminprofileid);
 
       EmailModel getemailbytemplateid(templateenum template);
       
       

    }
}
