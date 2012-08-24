using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



using Shell.MVC2.Models.Chat;
using Shell.MVC2.Services.Chat;
using Shell.MVC2.Repositories.Chat;

namespace Shell.MVC2.Infastructure.Chat
{
    public class CommandManager
    {
        private readonly string _clientId;
        private readonly string _userAgent;
        private readonly string _userId;
        private string _roomName;       //switched this from read only since roomname could already exist
        private readonly INotificationService _notificationService;
        private readonly IChatService _chatService;
        private readonly IChatRepository  _repository;

        public CommandManager(string clientId,
                              string userId,
                              string roomName,
                              IChatService service,
                              IChatRepository repository,
                              INotificationService notificationService)
            : this(clientId, null, userId, roomName, service, repository, notificationService)
        {
        }

        public CommandManager(string clientId,
                              string userAgent,
                              string userId,
                              string roomName,
                              IChatService service,
                              IChatRepository repository,
                              INotificationService notificationService)
        {
            _clientId = clientId;
            _userAgent = userAgent;
            _userId = userId;
            _roomName = roomName;
            _chatService = service;
            _repository = repository;
            _notificationService = notificationService;
        }

        public bool TryHandleCommand(string commandName, string[] parts)
        {
            commandName = commandName.Trim();
            if (commandName.StartsWith("/"))
            {
                return false;
            }

            if (!TryHandleBaseCommand(commandName, parts) &&
                !TryHandleUserCommand(commandName, parts) &&
                !TryHandleRoomCommand(commandName, parts))
            {
                // If none of the commands are valid then throw an exception
                throw new InvalidOperationException(String.Format("'{0}' is not a valid command.", commandName));
            }

            return true;
        }

        public bool TryHandleCommand(string command)
        {
            command = command.Trim();
            if (!command.StartsWith("/"))
            {
                return false;
            }

            string[] parts = command.Substring(1).Split(' ');
            string commandName = parts[0];

            return TryHandleCommand(commandName, parts);
        }

        // Commands that require a user and room
        private bool TryHandleRoomCommand(string commandName, string[] parts)
        {
            ChatUser user = _repository.VerifyUserId(_userId);
           //TO DO changed this to use the new VerifyAndOpen
            ChatRoom room = _repository.VerifyUserRoom(user, _roomName);
            //ChatRoom room = _repository.VerifyAndOpenRoomIfExists()

            if (commandName.Equals("me", StringComparison.OrdinalIgnoreCase))
            {
                HandleMe(room, user, parts);
                return true;
            }
            else if (commandName.Equals("leave", StringComparison.OrdinalIgnoreCase))
            {
                HandleLeave(room, user);

                return true;
            }
            else if (commandName.Equals("nudge", StringComparison.OrdinalIgnoreCase))
            {
                HandleNudge(room, user, parts);

                return true;
            }
            else if (TryHandleOwnerCommand(user, room, commandName, parts))
            {
                return true;
            }

            return false;
        }

        // Commands that require the user to be the owner of the room
        private bool TryHandleOwnerCommand(ChatUser user, ChatRoom room, string commandName, string[] parts)
        {
            if (commandName.Equals("kick", StringComparison.OrdinalIgnoreCase))
            {
                HandleKick(user, room, parts);

                return true;
            }
            else if (commandName.Equals("invitecode", StringComparison.OrdinalIgnoreCase))
            {
                HandleInviteCode(user, room, forceReset: false);
                return true;
            }
            else if (commandName.Equals("resetinvitecode", StringComparison.OrdinalIgnoreCase))
            {
                HandleInviteCode(user, room, forceReset: true);
                return true;
            }
            else if (commandName.Equals("topic", StringComparison.OrdinalIgnoreCase))
            {
                HandleTopic(user, room, parts);
                return true;
            }

            return false;
        }

        private bool TryHandleBaseCommand(string commandName, string[] parts)
        {
            if (commandName.Equals("help", StringComparison.OrdinalIgnoreCase))
            {
                HandleHelp();

                return true;
            }
            else if (commandName.Equals("nick", StringComparison.OrdinalIgnoreCase))
            {
                HandleNick(parts);

                return true;
            }

            return false;
        }

