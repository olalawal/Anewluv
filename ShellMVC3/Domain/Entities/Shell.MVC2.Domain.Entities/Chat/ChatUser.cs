using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
//using Shell.MVC2.Infastructure.Chat;

namespace Shell.MVC2.Domain.Entities.Anewluv.Chat
{
  
    [DataContract]
        public class ChatUser
        {
            [Key]           
            public string Id { get; set; }          
            public string Name { get; set; }
            //added gender for the hell of it altought we are nop populating it right now
            public string Gender { get; set; }
            // MD5 email hash for gravatar
            public string Hash { get; set; }
            public string Salt { get; set; }
            public string HashedPassword { get; set; }
            public DateTime LastActivity { get; set; }
            public DateTime? LastNudged { get; set; }
            public int Status { get; set; }
            [StringLength(200)]
            public string Note { get; set; }
            [StringLength(200)]
            public string AfkNote { get; set; }
            public bool IsAfk { get; set; }
            [StringLength(2)]
            public string Flag { get; set; }
            public string ScreenName { get; set; }
            public int profileid { get; set; }
            public string Email { get; set; }
            public bool? HasMobileAccess { get; set; }
            // List of clients that are currently connected for this user
            public virtual ICollection<ChatClient> ConnectedClients { get; set; }
            public virtual ICollection<ChatMessage> ChatMessages { get; set; }
            //public virtual ICollection<ChatMessage> RecivedMessages { get; set; }
            public virtual ICollection<ChatRoom> OwnedRooms { get; set; }
            public virtual ICollection<ChatRoom> Rooms { get; set; }

            // Private rooms this user is allowed to go into
            public virtual ICollection<ChatRoom> AllowedRooms { get; set; }

            //TO Do this use database to decalare
            //DO it in the serice
            public ChatUser()
            {
              //  ConnectedClients = new SafeCollection<ChatClient>();
              //  OwnedRooms = new SafeCollection<ChatRoom>();
              //  Rooms = new SafeCollection<ChatRoom>();
               // AllowedRooms = new SafeCollection<ChatRoom>();
            }
        }
   
}