using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;


namespace Shell.MVC2.Interfaces
{
     public interface  IMailRepository
    {

         List<mailmessageviewmodel> replyemail1(int? id, int mailboxMsgFldId)  ;

         List<mailmessageviewmodel> replyemail(int? id);    
  
         int getmailboxmessagefoldersid(int mailboxMsgId )     ;
   
         int getmailboxfolderid(string mailboxFolderTypeName, int profileid)    ;
   
         mailboxmessagefolder newmailboxmessagefolderobject(string mailboxFolderTypeName, int profileid) ;

         void add(mailboxmessage mailboxmessage);

         string getuserid(string User);
        
         int getallmailcountbyfolderid(int folderid, int profile_id);

         int getnewmailcountbyfolderid(int folderid, int profile_id)    ;    

        //TO DO find a way to use ENUM for these names 
         List<mailviewmodel> getallmailbydefaultmailboxfoldertypemail(string foldertypename, int profile_id);

         List<mailviewmodel> getallmailbyfolder(int folderid, int profile_id);      


        //TO DO read out the description feild from enum using sample code
         List<mailviewmodel> getmailmsgthreadbyuserid(int uniqueId, int profile_id);

 
     
    }
}