        // Commands that require a user name
        private bool TryHandleUserCommand(string commandName, string[] parts)
        {
            ChatUser user = _repository.VerifyUserId(_userId);

            if (commandName.Equals("rooms", StringComparison.OrdinalIgnoreCase))
            {
                HandleRooms();

                return true;
            }
            else if (commandName.Equals("list", StringComparison.OrdinalIgnoreCase))
            {
                HandleList(parts);
                return true;
            }
            else if (commandName.Equals("where", StringComparison.OrdinalIgnoreCase))
            {
                HandleWhere(parts);
                return true;
            }
            else if (commandName.Equals("who", StringComparison.OrdinalIgnoreCase))
            {
                HandleWho(parts);
                return true;
            }
            else if (commandName.Equals("join", StringComparison.OrdinalIgnoreCase))
            {
                HandleJoin(user, parts);

                return true;
            }
            else if (commandName.Equals("create", StringComparison.OrdinalIgnoreCase))
            {
                HandleCreate(user, parts);

                return true;
            }
            else if (commandName.Equals("msg", StringComparison.OrdinalIgnoreCase))
            {
                HandleMsg(user, parts);

                return true;
            }
            //new method to send chat request via popup
            else if (commandName.Equals("chatreq", StringComparison.OrdinalIgnoreCase))
            {
                HandleCreateChatRequest  (user, parts);

                return true;
            }

                   //new method to send chat request via popup
            else if (commandName.Equals("acceptchatreq", StringComparison.OrdinalIgnoreCase))
            {
                HandleAcceptChatRequest(user, parts);

                return true;
            }


                          //new method to send chat request via popup
            else if (commandName.Equals("rejectchatreq", StringComparison.OrdinalIgnoreCase))
            {
                HandleRejectChatRequest(user, parts);

                return true;
            }

            else if (commandName.Equals("gravatar", StringComparison.OrdinalIgnoreCase))
            {
                HandleGravatar(user, parts);

                return true;
            }
            else if (commandName.Equals("leave", StringComparison.OrdinalIgnoreCase) && parts.Length == 2)
            {
                HandleLeave(user, parts);

                return true;
            }
            else if (commandName.Equals("nudge", StringComparison.OrdinalIgnoreCase) && parts.Length == 2)
            {
                HandleNudge(user, parts);

                return true;
            }
            else if (commandName.Equals("addowner", StringComparison.OrdinalIgnoreCase))
            {
                HandleAddOwner(user, parts);

                return true;
            }
            else if (commandName.Equals("removeowner", StringComparison.OrdinalIgnoreCase))
            {
                HandleRemoveOwner(user, parts);

                return true;
            }
            else if (commandName.Equals("lock", StringComparison.OrdinalIgnoreCase))
            {
                HandleLock(user, parts);

                return true;
            }
            else if (commandName.Equals("close", StringComparison.OrdinalIgnoreCase))
            {
                HandleClose(user, parts);

                return true;
            }
            else if (commandName.Equals("allow", StringComparison.OrdinalIgnoreCase))
            {
                HandleAllow(user, parts);

                return true;
            }
            else if (commandName.Equals("unallow", StringComparison.OrdinalIgnoreCase))
            {
                HandleUnallow(user, parts);

                return true;
            }
            else if (commandName.Equals("logout", StringComparison.OrdinalIgnoreCase))
            {
                HandleLogOut(user);

                return true;
            }
            else if (commandName.Equals("note", StringComparison.OrdinalIgnoreCase))
            {
                HandleNote(user, parts);

                return true;
            }
            else if (commandName.Equals("afk", StringComparison.OrdinalIgnoreCase))
            {
                HandleAfk(user, parts);

                return true;
            }
            else if (commandName.Equals("flag", StringComparison.OrdinalIgnoreCase))
            {
                HandleFlag(user, parts);

                return true;
            }
            else if (commandName.Equals("open", StringComparison.OrdinalIgnoreCase))
            {
                HandleOpen(user, parts);

                return true;
            }

            return false;
        }

        private void HandleOpen(ChatUser user, string[] parts)
        {
            if (parts.Length < 2)
            {
                throw new InvalidOperationException("Which room do you want to open?");
            }

            string roomName = parts[1];
            ChatRoom room = _repository.VerifyRoom(roomName, mustBeOpen: false);

            _chatService.OpenRoom(user, room);
            // Automatically join user to newly opened room
            JoinRoom(user, room, null);
        }

        private void HandleTopic(ChatUser user, ChatRoom room, string[] parts)
        {
            string newTopic = String.Join(" ", parts.Skip(1)).Trim();
            ChatService.ValidateTopic(newTopic);
            newTopic = String.IsNullOrWhiteSpace(newTopic) ? null : newTopic;
            _chatService.ChangeTopic(user, room, newTopic);
            _notificationService.ChangeTopic(user, room);
        }

        private void HandleInviteCode(ChatUser user, ChatRoom room, bool forceReset)
        {
            if (String.IsNullOrEmpty(room.InviteCode) || forceReset)
            {
                _chatService.SetInviteCode(user, room, RandomUtils.NextInviteCode());
            }
            _notificationService.PostNotification(room, user, String.Format("Invite Code for this room: {0}", room.InviteCode));
        }

