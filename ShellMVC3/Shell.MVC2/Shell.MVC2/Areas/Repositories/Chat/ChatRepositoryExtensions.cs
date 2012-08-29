using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shell.MVC2.Models.Chat;
using Shell.MVC2.Services.Chat; //TO DO convert to WCF
namespace Shell.MVC2.Repositories.Chat
{
    public static class RepositoryExtensions
    {
        public static IQueryable<ChatUser> Online(this IQueryable<ChatUser> source)
        {
            return source.Where(u => u.Status != (int)UserStatus.Offline);
        }

        public static IEnumerable<ChatUser> Online(this IEnumerable<ChatUser> source)
        {
            return source.Where(u => u.Status != (int)UserStatus.Offline);
        }

        public static ChatUser UserOnline(this IChatRepository repository, string name)
        {
            ChatUser user = repository.GetUserByName(name);

            if (user != null && user.Status != (int)UserStatus.Offline)
                return user;

            return null;
           
        }

        public static IEnumerable<ChatRoom> Allowed(this IEnumerable<ChatRoom> rooms, string userId)
        {
            return from r in rooms
                   where !r.Private ||
                         r.Private && r.AllowedUsers.Any(u => u.Id == userId)
                   select r;
        }

        public static ChatRoom VerifyUserRoom(this IChatRepository   repository, ChatUser user, string roomName)
        {
            if (String.IsNullOrEmpty(roomName))
            {
                throw new InvalidOperationException("Use '/join room' to join a room.");
            }

            //3-24-2011 added code to get the room if it already existed 
            ChatRoom room  = GetRoomIfCreatedBefore(repository, roomName);
            //if (room == null)
            //{
            //    roomName = ChatService.NormalizeRoomName(roomName);
            //    room = repository.GetRoomByName(roomName);
            //}

            if (room == null)
            {
                throw new InvalidOperationException(String.Format("You're in '{0}' but it doesn't exist.", roomName));
            }

            if (!room.Users.Any(u => u.Name.Equals(user.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException(String.Format("You're not in '{0}'. Use '/join {0}' to join it.", roomName));
            }

            return room;
        }

        public static ChatUser VerifyUserId(this IChatRepository  repository, string userId)
        {
            ChatUser user = repository.GetUserById(userId);

            if (user == null)
            {
                // The user isn't logged in 
                throw new InvalidOperationException("You're not logged in.");
            }

            return user;
        }
        /// <summary>
        /// updated 3-22-2012 check the room back wards and forwars i.e handle issues such as Kelly_Dar and Dar_kelly , make sure nethier exists.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="roomName"></param>
        /// <param name="OpenIfClosed"></param>
        /// <returns></returns>
        public static ChatRoom VerifyAndOpenRoomIfExists(this IChatRepository repository, string roomName, bool OpenIfClosed= true)
        {



            if (String.IsNullOrWhiteSpace(roomName))
            {
                throw new InvalidOperationException("Room name cannot be blank!");
            }
         
                //best wau uf ut works
            var room = GetRoomIfCreatedBefore(repository, roomName);
           

            if (room == null)
            {
                return null;
            }

            if (room.Closed && OpenIfClosed)
            {
                room.Closed = false;
                return room;
            }

            return room;
        }

        public static ChatRoom GetRoomIfCreatedBefore(this IChatRepository repository, string roomName)
        {
            // var toUserName = parts[1];          
            string[] RoomMembers = roomName.Split('_');
            if (RoomMembers.Count() > 0)           
            //make sure room does not exist
            return  (from z in repository.Rooms where RoomMembers.All(s => z.Name.Contains(s)) select z).FirstOrDefault();

            roomName = ChatService.NormalizeRoomName(roomName);            
            return repository.GetRoomByName(roomName);
 
        }

        //public static  createChatRequestRoom(this IChatRepository repository,ChatUser TopicStarter, ChatUser Topic, string roomName, string roomType)
        //{
        //    ChatRoom room = GetRoomIfCreatedBefore(repository, roomName);
        //    room.Owners.Add(TopicStarter);
        //    room.Owners.Add(Topic);
        //    //set the topic as the person being invited
        //    room.Topic = Topic.Name;           
        //    //set the topic starter as the person who initiated the request
        //    room.TopicStarter = TopicStarter.Name;
        //    room.Type = "ChatRequest";
        //    room.ChatRequestAccepted = false;
        //    room.ChatRequestRejected = false;


        //}




        public static ChatRoom VerifyRoom(this IChatRepository repository, string roomName, bool mustBeOpen = true)
        {
            if (String.IsNullOrWhiteSpace(roomName))
            {
                throw new InvalidOperationException("Room name cannot be blank!");
            }

        
            //3-24-2011 added code to get the room if it already existed 
            ChatRoom room = GetRoomIfCreatedBefore(repository, roomName);
          
            if (room == null)
            {
                throw new InvalidOperationException(String.Format("Unable to find room '{0}'", roomName));
            }

            if (room.Closed && mustBeOpen)
            {
                throw new InvalidOperationException(String.Format("The room '{0}' is closed", roomName));
            }

            return room;
        }

        public static ChatUser VerifyUser(this IChatRepository repository, string userName)
        {
            userName = ChatService.NormalizeUserName(userName);

            ChatUser user = repository.GetUserByName(userName);

            if (user == null)
            {
                throw new InvalidOperationException(String.Format("Unable to find user '{0}'.", userName));
            }

            return user;
        }
    }
}