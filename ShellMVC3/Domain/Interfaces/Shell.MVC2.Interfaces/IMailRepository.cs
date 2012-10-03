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

         IQueryable<mailmessagemodel> ReplyEmail1(int? id, int mailboxMsgFldId)  ;

         IQueryable<mailmessagemodel> ReplyEmail(int? id)  ;    
  
         int GetmailboxmessagefoldersID(int mailboxMsgId )     ;
   
         int GetmailboxfolderID(string mailboxFolderTypeName, int profileid)    ;
   
         mailboxmessagefolder NewmailboxmessagefolderObject(string mailboxFolderTypeName, int profileid) ;

         void Add(mailboxmessage mailboxmessage);

         string getUserID(string User);
        
         int getAllMailCountbyfolderid(int folderid, string profile_id);

         int GetNewMailCountbyfolderid(int folderid, string profile_id)    ;    

        //TO DO find a way to use ENUM for these names 
         List<mailmodel> GetAllMailbydefaultmailboxfoldertypeMail(string foldertypename, string profile_id);

         List<mailmodel> GetAllMailbyfolder(int folderid, string profile_id) ;      


        //TO DO read out the description feild from enum using sample code
         List<mailmodel> GetMailMsgThreadByUserID(int uniqueId, string profile_id);

 
     
    }
}
