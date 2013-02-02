using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv.Chat
{
    public class ChatMessage
    {
        [Key]
        public int Key { get; set; }
        public string Content { get; set; }
        public string Id { get; set; }
        public virtual ChatRoom Room { get; set; }
        public virtual ChatUser User { get; set; }
        public DateTimeOffset When { get; set; }
    }
}