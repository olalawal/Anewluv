using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using Shell.MVC2.Domain.Entities.Anewluv.Chat;
using Shell.MVC2.Domain.Entities.Anewluv.Chat.ViewModels ;

using Shell.MVC2.Interfaces;
using Shell.MVC2.Infrastructure;

namespace Shell.MVC2.Data
{
   public  class ChatRepository : MemberRepositoryBase  
    {

       
       
        private  AnewluvContext db; // = new AnewluvContext();
       //private  PostalData2Entities postaldb; //= new PostalData2Entities();
   
       
      

       public ChatRepository(AnewluvContext datingcontext) 
           :base(datingcontext)
       {
          
       }

        public UserViewModel getuserviewmodel(ChatUser user)
        {
          
            
            Name = user.Name;
            ScreenName = user.ScreenName;
            Gender = user.Gender;
            Hash = user.Hash;
            Active = user.Status == (int)UserStatus.Active;
            Status = ((UserStatus)user.Status).ToString();
            Note = user.Note;
            AfkNote = user.AfkNote;
            IsAfk = user.IsAfk;
            Flag = user.Flag;
            Country = ChatService.GetCountry(user.Flag);
            LastActivity = user.LastActivity;
        }


           
      public  MessageViewModel  messageviewmodel (ChatMessage message)
        {
            Id = message.Id;
            Content = message.Content;
            User = new UserViewModel(message.User);
            When = message.When;
        }

     
    }
}