        private void HandleLogOut(ChatUser user)
        {
            _notificationService.LogOut(user, _clientId);
        }

        private void HandleUnallow(ChatUser user, string[] parts)
        {
            if (parts.Length == 1)
            {
                throw new InvalidOperationException("Which user to you want to revoke persmissions from?");
            }

            string targetUserName = parts[1];

            ChatUser targetUser = _repository.VerifyUser(targetUserName);

            if (parts.Length == 2)
            {
                throw new InvalidOperationException("Which room?");
            }

            string roomName = parts[2];
            ChatRoom targetRoom = _repository.VerifyRoom(roomName);

            _chatService.UnallowUser(user, targetUser, targetRoom);

            _notificationService.UnallowUser(targetUser, targetRoom);

            _repository.CommitChanges();
        }

        private void HandleAllow(ChatUser user, string[] parts)
        {
            if (parts.Length == 1)
            {
                throw new InvalidOperationException("Who do you want to allow?");
            }

            string targetUserName = parts[1];

            ChatUser targetUser = _repository.VerifyUser(targetUserName);

            if (parts.Length == 2)
            {
                throw new InvalidOperationException("Which room?");
            }

            string roomName = parts[2];
            ChatRoom targetRoom = _repository.VerifyRoom(roomName);

            _chatService.AllowUser(user, targetUser, targetRoom);

            _notificationService.AllowUser(targetUser, targetRoom);

            _repository.CommitChanges();
        }

        private void HandleAddOwner(ChatUser user, string[] parts)
        {
            if (parts.Length == 1)
            {
                throw new InvalidOperationException("Who do you want to make an owner?");
            }

            string targetUserName = parts[1];

            ChatUser targetUser = _repository.VerifyUser(targetUserName);

            if (parts.Length == 2)
            {
                throw new InvalidOperationException("Which room?");
            }

            string roomName = parts[2];
            ChatRoom targetRoom = _repository.VerifyRoom(roomName);

            _chatService.AddOwner(user, targetUser, targetRoom);

            _notificationService.AddOwner(targetUser, targetRoom);

            _repository.CommitChanges();
        }

        private void HandleRemoveOwner(ChatUser user, string[] parts)
        {
            if (parts.Length == 1)
            {
                throw new InvalidOperationException("Which owner do you want to remove?");
            }

            string targetUserName = parts[1];

            ChatUser targetUser = _repository.VerifyUser(targetUserName);

            if (parts.Length == 2)
            {
                throw new InvalidOperationException("Which room?");
            }

            string roomName = parts[2];
            ChatRoom targetRoom = _repository.VerifyRoom(roomName);

            _chatService.RemoveOwner(user, targetUser, targetRoom);

            _notificationService.RemoveOwner(targetUser, targetRoom);

            _repository.CommitChanges();
        }

        private void HandleKick(ChatUser user, ChatRoom room, string[] parts)
        {
            if (parts.Length == 1)
            {
                throw new InvalidOperationException("Who are you trying to kick?");
            }

            if (room.Users.Count == 1)
            {
                throw new InvalidOperationException("You're the only person in here...");
            }

            string targetUserName = parts[1];

            ChatUser targetUser = _repository.VerifyUser(targetUserName);

            _chatService.KickUser(user, targetUser, room);

            _notificationService.KickUser(targetUser, room);

            _repository.CommitChanges();
        }

        private void HandleLock(ChatUser user, string[] parts)
        {
            if (parts.Length < 2)
            {
                throw new InvalidOperationException("Which room do you want to lock?");
            }

            string roomName = parts[1];
            ChatRoom room = _repository.VerifyRoom(roomName);

            _chatService.LockRoom(user, room);

            _notificationService.LockRoom(user, room);
        }

        private void HandleClose(ChatUser user, string[] parts)
        {
            if (parts.Length < 2)
            {
                throw new InvalidOperationException("Which room do you want to close?");
            }

            string roomName = parts[1];
            ChatRoom room = _repository.VerifyRoom(roomName);

            // Before I close the room, I need to grab a copy of -all- the users in that room.
            // Otherwise, I can't send any notifications to the room users, because they
            // have already been kicked.
            var users = room.Users.ToList();
            _chatService.CloseRoom(user, room);

            _notificationService.CloseRoom(users, room);
        }

        private void HandleWhere(string[] parts)
        {
            if (parts.Length == 1)
            {
                throw new InvalidOperationException("Who are you trying to locate?");
            }

            ChatUser user = _repository.VerifyUser(parts[1]);
            _notificationService.ListRooms(user);
        }

