using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Anewluv.Domain.Data.Chat.ViewModels
{
    public class MessageViewModel
    {
        public MessageViewModel(ChatMessage message)
        {
            Id = message.Id ;
            Content = message.Content;
            User = new UserViewModel(message.User);
            When = message.When;
            //HtmlEncoded = message.HtmlEncoded;
        }

        public string Id { get; set; }
        public string Content { get; set; }
        public DateTimeOffset When { get; set; }
        public UserViewModel User { get; set; }
    }
}