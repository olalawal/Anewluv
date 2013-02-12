using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Domain.Entities.Anewluv.Chat;

namespace Shell.MVC2.Interfaces
{
    

    
        public interface IChatRepository : IDisposable
        {

            IQueryable<ChatRoom> Rooms { get; }
            IQueryable<ChatUser> Users { get; }

            IQueryable<ChatUser> GetOnlineUsers(ChatRoom room);
            IQueryable<ChatUser> GetOnlineUsers();

            IQueryable<ChatUser> SearchUsers(string name);
            IQueryable<ChatMessage> GetMessagesByRoom(string roomName);
            ChatMessage GetMessageById(string id);
            IQueryable<ChatMessage> GetPreviousMessages(string messageId);
            IQueryable<ChatRoom> GetAllowedRooms(ChatUser user);
           


            ChatUser GetUserById(string userId);
            ChatRoom GetRoomByName(string roomName);

            ChatUser GetUserByName(string userName);
            ChatUser GetUserByClientId(string clientId);
            ChatUser GetUserByScreenName(string userScreenName);

            ChatClient GetClientById(string clientId);


            void AddUserRoom(ChatUser user, ChatRoom room);
            void RemoveUserRoom(ChatUser user, ChatRoom room);

            void Add(ChatClient client);
            void Add(ChatMessage message);
            void Add(ChatRoom room);
            void Add(ChatUser user);
            void Remove(ChatClient client);
            void Remove(ChatRoom room);
            void Remove(ChatUser user);
            bool Update(ChatRoom room);
            bool Update(ChatUser user);


            void RemoveAllClients();
            void CommitChanges();

            bool IsUserInRoom(ChatUser user, ChatRoom room);

            ////new items that were extentions 
            //public static ChatUser UserOnline(string name);
            //public static ChatRoom VerifyUserRoom(ChatUser user, string roomName);
            //public static ChatUser VerifyUserId(string userId);            
            ///// <summary>
            ///// updated 3-22-2012 check the room back wards and forwars i.e handle issues such as Kelly_Dar and Dar_kelly , make sure nethier exists.
            ///// </summary>
            ///// <param name="repository"></param>
            ///// <param name="roomName"></param>
            ///// <param name="OpenIfClosed"></param>
            ///// <returns></returns>
            //public static ChatRoom VerifyAndOpenRoomIfExists(string roomName, bool OpenIfClosed = true);
            //public static ChatRoom GetRoomIfCreatedBefore(string roomName);     
            //public static ChatRoom VerifyRoom(string roomName, bool mustBeOpen = true);           
            //public static ChatUser VerifyUser( string userName);

            //updated this to also remove empty spaces
              string NormalizeUserName(string userName);

              string NormalizeRoomName(string roomName);
            
    }
}
