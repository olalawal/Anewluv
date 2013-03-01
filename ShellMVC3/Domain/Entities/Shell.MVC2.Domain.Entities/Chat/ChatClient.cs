using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv.Chat
{
    public class ChatClient
    {
        [Key]      
        public string Id { get; set; }
        public string User_id { get; set; }
        public ChatUser User { get; set; }
        public string UserAgent { get; set; }       
        public DateTimeOffset LastActivity { get; set; }
    }
}