        private void HandleWho(string[] parts)
        {
            if (parts.Length == 1)
            {
                _notificationService.ListUsers();
                return;
            }

            var name = ChatService.NormalizeUserName(parts[1]);

            ChatUser user = _repository.GetUserByName(name);

            if (user == null)
            {
                throw new InvalidOperationException(String.Format("We didn't find anyone with the username {0}", name));
            }

            _notificationService.ShowUserInfo(user);
        }

        private void HandleList(string[] parts)
        {
            if (parts.Length < 2)
            {
                throw new InvalidOperationException("List users in which room?");
            }

            string roomName = parts[1];
            ChatRoom room = _repository.VerifyRoom(roomName);

            var names = room.Users.Online().Select(s => s.Name);

            _notificationService.ListUsers(room, names);
        }

        private void HandleHelp()
        {
            _notificationService.ShowHelp();
        }

        private void HandleLeave(ChatUser user, string[] parts)
        {
            string roomName = parts[1];
            ChatRoom room = _repository.VerifyRoom(roomName);

            HandleLeave(room, user);
        }

        private void HandleLeave(ChatRoom room, ChatUser user)
        {
            _chatService.LeaveRoom(user, room);

            _notificationService.LeaveRoom(user, room);

            _repository.CommitChanges();
        }

        private void HandleMe(ChatRoom room, ChatUser user, string[] parts)
        {
            if (parts.Length < 2)
            {
                throw new InvalidOperationException("You what?");
            }

            var content = String.Join(" ", parts.Skip(1));

            _notificationService.OnSelfMessage(room, user, content);
        }

        private void HandleMsg(ChatUser user, string[] parts)
        {
            if (_repository.Users.Count() == 1)
            {
                throw new InvalidOperationException("You're the only person in here...");
            }

            if (parts.Length < 2 || String.IsNullOrWhiteSpace(parts[1]))
            {
                throw new InvalidOperationException("Who are you trying send a private message to?");
            }
            var toUserName = parts[1];
            ChatUser toUser = _repository.VerifyUser(toUserName);

            if (toUser == user)
            {
                throw new InvalidOperationException("You can't private message yourself!");
            }

            string messageText = String.Join(" ", parts.Skip(2)).Trim();

            if (String.IsNullOrEmpty(messageText))
            {
                throw new InvalidOperationException(String.Format("What did you want to say to '{0}'.", toUser.Name));
            }

            HashSet<string> urls;
            var transform = new TextTransform(_repository);
            messageText = transform.Parse(messageText);

            messageText = TextTransform.TransformAndExtractUrls(messageText, out urls);

            _notificationService.SendPrivateMessage(user, toUser, messageText);
        }

