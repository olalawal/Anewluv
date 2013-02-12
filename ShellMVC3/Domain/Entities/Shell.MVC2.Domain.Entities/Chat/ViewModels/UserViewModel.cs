using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shell.MVC2.Domain.Entities.Anewluv.Chat;


namespace Shell.MVC2.Domain.Entities.Anewluv.Chat.ViewModels
{
    public class UserViewModel
    {

        public UserViewModel(ChatUser user)
        {
            Name = user.Name;
            Hash = user.Hash;
            Active = user.Status == (int)userstatusEnum.Active;
            Status = ((userstatusEnum)user.Status).ToString();
            Note = user.Note;
            AfkNote = user.AfkNote;
            IsAfk = user.IsAfk;
            Flag = user.Flag;
            Country = ChatService.GetCountry(user.Flag);
            LastActivity = user.LastActivity;
            IsAdmin = user.IsAdmin;
        }

        public string Name { get; private set; }
        public string ScreenName { get; set; }
        public string Gender { get; private set; }
        public string Hash { get; private set; }
        public bool Active { get; private set; }
        public string Status { get; private set; }
        public string Note { get; private set; }
        public string AfkNote { get; private set; }
        public bool IsAfk { get; private set; }
        public string Flag { get; private set; }
        public string Country { get; private set; }
        public DateTime LastActivity { get; private set; }
    }
}