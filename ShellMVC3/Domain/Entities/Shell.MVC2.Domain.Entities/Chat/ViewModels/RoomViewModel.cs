using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shell.MVC2.Domain.Entities.Anewluv.Chat;

namespace Shell.MVC2.Domain.Entities.Anewluv.Chat.ViewModels
{
    public class RoomViewModel
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public bool Private { get; set; }
        public string Type { get; set; }
        public bool ChatRequestRejected { get; set; }
        public bool ChatRequestAccepted { get; set; }
        public string Topic { get; set; }
        public string TopicStarter { get; set; }
        public string TopicScreenName { get; set; }
        public string TopicStarterScreenName { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }
        public IEnumerable<string> Owners { get; set; }
        public IEnumerable<MessageViewModel> RecentMessages { get; set; }
    }
}