        //TO do handle stuff such as database logging on repository level i.e under save changes ?
        ///TO DO simplyfy this to only do creation of a chat request
        ///nothing to do with room since room stuff will be done during accept?
        /// <summary>
        /// new
        /// </summary>
        /// <param name="user"></param>
        /// <param name="parts"></param>
        private void HandleCreateChatRequest(ChatUser user, string[] parts)
        {

            //allow sending messages even if one user here
            #if! (DISCONECTED || DEBUG)
            if (_repository.Users.Count() == 1)
            {
                throw new InvalidOperationException("You're the only person in here...");
            }
            #endif




            if (parts.Length < 2 || String.IsNullOrWhiteSpace(parts[1]))
            {
                throw new InvalidOperationException("Who are you trying to open a chat request with ?");
            }
            var toUserName = parts[1];
            ChatUser toUser = _repository.VerifyUser(toUserName);
         
            if (parts.Length < 3 || String.IsNullOrWhiteSpace(parts[1]))
            {
                throw new InvalidOperationException("No room name was provided to create a chat request with");
            }

            //build the message
            string messageText = String.Join(" ", parts.Skip(3)).Trim();
            //add sender username
            messageText = user.ScreenName  + " "  + messageText;
            HashSet<string> urls;
            var transform = new TextTransform(_repository);
            messageText = transform.Parse(messageText);
            messageText = TextTransform.TransformAndExtractUrls(messageText, out urls);



            if (toUser == user)
            {
                throw new InvalidOperationException("You can't chat with yourself!");
            }


            if (String.IsNullOrEmpty(messageText))
            {
                throw new InvalidOperationException(String.Format("What did you want to say to '{0}'.", toUser.Name));
            }

            //now do the room stuff

            var roomName = parts[2];

            //get room if it already exists
            ChatRoom room = _repository.VerifyAndOpenRoomIfExists(roomName, false);
            //hanlde if room was already created and closed , i.e a prior chat request
            if (room != null && room.ChatRequestRejected == true)
            {
                //TO DO use notification 
                throw new InvalidOperationException(String.Format("Your chat request has already been refused"));
            }

            //to do can probbaly take this out
            if (room == null)
            {

                //No need to verify room anymore 
                //room might have been created but it was not closed which means the last request was just not responded to
           //     room = _repository.VerifyRoom(roomName);
          //  }
           // else
          //  {
                //create the room here then if we have a new connection
                room = _chatService.AddRoom(user, roomName);
                //TO DO set the toipic with no notification
            }
            
            //allow messages to self in disconnected mode
            #if! (DISCONECTED || DEBUG)
             if (toUser == user)
            {
                throw new InvalidOperationException("You can't private message yourself!");
            }
            #endif
                     
            //TO do think about what to do about people spamming invites and such
            //give the ability of the user to permanaly not allow chat invites to go out
            //covert the room to a chatrequest room
            if (room.ChatRequestAccepted  == true)
            {

                //just rejoin the rooms then
                if (ChatService.IsUserInRoom(room, user))
                {
                    _notificationService.JoinRoom(user, room);
                }
                else
                {
                    JoinRoom(user, room, null);
                }

                //only add if user is not already in the room
                if (ChatService.IsUserInRoom(room, toUser))
                {
                    _notificationService.JoinRoom(toUser, room);
                }
                else
                {
                    JoinRoom(toUser, room, null);
                }

                return;
                //_notificationService.SendDialogNotification(user, );
               // throw new InvalidOperationException(String.Format("The member '{0}' has already accepted your chat reuqest, click go to your contacts tab and open a chat room", toUser.Name));
            }
            else
            {

                //update room settings for an initated request
                //move this to chat service
                //TO DO move this to services
                room.Owners.Clear();
                room.Owners.Add(user);
                //set the topic as the person being invited
                room.Topic = toUser.Name;
                room.TopicScreenName = toUser.ScreenName;
                //set the topic starter as the person who initiated the request
                room.TopicStarter = user.Name;
                room.TopicStarterScreenName  = user.ScreenName;
                room.Type = "ChatRequest";
                room.ChatRequestAccepted = false;
                room.ChatRequestRejected = false ;

                //update the room here
                _repository.Update(room);

                //now join both users to the room now that it has the correct ChatRequest = true flag so             
                //when notified the UI will populate the correct type of boxes on UI    

                //add the from user as the owner
                //only add if user is not already in the room
                if (ChatService.IsUserInRoom(room, user))
                {
                    _notificationService.JoinRoom(user, room);
                }
                else
                {
                    JoinRoom(user, room, null);
                }

                //only add if user is not already in the room
                if (ChatService.IsUserInRoom(room, toUser ))
                {
                    _notificationService.JoinRoom(toUser, room);
                }
                else
                {
                    JoinRoom(toUser, room, null);
                }
                          

                _repository.CommitChanges();

                //_notificationService.ConvertToChatRequestRoom(user, room);


                //TO DO create a chat room and join the recipeint to it so he can see the message
                //notifiy that the room has been converted to chat request and send the updates to both         
                _notificationService.SendChatRequest(user, toUser, messageText, room);
            }
            
        }


        ///TO DO modify this code the do all the room stuff, send chat request is just for notification
        /// <summary>
        /// new
        /// </summary>
        /// <param name="user"></param>
        /// <param name="parts"></param>
        private void HandleRejectChatRequest(ChatUser user, string[] parts)
        {

            //allow sending messages even if one user here
#if! (DISCONECTED || DEBUG)
            if (_repository.Users.Count() == 1)
            {
                throw new InvalidOperationException("You're the only person in here...");
            }
#endif
            if (parts.Length < 2 || String.IsNullOrWhiteSpace(parts[1]))
            {
                throw new InvalidOperationException("Who are you trying to open a chat request with ?");
            }
            var toUserName = parts[1];
            ChatUser toUser = _repository.VerifyUser(toUserName);

            if (parts.Length < 3 || String.IsNullOrWhiteSpace(parts[1]))
            {
                throw new InvalidOperationException("No room is avialable to chat inh");
            }

            //build the message
            string messageText = String.Join(" ", parts.Skip(3)).Trim();
            //add sender username
            messageText = messageText + user.ScreenName;
            HashSet<string> urls;
            var transform = new TextTransform(_repository);
            messageText = transform.Parse(messageText);
            messageText = TextTransform.TransformAndExtractUrls(messageText, out urls);


            if (String.IsNullOrEmpty(messageText))
            {
                throw new InvalidOperationException(String.Format("What did you want to say to '{0}'.", toUser.Name));
            }

            //now do the room stuff

            var roomName = parts[2];

            //get room and open it it already exists
            ChatRoom room = _repository.VerifyAndOpenRoomIfExists(roomName, true );
            
            //hanlde if room was already created and closed , i.e a prior chat request
            if (room != null && room.Closed)
            {
                //TO DO use notification 
                throw new InvalidOperationException(String.Format("Your chat request has already been refused"));
            }

            //to do can probbaly take this out
            if (room == null)
            {

                //No need to verify room anymore 
                //room might have been created but it was not closed which means the last request was just not responded to
                //     room = _repository.VerifyRoom(roomName);
                //  }
                // else
                //  {
                //create the room here then if we have a new connection
                room = _chatService.AddRoom(user, roomName);
                //TO DO set the toipic with no notification
            }

            //allow messages to self in disconnected mode
#if! (DISCONECTED || DEBUG)
             if (toUser == user)
            {
                throw new InvalidOperationException("You can't Chat with yourself!");
            }
#endif

            //update room settings for a rejected request before closing the rooms since its needed by the viewmodel
            //move this to chat service
            //TO DO move this to services           
            //setup room settings
            room.ChatRequestAccepted = false;
            room.ChatRequestRejected = true;
            //update the room
            _repository.Update(room);

                //made the sender the owner of the room so they can close it
                 room.Owners.Add(user);
                 _chatService.LeaveRoom(user, room);
                 //close the room since it was rejected
                 _chatService.CloseRoom(user, room);

                 _repository.CommitChanges();

                _notificationService.RejectChatRequest(user,toUser,messageText , room);


                //TO DO create a chat room and join the recipeint to it so he can see the message
                //notifiy that the room has been converted to chat request and send the updates to both         
               // _notificationService.SendChatRequest(user, toUser, messageText, room);
            

        }

