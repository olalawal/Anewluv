using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shell.MVC2.Domain.Entities.Anewluv.Chat
{
    public class ClientMessage
    {

        public string Id { get; set; }
        public string Content { get; set; }
        public string Room { get; set; }
        public DateTimeOffset LastActivity { get; set; }
    
    }
}