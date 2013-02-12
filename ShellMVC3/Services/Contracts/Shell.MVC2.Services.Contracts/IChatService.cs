using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.ServiceModel.Web;
using Shell.MVC2.Domain.Entities.Anewluv.Chat;

namespace Shell.MVC2.Services.Chat
{
    public interface IChatService
    {
        // Users
        ChatUser AddUser(string userName, string screenname, string clientId, string userAgent, string password);
        ChatUser AddUser(string userName, string screenname, string email);
        ChatUser Online(string userName);
        List<ChatUser> GetOnlineUsers();

        ChatClient AddClient(ChatUser user, string clientId, string userAgent);
        void AuthenticateUser(string userName, string password);
        void ChangeUserName(ChatUser user, string newUserName);
        void ChangeUserPassword(ChatUser user, string oldPassword, string newPassword);
        void SetUserPassword(ChatUser user, string password);
        void UpdateActivity(ChatUser user, string clientId, string userAgent);
        ChatUser DisconnectClient(string clientId);

        // Rooms
        ChatRoom AddRoom(ChatUser user, string roomName);
        void UpdateRoom(ChatRoom room);
        void JoinRoom(ChatUser user, ChatRoom room, string inviteCode);
        void LeaveRoom(ChatUser user, ChatRoom room);
        void SetInviteCode(ChatUser user, ChatRoom room, string inviteCode);
        bool ConvertToChatRequestRoom(ChatUser user, ChatRoom targetRoom);

        // Messages
        ChatMessage AddMessage(ChatUser user, ChatRoom room, string id, string content);

        // Owner commands
        void AddOwner(ChatUser user, ChatUser targetUser, ChatRoom targetRoom);
        void RemoveOwner(ChatUser user, ChatUser targetUser, ChatRoom targetRoom);
        void KickUser(ChatUser user, ChatUser targetUser, ChatRoom targetRoom);
        void AllowUser(ChatUser user, ChatUser targetUser, ChatRoom targetRoom);
        void UnallowUser(ChatUser user, ChatUser targetUser, ChatRoom targetRoom);
        void LockRoom(ChatUser user, ChatRoom targetRoom);

        void CloseRoom(ChatUser user, ChatRoom targetRoom);
        void OpenRoom(ChatUser user, ChatRoom targetRoom);
        void ChangeTopic(ChatUser user, ChatRoom room, string newTopic);
    }
}