        ///TO DO modify this code the do all the room stuff, send chat request is just for notification
        /// <summary>
        /// new
        /// </summary>
        /// <param name="user"></param>
        /// <param name="parts"></param>
        private void HandleAcceptChatRequest(ChatUser user, string[] parts)
        {

            //allow sending messages even if one user here
#if! (DISCONECTED || DEBUG)
            if (_repository.Users.Count() == 1)
            {
                throw new InvalidOperationException("You're the only person in here...");
            }
#endif
            if (parts.Length < 2 || String.IsNullOrWhiteSpace(parts[1]))
            {
                throw new InvalidOperationException("Who are you trying to reject a chat request from ?");
            }
            var toUserName = parts[1];
            ChatUser toUser = _repository.VerifyUser(toUserName);

            //Lets eliminate room from this equation, for when we fix it where no room is created unless accept happens ?
            //if (parts.Length < 3 || String.IsNullOrWhiteSpace(parts[1]))
            //{
            //    throw new InvalidOperationException("No room is avialable to chat inh");
            //}

            //build the message
            string messageText = String.Join(" ", parts.Skip(3)).Trim();
            //add sender username
            messageText = user.ScreenName  + " " + messageText;
            HashSet<string> urls;
            var transform = new TextTransform(_repository);
            messageText = transform.Parse(messageText);
            messageText = TextTransform.TransformAndExtractUrls(messageText, out urls);


            if (String.IsNullOrEmpty(messageText))
            {
                throw new InvalidOperationException(String.Format("What did you want to say to '{0}'.", toUser.Name));
            }

            //now do the room stuff

            var roomName = parts[2];

            //get room and open it it already exists
            ChatRoom room = _repository.VerifyAndOpenRoomIfExists(roomName, true);

            //hanlde if room was already created and closed , i.e a prior chat request
            if (room != null && room.Closed)
            {
                //TO DO use notification 
                throw new InvalidOperationException(String.Format("You have already refeused this chat request"));
            }

            //to do can probbaly take this out
            if (room == null)
            {

                //No need to verify room anymore 
                //room might have been created but it was not closed which means the last request was just not responded to
                //     room = _repository.VerifyRoom(roomName);
                //  }
                // else
                //  {
                //create the room here then if we have a new connection
                room = _chatService.AddRoom(user, roomName);
                //TO DO set the toipic with no notification
            }

            //allow messages to self in disconnected mode
#if! (DISCONECTED || DEBUG)
             if (toUser == user)
            {
                throw new InvalidOperationException("You can't Chat with yourself!");
            }
#endif
          
            //update room settings for an accepted request
            //move this to chat service
            //TO DO move this to services
            room.Type = "ChatRoom";
            room.ChatRequestAccepted = true;
            room.ChatRequestRejected = false ;

            _repository.Update(room);

            //notify the users that the chat request has been accepted etc
            //add the from user as the owner
            //only add if user is not already in the room
            if (ChatService.IsUserInRoom(room, user))
            {
                _notificationService.JoinRoom(user, room);
            }
            else
            {
                JoinRoom(user, room, null);
            }

            //only add if user is not already in the room
            if (ChatService.IsUserInRoom(room, toUser))
            {
                _notificationService.JoinRoom(toUser, room);
            }
            else
            {
                JoinRoom(toUser, room, null);
            }


            
            _repository.CommitChanges();

            //_notificationService.ConvertToChatRequestRoom(user, room);

            _notificationService.AcceptChatRequest (user, toUser, messageText, room);
            //TO DO create a chat room and join the recipeint to it so he can see the message
            //notifiy that the room has been converted to chat request and send the updates to both         
            // _notificationService.SendChatRequest(user, toUser, messageText, room);


        }


