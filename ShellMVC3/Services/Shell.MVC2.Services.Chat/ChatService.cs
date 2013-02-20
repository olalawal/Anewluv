using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Shell.MVC2.Services.Contracts;
using Shell.MVC2.Domain.Entities.Anewluv.Chat;
using Shell.MVC2.Domain.Entities.Anewluv.Chat.ViewModels ;
using Shell.MVC2.Interfaces;
using System.ServiceModel.Activation;
using System.Text.RegularExpressions;
using Microsoft.AspNet.SignalR;

namespace Shell.MVC2.Services.Chat
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]  
    public class ChatService : IChatService
    {
        private readonly IChatRepository _repository;
        //added for functions require comunication with the hub
       // private readonly Hub _chathub; 

        // private readonly ICryptoService _crypto;
        //private readonly AccountMembershipService _membership;
        private readonly IMemberRepository  _membersrepository;

        private const int NoteMaximumLength = 140;
        private const int TopicMaximumLength = 80;

        public ChatService(IChatRepository repository, IMemberRepository membersrepository)
        {
            _repository = repository;
            _membersrepository = membersrepository;
           // _chathub = chathub;  //just  to see if this resloves
            // _crypto = crypto;
        }

        //returns a user who is online
        public ChatUser Online(string name)
        {
            return _repository.UserOnline(name);
        }

        //returns a user who is online
        public List<ChatUser> GetOnlineUsers()
        {
            return _repository.GetOnlineUsers().OrderBy(s => s.Name).ToList();
        }


        public ChatUser AddUser(string userName, string ScreenName, string email)
        {
            string gender = "";

            if (!IsValidUserName(userName))
            {
                throw new InvalidOperationException(String.Format("'{0}' is not a valid user name.", userName));
            }


            //get the gender if we are not disconnected 
#if !DISCONECTED
            gender = _membersrepository.getgenderbyscreenname(ScreenName);
#endif

            // This method is used in the auth workflow. If the username is taken it will add a number
            // to the user name.
            if (UserExists(userName))
            {
                var usersWithNameLikeMine = _repository.Users.Count(u => u.Name.StartsWith(userName));
                userName += usersWithNameLikeMine;
            }

            var user = new ChatUser
            {
                Name = userName,
                Status = (int)userstatusEnum.Active,
                Email = email,
                Gender = gender,
                // Hash = email.ToMD5(),
                ScreenName = ScreenName,
                Id = Guid.NewGuid().ToString("d"),
                LastActivity = DateTime.UtcNow
            };

            _repository.Add(user);
            _repository.CommitChanges();



            return user;
        }

        //direct log in to chat i guess
        public ChatUser AddUser(string userName, string ScreenName, string clientId, string userAgent, string password)
        {
            string gender = "";

            if (!IsValidUserName(userName))
            {
                throw new InvalidOperationException(String.Format("'{0}' is not a valid user name.", userName));
            }

            if (String.IsNullOrEmpty(password))
            {
                ThrowPasswordIsRequired();
            }

            EnsureUserNameIsAvailable(userName);

            //get the gender if we are not disconnected 
#if !DISCONECTED
            gender = _membersrepository.getgenderbyscreenname(ScreenName);
#endif

            //3-30-2012 updated code to allow for adding stuff from the repository
            var user = new ChatUser
            {
                Name = userName,
                ScreenName = ScreenName,
                Gender = gender,
                Status = (int)userstatusEnum.Active,
                Id = Guid.NewGuid().ToString("d"),
                //  Salt = _crypto.CreateSalt(),
                LastActivity = DateTime.UtcNow
            };

            ValidatePassword(password);
            user.HashedPassword = password; // password.ToSha256(user.Salt);

            _repository.Add(user);

            AddClient(user, clientId, userAgent);

            return user;
        }

        public void AuthenticateUser(string userName, string password)
        {
            ChatUser user = _repository.VerifyUser(userName);

            if (user.HashedPassword == null)
            {
                throw new InvalidOperationException(String.Format("The nick '{0}' is unclaimable", userName));
            }

            //  if (user.HashedPassword != password.ToSha256(user.Salt))
            if (user.HashedPassword != password)
            {
                throw new InvalidOperationException(String.Format("Unable to claim '{0}'.", userName));
            }

            EnsureSaltedPassword(user, password);
        }

        public void ChangeUserName(ChatUser user, string newUserName)
        {
            if (!IsValidUserName(newUserName))
            {
                throw new InvalidOperationException(String.Format("'{0}' is not a valid user name.", newUserName));
            }

            if (user.Name.Equals(newUserName, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("That's already your username...");
            }

            EnsureUserNameIsAvailable(newUserName);

            // Update the user name
            user.Name = newUserName;
        }

        public void SetUserPassword(ChatUser user, string password)
        {
            ValidatePassword(password);
            user.HashedPassword = password;//.ToSha256(user.Salt);
        }

        public void ChangeUserPassword(ChatUser user, string oldPassword, string newPassword)
        {
            // if (user.HashedPassword != oldPassword.ToSha256(user.Salt))
            if (user.HashedPassword != oldPassword)
            {
                throw new InvalidOperationException("Passwords don't match.");
            }

            ValidatePassword(newPassword);

            EnsureSaltedPassword(user, newPassword);
        }




        public ChatRoom AddRoom(ChatUser user, string name)
        {
            if (name.Equals("Lobby", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Lobby is not a valid chat room.");
            }

            if (!IsValidRoomName(name))
            {
                throw new InvalidOperationException(String.Format("'{0}' is not a valid room name.", name));
            }

            var room = new ChatRoom
            {
                Name = name,
                Creator = user
            };

            room.Owners.Add(user);

            _repository.Add(room);

            user.OwnedRooms.Add(room);

            return room;
        }

        public void JoinRoom(ChatUser user, ChatRoom room, string inviteCode)
        {
            // Throw if the room is private but the user isn't allowed
            if (room.Private)
            {
                // First, check if the invite code is correct
                if (!String.IsNullOrEmpty(inviteCode) && String.Equals(inviteCode, room.InviteCode, StringComparison.OrdinalIgnoreCase))
                {
                    // It is, add the user to the allowed users so that future joins will work
                    room.AllowedUsers.Add(user);
                }
                if (!IsUserAllowed(room, user))
                {
                    throw new InvalidOperationException(String.Format("Unable to join {0}. This room is locked and you don't have permission to enter. If you have an invite code, make sure to enter it in the /join command", room.Name));
                }
            }

            // Add this room to the user's list of rooms
            user.Rooms.Add(room);

            // Add this user to the list of room's users
            room.Users.Add(user);
        }

        public void UpdateRoom(ChatRoom room)
        {


        }

        public void SetInviteCode(ChatUser user, ChatRoom room, string inviteCode)
        {
            EnsureOwner(user, room);
            if (!room.Private)
            {
                throw new InvalidOperationException("Only private rooms can have invite codes");
            }

            // Set the invite code and save
            room.InviteCode = inviteCode;
            _repository.CommitChanges();
        }

        public void UpdateActivity(ChatUser user, string clientId, string userAgent)
        {
            user.Status = (int)userstatusEnum.Active;
            user.LastActivity = DateTime.UtcNow;

            ChatClient client = AddClient(user, clientId, userAgent);
            client.UserAgent = userAgent;
            client.LastActivity = DateTimeOffset.UtcNow;

            // Remove any Afk notes.
            if (user.IsAfk)
            {
                user.AfkNote = null;
                user.IsAfk = false;
            }
        }




        public void LeaveRoom(ChatUser user, ChatRoom room)
        {
            // Remove the user from the room
            room.Users.Remove(user);

            // Remove this room from the users' list
            user.Rooms.Remove(room);
        }

        public ChatMessage AddMessage(ChatUser user, ChatRoom room, string id, string content)
        {
            var chatMessage = new ChatMessage
            {
                Id = id,
                User = user,
                Content = content,
                When = DateTimeOffset.UtcNow,
                Room = room
            };

            _repository.Add(chatMessage);

            return chatMessage;
        }

        public void AddOwner(ChatUser ownerOrCreator, ChatUser targetUser, ChatRoom targetRoom)
        {
            // Ensure the user is owner of the target room
            EnsureOwner(ownerOrCreator, targetRoom);

            if (targetRoom.Owners.Contains(targetUser))
            {
                // If the target user is already an owner, then throw
                throw new InvalidOperationException(String.Format("'{0}' is already an owner of '{1}'.", targetUser.Name, targetRoom.Name));
            }

            // Make the user an owner
            targetRoom.Owners.Add(targetUser);
            targetUser.OwnedRooms.Add(targetRoom);

            if (targetRoom.Private)
            {
                if (!targetRoom.AllowedUsers.Contains(targetUser))
                {
                    // If the room is private make this user allowed
                    targetRoom.AllowedUsers.Add(targetUser);
                    targetUser.AllowedRooms.Add(targetRoom);
                }
            }
        }

        public void RemoveOwner(ChatUser creator, ChatUser targetUser, ChatRoom targetRoom)
        {
            // Ensure the user is creator of the target room
            EnsureCreator(creator, targetRoom);

            if (!targetRoom.Owners.Contains(targetUser))
            {
                // If the target user is not an owner, then throw
                throw new InvalidOperationException(String.Format("'{0}' is not an owner of '{1}'.", targetUser.Name, targetRoom.Name));
            }

            // Remove user as owner of room
            targetRoom.Owners.Remove(targetUser);
            targetUser.OwnedRooms.Remove(targetRoom);
        }

        public void KickUser(ChatUser user, ChatUser targetUser, ChatRoom targetRoom)
        {
            EnsureOwner(user, targetRoom);

            if (targetUser == user)
            {
                throw new InvalidOperationException("Why would you want to kick yourself?");
            }

            if (!IsUserInRoom(targetRoom, targetUser))
            {
                throw new InvalidOperationException(String.Format("'{0}' isn't in '{1}'.", targetUser.Name, targetRoom.Name));
            }

            // If this user isnt' the creator and the target user is an owner then throw
            if (targetRoom.Creator != user && targetRoom.Owners.Contains(targetUser))
            {
                throw new InvalidOperationException("Owners cannot kick other owners. Only the room creator and kick an owner.");
            }

            LeaveRoom(targetUser, targetRoom);
        }

        public ChatClient AddClient(ChatUser user, string clientId, string userAgent)
        {
            ChatClient client = _repository.GetClientById(clientId);
            if (client != null)
            {
                return client;
            }

            client = new ChatClient
            {
                Id = clientId,
                User = user,
                UserAgent = userAgent,
                LastActivity = DateTimeOffset.UtcNow
            };

            _repository.Add(client);
            _repository.CommitChanges();

            return client;
        }

        public ChatUser DisconnectClient(string clientId)
        {
            // Remove this client from the list of user's clients
            ChatClient client = _repository.GetClientById(clientId);

            // No client tracking this user
            if (client == null)
            {
                return null;
            }

            // Get the user for this client
            ChatUser user = client.User;

            if (user != null)
            {
                user.ConnectedClients.Remove(client);

                if (!user.ConnectedClients.Any())
                {
                    // If no more clients mark the user as offline
                    user.Status = (int)userstatusEnum.Offline;
                }

                _repository.Remove(client);
                _repository.CommitChanges();
            }

            return user;
        }

        private void EnsureUserNameIsAvailable(string userName)
        {
            if (UserExists(userName))
            {
                ThrowUserExists(userName);
            }
        }

        private bool UserExists(string userName)
        {
            return _repository.Users.Any(u => u.Name.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }

        //updated this to also remove empty spaces
        internal static string NormalizeUserName(string userName)
        {

            return userName.StartsWith("@") ? userName.Substring(1) : userName;
        }

        internal static string NormalizeRoomName(string roomName)
        {
            return roomName.StartsWith("#") ? roomName.Substring(1) : roomName;
        }

        internal static void ThrowUserExists(string userName)
        {
            throw new InvalidOperationException(String.Format("Username {0} already taken, please pick a new one using '/nick nickname'.", userName));
        }

        internal static void ThrowPasswordIsRequired()
        {
            throw new InvalidOperationException("A password is required.");
        }

        internal static bool IsUserInRoom(ChatRoom room, ChatUser user)
        {
            return room.Users.Any(r => r.Name.Equals(user.Name, StringComparison.OrdinalIgnoreCase));
        }

        private bool IsUserAllowed(ChatRoom room, ChatUser user)
        {
            return room.AllowedUsers.Contains(user);
        }

        private static void ValidatePassword(string password)
        {
            if (String.IsNullOrEmpty(password) || password.Length < 6)
            {
                throw new InvalidOperationException("Your password must be at least 6 characters.");
            }
        }

        private static bool IsValidUserName(string name)
        {
            return !String.IsNullOrEmpty(name) && Regex.IsMatch(name, "^[\\w-_.]{1,30}$");
        }

        private static bool IsValidRoomName(string name)
        {
            return !String.IsNullOrEmpty(name) && Regex.IsMatch(name, "^[\\w-_]{1,30}$");
        }

        private static void EnsureOwner(ChatUser user, ChatRoom room)
        {
            if (!room.Owners.Contains(user))
            {
                throw new InvalidOperationException("You are not an owner of room '" + room.Name + "'");
            }
        }

        private static void EnsureCreator(ChatUser user, ChatRoom room)
        {
            if (user != room.Creator)
            {
                throw new InvalidOperationException("You are not the creator of " + room.Name);
            }
        }

        private void EnsureSaltedPassword(ChatUser user, string password)
        {
            if (String.IsNullOrEmpty(user.Salt))
            {
                user.Salt = "";//_crypto.CreateSalt();
            }
            user.HashedPassword = password; //.ToSha256(user.Salt);
        }

        public void AllowUser(ChatUser user, ChatUser targetUser, ChatRoom targetRoom)
        {
            EnsureOwner(user, targetRoom);

            if (!targetRoom.Private)
            {
                throw new InvalidOperationException(String.Format("{0} is not a private room.", targetRoom.Name));
            }

            if (targetUser.AllowedRooms.Contains(targetRoom))
            {
                throw new InvalidOperationException(String.Format("{0} is already allowed for {1}.", targetUser.Name, targetRoom.Name));
            }

            targetRoom.AllowedUsers.Add(targetUser);
            targetUser.AllowedRooms.Add(targetRoom);

            _repository.CommitChanges();
        }

        public void UnallowUser(ChatUser user, ChatUser targetUser, ChatRoom targetRoom)
        {
            EnsureOwner(user, targetRoom);

            if (targetUser == user)
            {
                throw new InvalidOperationException("Why would you want to unallow yourself?");
            }

            if (!targetRoom.Private)
            {
                throw new InvalidOperationException(String.Format("{0} is not a private room.", targetRoom.Name));
            }

            if (!targetUser.AllowedRooms.Contains(targetRoom))
            {
                throw new InvalidOperationException(String.Format("{0} isn't allowed to access {1}.", targetUser.Name, targetRoom.Name));
            }

            // If this user isn't the creator and the target user is an owner then throw
            if (targetRoom.Creator != user && targetRoom.Owners.Contains(targetUser))
            {
                throw new InvalidOperationException("Owners cannot unallow other owners. Only the room creator and unallow an owner.");
            }

            targetRoom.AllowedUsers.Remove(targetUser);
            targetUser.AllowedRooms.Remove(targetRoom);

            // Make the user leave the room
            LeaveRoom(targetUser, targetRoom);

            _repository.CommitChanges();
        }

        public void LockRoom(ChatUser user, ChatRoom targetRoom)
        {
            EnsureOwner(user, targetRoom);

            if (targetRoom.Private)
            {
                throw new InvalidOperationException(String.Format("{0} is already locked.", targetRoom.Name));
            }

            // Make the room private
            targetRoom.Private = true;

            // Add the creator to the allowed list
            targetRoom.AllowedUsers.Add(user);

            // Add the room to the users' list
            user.AllowedRooms.Add(targetRoom);

            // Make all users in the current room allowed
            foreach (var u in _repository.GetOnlineUsersInRoom(targetRoom) )  ///targetRoom.Users.Online()
            {
                u.AllowedRooms.Add(targetRoom);
                targetRoom.AllowedUsers.Add(u);
            }

            _repository.CommitChanges();
        }

        public bool ConvertToChatRequestRoom(ChatUser user, ChatRoom targetRoom)
        {



            //if (targetRoom.ChatRequest)
            //{
            //    //TO DO figure out how to send errors back to the UI 
            //    // throw new InvalidOperationException(String.Format("You aleady have a chat request open with this member.", targetRoom.Name));
            //    //send message to sender stating you already sent this person a message

            //    return false;
            //}

            ////allow any room to be converted to a chat request , if it was not already
            ////EnsureOwner(user, targetRoom);

            //// Make the room private
            //targetRoom.ChatRequest = true;

            //// Add the creator to the allowed list
            //targetRoom.AllowedUsers.Add(user);

            //// Add the room to the users' list
            //user.AllowedRooms.Add(targetRoom);

            //// Make all users in the current room allowed
            //foreach (var u in targetRoom.Users.Online())
            //{
            //    u.AllowedRooms.Add(targetRoom);
            //    targetRoom.AllowedUsers.Add(u);
            //}

            //_repository.CommitChanges();

            return true;
        }

        //4-1-2012 updated code to only check if user is owner if there are more than two users in the room
        public void CloseRoom(ChatUser user, ChatRoom targetRoom)
        {
            if (targetRoom.Users.Count > 3)
                EnsureOwner(user, targetRoom);

            if (targetRoom.Closed)
            {
                throw new InvalidOperationException(String.Format("{0} is already closed.", targetRoom.Name));
            }

            // Make sure the (owner) user is not in the room.
            if (targetRoom.Users.Contains(user))
            {
                throw new InvalidOperationException("You are trying to close a room which you are still in. Please leave the room before closing it.");
            }

            // Kick all existing users in the room.
            foreach (var targetUser in targetRoom.Users.ToList())
            {
                LeaveRoom(targetUser, targetRoom);
            }

            // Make the room closed.
            targetRoom.Closed = true;

            _repository.CommitChanges();
        }

        public void OpenRoom(ChatUser user, ChatRoom targetRoom)
        {
            EnsureOwner(user, targetRoom);

            if (!targetRoom.Closed)
            {
                throw new InvalidOperationException(string.Format("{0} is already open.", targetRoom.Name));
            }

            // Open the room
            targetRoom.Closed = false;
            _repository.CommitChanges();
        }

        public void ChangeTopic(ChatUser user, ChatRoom room, string newTopic)
        {
            //TO do figure out why this is bad at some point
            //3/24/2012 allowing user to to change a topic if they are in a room, might as well anwyways , no rooms have owners right now
            // EnsureOwner(user, room);
            room.Topic = newTopic;
            _repository.CommitChanges();
        }

        internal static void ValidateNote(string note, string noteTypeName = "note", int? maxLength = null)
        {
            var lengthToValidateFor = (maxLength ?? NoteMaximumLength);
            if (!String.IsNullOrWhiteSpace(note) &&
                note.Length > lengthToValidateFor)
            {
                throw new InvalidOperationException(
                    String.Format("Sorry, but your {1} is too long. Can please keep it under {0} characters.",
                        lengthToValidateFor, noteTypeName));
            }
        }

        internal static void ValidateTopic(string topic)
        {
            ValidateNote(topic, noteTypeName: "topic", maxLength: TopicMaximumLength);
        }

        internal static void ValidateIsoCode(string isoCode)
        {
            string country = GetCountry(isoCode);
            if (String.IsNullOrWhiteSpace(country))
            {
                throw new InvalidOperationException(
                    "Sorry, but the country ISO code you requested doesn't exist. Please refer to http://en.wikipedia.org/wiki/ISO_3166-1_alpha-2 for a proper list of country ISO codes.");
            }
        }

        internal static string GetCountry(string isoCode)
        {
            if (String.IsNullOrEmpty(isoCode))
            {
                return null;
            }

            string country;
            //   return Countries.TryGetValue(isoCode, out country) ? country : null;
            //TO DO get from dtating service if need be
            return "";
        }
    }
}