using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shell.MVC2.Models.Chat; 

namespace Shell.MVC2.Repositories.Chat
{
    public interface IChatRepository  : IDisposable
    {
       
        IQueryable<ChatRoom> Rooms { get; }
        IQueryable<ChatUser> Users { get; }
        
        IQueryable<ChatUser> SearchUsers(string name);
        IQueryable<ChatMessage> GetMessagesByRoom(string roomName);
        IQueryable<ChatMessage> GetPreviousMessages(string messageId);
        IQueryable<ChatRoom> GetAllowedRooms(ChatUser user);

        ChatUser GetUserById(string userId);
        ChatRoom GetRoomByName(string roomName);
        ChatUser GetUserByName(string userName);
        ChatUser GetUserByClientId(string clientId);
        ChatUser GetUserByScreenName(string userScreenName);

        ChatClient GetClientById(string clientId);

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
    }
}