        private void HandleCreate(ChatUser user, string[] parts)
        {
            
            if (parts.Length > 2)
            {
               throw new InvalidOperationException("Room name cannot contain spaces.");

            }

            if (parts.Length == 1)
            {
                throw new InvalidOperationException("No room specified.");
            }

            string roomName = parts[1];
            if (String.IsNullOrWhiteSpace(roomName))
            {
                throw new InvalidOperationException("No room specified.");
            }

            ChatRoom room = _repository.VerifyAndOpenRoomIfExists(roomName, false);
            
            //commented out for now since if a room exists we just want to re-join it?
            //ChatRoom room = _repository.GetRoomByName(roomName);
            //if (room != null)
            //{
            //    throw new InvalidOperationException(String.Format("The room '{0}' already exists{1}",
            //        roomName,
            //        room.Closed ? " but it's closed" : String.Empty));
            //}
            if (room == null)
            {
                // Create the room, then join it
                room = _chatService.AddRoom(user, roomName);
            }
            else
            {

                // update the global variable for chat room as needed
                _roomName = room.Name;
            }

            //TO DO test this and update to make sure it works 
            //dont phyically join if user is in the room if so just 
            //do some sort of notification to the room
            if (ChatService.IsUserInRoom(room, user))
            {
                _notificationService.JoinRoom(user, room);
            }
            else
            {
                JoinRoom(user, room,null);
            }

            //JoinRoom(user, room, null);

            _repository.CommitChanges();
        }

        private void HandleJoin(ChatUser user, string[] parts)
        {
            if (parts.Length < 2)
            {
                throw new InvalidOperationException("Join which room?");
            }

            // Extract arguments
            string roomName = parts[1];
            string inviteCode = null;
            if (parts.Length > 2)
            {
                inviteCode = parts[2];
            }

            // Locate the room
           // ChatRoom room = _repository.VerifyRoom(roomName);
            ChatRoom room = _repository.VerifyRoom(roomName);

            if (ChatService.IsUserInRoom(room, user))
            {
                _notificationService.JoinRoom(user, room);
            }
            else
            {
                JoinRoom(user, room, inviteCode);
            }
        }

        private void JoinRoom(ChatUser user, ChatRoom room, string inviteCode)
        {
            _chatService.JoinRoom(user, room, inviteCode);

            _repository.CommitChanges();

            _notificationService.JoinRoom(user, room);
        }

        private void HandleGravatar(ChatUser user, string[] parts)
        {
            string email = String.Join(" ", parts.Skip(1));

            if (String.IsNullOrWhiteSpace(email))
            {
                throw new InvalidOperationException("Email was not specified!");
            }

            string hash = email.ToLowerInvariant().ToMD5();

            SetGravatar(user, hash);
        }

        private void SetGravatar(ChatUser user, string hash)
        {
            // Set user hash
            user.Hash = hash;

            _notificationService.ChangeGravatar(user);

            _repository.CommitChanges();
        }

        private void HandleRooms()
        {
            _notificationService.ShowRooms();
        }

