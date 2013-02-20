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

namespace Shell.MVC2.Services.Contracts
{
    public interface IChatNotificationService
    {
        void ChangeGravatar(ChatUser user);
        void JoinRoom(ChatUser user, ChatRoom room);

        // Client actions
        void LogOn(ChatUser user, string clientId);
        void LogOut(ChatUser user, string clientId);

        void ListUsers();
        void ListUsers(ChatRoom room, IEnumerable<string> names);
        void ListRooms(ChatUser user);
        void ListUsers(IEnumerable<ChatUser> users);

        void NudgeRoom(ChatRoom room, ChatUser user);
        void NugeUser(ChatUser user, ChatUser targetUser);

        void ChangePassword();
        void SetPassword();
        void ChangeNote(ChatUser user);
        void ChangeFlag(ChatUser user);
        void ChangeTopic(ChatUser user, ChatRoom room);

        void PostNotification(ChatRoom room, ChatUser user, string message);
        void SendPrivateMessage(ChatUser user, ChatUser targetUser, string messageText);
        void SendChatRequest(ChatUser user, ChatUser targetUser, string messageText,ChatRoom room);
        void RejectChatRequest(ChatUser user, ChatUser targetUser, string messageText, ChatRoom room);
        void AcceptChatRequest(ChatUser user, ChatUser targetUser, string messageText, ChatRoom room);
        void LeaveRoom(ChatUser user, ChatRoom room);

        void AddOwner(ChatUser targetUser, ChatRoom targetRoom);
        void RemoveOwner(ChatUser targetUser, ChatRoom targetRoom);
        void KickUser(ChatUser targetUser, ChatRoom targetRoom);
        void AllowUser(ChatUser targetUser, ChatRoom targetRoom);
        void UnallowUser(ChatUser targetUser, ChatRoom targetRoom);

        void OnUserCreated(ChatUser user);
        void OnUserNameChanged(ChatUser user, string oldUserName, string newUserName);

        void OnSelfMessage(ChatRoom room, ChatUser user, string content);

        void ShowUserInfo(ChatUser user);
        void ShowHelp();
        void ShowRooms();

        void LockRoom(ChatUser targetUser, ChatRoom room);
        void SendDialogNotification(ChatUser targetUser, string message);
        void CloseRoom(IEnumerable<ChatUser> users, ChatRoom room);

    }
}