using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shell.MVC2.Models.Chat;

namespace Shell.MVC2.ViewModels.Chat
{
    public class MessageViewModel
    {
        public MessageViewModel(ChatMessage message)
        {
            Id = message.Id;
            Content = message.Content;
            User = new UserViewModel(message.User);
            When = message.When;
        }

        public string Id { get; set; }
        public string Content { get; set; }
        public DateTimeOffset When { get; set; }
        public UserViewModel User { get; set; }
    }
}