        /// <summary>
        /// Used to reserve a nick name.
        /// /nick nickname - sets the user's name to nick name or creates a user with that name
        /// /nick nickname password - sets a password for the specified nick name (if the current user has that nick name)
        /// /nick nickname password newpassword - updates the password for the specified nick name (if the current user has that nick name)
        /// </summary>
        private void HandleNick(string[] parts)
        {
            if (parts.Length == 1)
            {
                throw new InvalidOperationException("No nick specified!");
            }

            string userName = parts[1];
            if (String.IsNullOrWhiteSpace(userName))
            {
                throw new InvalidOperationException("No nick specified!");
            }

            string password = null;
            if (parts.Length > 2)
            {
                password = parts[2];
            }

            string newPassword = null;
            if (parts.Length > 3)
            {
                newPassword = parts[3];
            }

            // See if there is a current user
            ChatUser user = _repository.GetUserById(_userId);

            if (user == null && String.IsNullOrEmpty(newPassword))
            {
                user = _repository.GetUserByName(userName);

                // There's a user with the name specified
                if (user != null)
                {
                    if (String.IsNullOrEmpty(password))
                    {
                        ChatService.ThrowPasswordIsRequired();
                    }
                    else
                    {
                        // If there's no user but there's a password then authenticate the user
                        _chatService.AuthenticateUser(userName, password);

                        // Add this client to the list of clients for this user
                        _chatService.AddClient(user, _clientId, _userAgent);

                        // Initialize the returning user
                        _notificationService.LogOn(user, _clientId);
                    }
                }
                else
                {
                    // If there's no user add a new one
                   // user = _chatService.AddUser(userName, _clientId, _userAgent, password);

                    // Notify the user that they're good to go!
                    _notificationService.OnUserCreated(user);
                }
            }
            else
            {
                if (String.IsNullOrEmpty(password))
                {
                    string oldUserName = user.Name;

                    // Change the user's name
                    _chatService.ChangeUserName(user, userName);

                    _notificationService.OnUserNameChanged(user, oldUserName, userName);
                }
                else
                {
                    // If the user specified a password, verify they own the nick
                    ChatUser targetUser = _repository.VerifyUser(userName);

                    // Make sure the current user and target user are the same
                    if (user != targetUser)
                    {
                        throw new InvalidOperationException("You can't set/change the password for a nickname you down own.");
                    }

                    if (String.IsNullOrEmpty(newPassword))
                    {
                        if (targetUser.HashedPassword == null)
                        {
                            _chatService.SetUserPassword(user, password);

                            _notificationService.SetPassword();
                        }
                        else
                        {
                            throw new InvalidOperationException("Use /nick [nickname] [oldpassword] [newpassword] to change and existing password.");
                        }
                    }
                    else
                    {
                        _chatService.ChangeUserPassword(user, password, newPassword);

                        _notificationService.ChangePassword();
                    }
                }
            }

            // Commit the changes
            _repository.CommitChanges();
        }

        private void HandleNudge(ChatUser user, string[] parts)
        {
            if (_repository.Users.Count() == 1)
            {
                throw new InvalidOperationException("You're the only person in here...");
            }

            var toUserName = parts[1];

            ChatUser toUser = _repository.VerifyUser(toUserName);

            if (toUser == user)
            {
                throw new InvalidOperationException("You can't nudge yourself!");
            }

            string messageText = String.Format("{0} nudged you", user);

            var betweenNudges = TimeSpan.FromSeconds(60);
            if (toUser.LastNudged.HasValue && toUser.LastNudged > DateTime.Now.Subtract(betweenNudges))
            {
                throw new InvalidOperationException(String.Format("User can only be nudged once every {0} seconds", betweenNudges.TotalSeconds));
            }

            toUser.LastNudged = DateTime.Now;
            _repository.CommitChanges();

            _notificationService.NugeUser(user, toUser);
        }

        private void HandleNudge(ChatRoom room, ChatUser user, string[] parts)
        {
            var betweenNudges = TimeSpan.FromMinutes(1);
            if (room.LastNudged == null || room.LastNudged < DateTime.Now.Subtract(betweenNudges))
            {
                room.LastNudged = DateTime.Now;
                _repository.CommitChanges();

                _notificationService.NudgeRoom(room, user);
            }
            else
            {
                throw new InvalidOperationException(String.Format("Room can only be nudged once every {0} seconds", betweenNudges.TotalSeconds));
            }
        }

        private void HandleNote(ChatUser user, string[] parts)
        {
            // We need to determine if we're either
            // 1. Setting a new Note.
            // 2. Clearing the existing Note.
            // If we have no optional text, then we need to clear it. Otherwise, we're storing it.
            bool isNoteBeingCleared = parts.Length == 1;
            user.Note = isNoteBeingCleared ? null : String.Join(" ", parts.Skip(1)).Trim();

            ChatService.ValidateNote(user.Note);

            _notificationService.ChangeNote(user);

            _repository.CommitChanges();
        }

        private void HandleAfk(ChatUser user, string[] parts)
        {
            string message = String.Join(" ", parts.Skip(1)).Trim();

            ChatService.ValidateNote(message);

            user.AfkNote = String.IsNullOrWhiteSpace(message) ? null : message;
            user.IsAfk = true;

            _notificationService.ChangeNote(user);

            _repository.CommitChanges();
        }

        private void HandleFlag(ChatUser user, string[] parts)
        {
            if (parts.Length <= 1)
            {
                // Clear the flag.
                user.Flag = null;
            }
            else
            {
                // Set the flag.
                string isoCode = String.Join(" ", parts[1]).ToLowerInvariant();
                ChatService.ValidateIsoCode(isoCode);
                user.Flag = isoCode;
            }

            _notificationService.ChangeFlag(user);

            _repository.CommitChanges();
        }
    }
}