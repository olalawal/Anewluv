using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Shell.MVC2.Domain.Entities.Anewluv.Chat.ViewModels
{
    public class MessageViewModel
    {
       

        public string Id { get; set; }
        public string Content { get; set; }
        public DateTimeOffset When { get; set; }
        public UserViewModel User { get; set; }
    }
}