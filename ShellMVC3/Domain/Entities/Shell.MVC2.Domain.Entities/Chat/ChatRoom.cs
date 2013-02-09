using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace Shell.MVC2.Domain.Entities.Anewluv.Chat
{
    //TO do get rid of this topic crap and use what is in user list, match up user's on UI side
    [DataContract]
    public class ChatRoom
    {
        [Key]
        public int Key { get; set; }

        public DateTime? LastNudged { get; set; }
        public string Name { get; set; }
        public bool? Closed { get; set; }
        [StringLength(80)]
        public string Topic { get; set; }
        public string TopicScreenName { get; set; }
        //using topic and topic starter to show the names of the chatting parties in the chatboxes
        public string TopicStarter { get; set; }
        public string TopicStarterScreenName { get; set; }
        // Private rooms
        public bool Private { get; set; }
        public virtual ICollection<ChatUser> AllowedUsers { get; set; }
        public string InviteCode { get; set; }
        //roomtypes (enum)
        public string Type { get; set; }
        //ChatRequestRooms flags
        public bool? ChatRequestRejected { get; set; }
        public bool? ChatRequestAccepted { get; set; }

        public virtual ICollection<ChatUser> AllowedChatRequestUsers { get; set; } //might not be needed

        // Creator of the room
        public virtual ChatUser Creator { get; set; }

        // Creator and owners
        public virtual ICollection<ChatUser> Owners { get; set; }
        public virtual ICollection<ChatMessage> Messages { get; set; }
        public virtual ICollection<ChatUser> Users { get; set; }

        //do this using database
        public ChatRoom()
        {
          //  Owners = new SafeCollection<ChatUser>();
          //  Messages = new SafeCollection<ChatMessage>();
           // Users = new SafeCollection<ChatUser>();
           // AllowedUsers = new SafeCollection<ChatUser>();
        }
    }
}