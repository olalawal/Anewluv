

using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Shell.MVC2.Services.Chat;



using System.Diagnostics;
using System.Globalization;
using Shell.MVC2.Interfaces;


using Newtonsoft.Json;
using Shell.MVC2.Infastructure.Chat;
using Shell.MVC2.Domain.Entities.Anewluv.Chat.ViewModels;
using Shell.MVC2.Services.Contracts;
using Shell.MVC2.Domain.Entities.Anewluv.Chat;

using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR;
using Shell.MVC2.Services.Chat.SignalR;


namespace Shell.MVC2.Services.Chat
{
    //, IControllerFactory 

    [HubName("chatHub")]
    public class ChatHub : Hub,   IChatNotificationService
{

    private readonly IChatRepository  _repository;
    private readonly IChatService _service;
  //  private readonly IResourceProcessor _resourceProcessor;
    //private readonly IApplicationSettings _settings;
    private static readonly Version _version = typeof(ChatHub).Assembly.GetName().Version;

    public ChatHub(IChatService service, IChatRepository  repository)
    {
        _service = service;
        _repository = repository;
    }

        //public vars
    private string UserAgent
    {
        get
        {
            if (Context.Headers != null)
            {
                return Context.Headers["User-Agent"];
            }
            return null;
        }
    }
    private bool OutOfSync
    {
        get
        {
            string version = Clients.Caller.version;
            return String.IsNullOrEmpty(version) ||
                    new Version(version) != _version;
        }
    }

    public void Sendtest()
    {
        // Call the broadcastMessage method to update clients. 
        Clients.All.broadcastMessage("","");


    }

    #region "Custom non jabra methods"
    //public Task Startup()
    //{

    //    var clientId = this.Context.User.Identity.Name;

    //    // clients[clientId].method();             
    //    Clients.Caller.guid = clientId;

    //    _service.AddUser(clientId, Clients.Caller.guid, "");

    //    return null;//Caller.guid;
    //}

    //public void Send(string message, string UserName, string roomGuid)
    //{
    //    //find the client ID'
    //    //    string id = this.Context.ClientId;
    //    string id2 = this.Context.User.Identity.Name;
    //    string callerID = Clients.Caller.id;

    //    var msg = new ChatMessageModel
    //    {
    //        Message = message,
    //        CreationDate = DateTime.UtcNow,

    //    };


    //    //Call the addMessage method on all clients. 
    //    Clients.addMessage(msg);

    //    // Invoke addMessage on all clients in group foo
    //    Clients[roomGuid].addMessage(message);


    //}
    ////method callable from client test
    //public string Send(string data)
    //{
    //    return data;
    //}    

    //public Task Disconnect()
    //{
    //    // Query the database to find the user by it's client id.
    //    // var user = db.Users.Where(u => u.ConnectionId == Context.ConnectionId);
    //    return null;
    //}

    //all private conv's take place in rooms which do not have names buy a random GUID
    //public void JoinRoom(string RoomGuid)
    //{

    //    AddToGroup(RoomGuid);
    //}
    #endregion

        /// <summary>
        /// handles intial join to chat service
        /// assumed user was already added...
        /// </summary>
        /// <returns></returns>
    public bool  Join(string UserId)
     {
         
         SetVersion();
        //Startup();
         
         // Get the client state
         ClientState clientState = GetClientState();


         // Try to get the user from the client state
         //ChatUser user = _repository.GetUserById(clientState.UserId);
         ChatUser user = _repository.GetUserById(UserId);

         // Threre's no user being tracked
         if (user == null)
         {
             return false;
         }

         // Migrate all users to use new auth
        
         // Update some user values
         _service.UpdateActivity(user, Context.ConnectionId, UserAgent);
         _repository.CommitChanges();

         OnUserInitialize(clientState, user);

         return true;
      }   
    private void SetVersion()
    {
        // Set the version on the client
        Clients.Caller.version = typeof(ChatHub).Assembly.GetName().Version.ToString();
    }
    public bool CheckStatus()
    {
        bool outOfSync = OutOfSync;

        SetVersion();

        string id = Clients.Caller.id;

        ChatUser user = _repository.VerifyUserId(id);

        // Make sure this client is being tracked
        _service.AddClient(user, Context.ConnectionId, UserAgent);

        var currentStatus = (userstatusEnum )user.Status;

        if (currentStatus == userstatusEnum.Offline)
        {
            // Mark the user as inactive
            user.Status = (int)userstatusEnum.Inactive;
            _repository.CommitChanges();

            // If the user was offline that means they are not in the user list so we need to tell
            // everyone the user is really in the room
            var userViewModel = new UserViewModel(user);

            foreach (var room in user.Rooms)
            {
                var isOwner = user.OwnedRooms.Contains(room);

                // Tell the people in this room that you've joined
                 Clients.Group(room.Name).addUser(userViewModel, room.Name, isOwner).Wait();

                // Update the room count
                OnRoomChanged(room);

                // Add the caller to the group so they receive messages
                 Groups.Add(Context.ConnectionId, room.Name).Wait();
            }
        }

        return outOfSync;
    }
    private void OnUserInitialize(ClientState clientState, ChatUser user)
    {
        // Update the active room on the client (only if it's still a valid room)
        if (user.Rooms.Any(room => room.Name.Equals(clientState.ActiveRoom, StringComparison.OrdinalIgnoreCase)))
        {
            // Update the active room on the client (only if it's still a valid room)
            Clients.Caller.activeRoom = clientState.ActiveRoom;
        }

        LogOn(user, Context.ConnectionId);
    }
    public bool Send(string content, string roomName)
    {
        var message = new ClientMessage
        {
            Content = content,
            Id = Guid.NewGuid().ToString("d"),
            Room = roomName
        };

        return Send(message);
    }
    public bool Send(ClientMessage message)
    {
        bool outOfSync = OutOfSync;

        SetVersion();

        // Sanitize the content (strip and bad html out)
        message.Content = message.Content;//     HttpUtility.HtmlEncode(message.Content);

        // See if this is a valid command (starts with /)
        if (TryHandleCommand(message.Content, message.Room))
        {
            return outOfSync;
        }

        string id = Clients.Caller.id;

        ChatUser user = _repository.VerifyUserId(id);
        ChatRoom room = _repository.VerifyUserRoom(user, message.Room);

        // Update activity *after* ensuring the user, this forces them to be active
        UpdateActivity(user, room);

        HashSet<string> links;
        var messageText = ParseChatMessageText(message.Content, out links);

        ChatMessage chatMessage = _service.AddMessage(user, room, message.Id, messageText);


        var messageViewModel = new MessageViewModel(chatMessage);
        Clients.Group(room.Name).addMessage(messageViewModel, room.Name);

        _repository.CommitChanges();

        string clientMessageId = chatMessage.Id;

        // Update the id on the message
        chatMessage.Id = Guid.NewGuid().ToString("d");
        _repository.CommitChanges();

        if (!links.Any())
        {
            return outOfSync;
        }

      //  ProcessUrls(links, room.Name, clientMessageId, chatMessage.Id);

        return outOfSync;
    }
    private string ParseChatMessageText(string content, out HashSet<string> links)
    {
        var textTransform = new TextTransform(_repository);
        string message = textTransform.Parse(content);
        return TextTransform.TransformAndExtractUrls(message, out links);
    }
    public UserViewModel GetUserInfo()
    {
        string id = Clients.Caller.id;

        ChatUser user = _repository.VerifyUserId(id);

        return new UserViewModel(user);
    }
    public string GetUserName()
    {
        string id = Clients.Caller.id;

        ChatUser user = _repository.VerifyUserId(id);

        return new UserViewModel(user).Name;
    }
    public string GetUserScreenName()
    {
        string id = Clients.Caller.id;

        ChatUser user = _repository.VerifyUserId(id);

        return new UserViewModel(user).ScreenName ;
    }
    public Task Disconnect()
    {
        DisconnectClient(Context.ConnectionId);

        return null;
    }
    public object GetCommands()
    {
        return new[] {
                new { Name = "help", Description = "Type /help to show the list of commands" },
                new { Name = "nick", Description = "Type /nick [user] [password] to create a user or change your nickname. You can change your password with /nick [user] [oldpassword] [newpassword]" },
                new { Name = "join", Description = "Type /join [room] [inviteCode] - to join a channel of your choice. If it is private and you have an invite code, enter it after the room name" },
                new { Name = "create", Description = "Type /create [room] to create a room" },
                new { Name = "me", Description = "Type /me 'does anything'" },
                new { Name = "msg", Description = "Type /msg @nickname (message) to send a private message to nickname. @ is optional." },
                new { Name = "leave", Description = "Type /leave to leave the current room. Type /leave [room name] to leave a specific room." },
                new { Name = "rooms", Description = "Type /rooms to show the list of rooms" },
                new { Name = "where", Description = "Type /where [name] to the rooms that user is in" },
                new { Name = "who", Description = "Type /who to show a list of all users, /who [name] to show specific information about that user" },
                new { Name = "list", Description = "Type /list (room) to show a list of users in the room" },
                new { Name = "gravatar", Description = "Type /gravatar [email] to set your gravatar." },
                new { Name = "nudge", Description = "Type /nudge to send a nudge to the whole room, or \"/nudge @nickname\" to nudge a particular user. @ is optional." },
                new { Name = "kick", Description = "Type /kick [user] to kick a user from the room. Note, this is only valid for owners of the room." },
                new { Name = "logout", Description = "Type /logout - To logout from this client (chat cookie will be removed)." },
                new { Name = "addowner", Description = "Type /addowner [user] [room] - To add an owner a user as an owner to the specified room. Only works if you're an owner of that room." },
                new { Name = "removeowner", Description = "Type /removeowner [user] [room] - To remove an owner from the specified room. Only works if you're the creator of that room." },
                new { Name = "lock", Description = "Type /lock [room] - To make a room private. Only works if you're the creator of that room." },
                new { Name = "close", Description = "Type /close [room] - To close a room. Only works if you're an owner of that room." },
                new { Name = "allow", Description = "Type /allow [user] [room] - To give a user permission to a private room. Only works if you're an owner of that room." },
                new { Name = "unallow", Description = "Type /unallow [user] [room] - To revoke a user's permission to a private room. Only works if you're an owner of that room." },
                new { Name = "invitecode", Description = "Type /invitecode - To show the current invite code" },
                new { Name = "resetinvitecode", Description = "Type /resetinvitecode - To reset the current invite code. This will render the previous invite code invalid" },
                new { Name = "note", Description = "Type /note - To set a note shown via a paperclip icon next to your name, with the message appearing when you hover over it."},
                new { Name = "afk", Description = "Type /afk - (aka. Away From Keyboard). To set a temporary note shown via a paperclip icon next to your name, with the message appearing when you hover over it. This note will disappear when you first resume typing."},
                new { Name = "flag", Description = "Type /flag [Iso 3366-2 Code] - To show a small flag which represents your nationality. Eg. /flag US for a USA flag. ISO Reference Chart: http://en.wikipedia.org/wiki/ISO_3166-1_alpha-2 (Apologies to people with dual citizenship). "},
                new { Name = "topic", Description = "Type /topic [topic] to set the room topic. Type /topic to clear the room's topic." },
                new { Name = "chatreq", Description = "Type /chatreq @nickname (message) to send a private chatrequest to nickname. @ is optional." }
            };
    }   
    private void OnRoomChanged(ChatRoom room)
     {
         var roomViewModel = new RoomViewModel
         {
             Name = room.Name,
             Private = room.Private,
              Type =room.Type ,
             ChatRequestAccepted = room.ChatRequestAccepted.GetValueOrDefault() ,
             ChatRequestRejected = room.ChatRequestRejected.GetValueOrDefault()
         };

         // Update the room count
         Clients.All.updateRoomCount(roomViewModel,_repository.GetOnlineUsersInRoom(room).Count () ) ;// room.Users.Online().Count());
     }             
    private ClientState GetClientState()
     {
         // New client state
         var jabbrState = GetCookieValue("jabbr.state");

         ClientState clientState = null;

         if (String.IsNullOrEmpty(jabbrState))
         {
             clientState = new ClientState();
         }
         else
         {
             clientState = JsonConvert.DeserializeObject<ClientState>(jabbrState);
         }

         // Read the id from the caller if there's no cookie
         clientState.UserId = clientState.UserId ?? Clients.Caller.guid;

        // string id = Clients.Caller.id;

         return clientState;
     }
    private string GetCookieValue(string key)
     {
         var request = HttpContext.Current.Request;

         if (request.Cookies.Count > 0)
         {
             string value = request.Cookies[key].ToString();
             return value != null ? HttpUtility.UrlDecode(value) : null;
         }

         return null;
     }
    public IEnumerable<RoomViewModel> GetRooms()
     {
         string id = Clients.Caller.id;
         ChatUser user = _repository.VerifyUserId(id);

         var rooms = _repository.GetAllowedRooms(user).Select(r => new RoomViewModel
         {
             Name = r.Name,
             Count = r.Users.Count(u => u.Status != (int)userstatusEnum.Offline),
             Private = r.Private,
             Type = r.Type ,
             ChatRequestRejected = r.ChatRequestRejected.GetValueOrDefault(),
             ChatRequestAccepted = r.ChatRequestAccepted.GetValueOrDefault() 
         });

         return rooms;
     }
    public IEnumerable<MessageViewModel> GetPreviousMessages(string messageId)
     {
         var previousMessages = (from m in _repository.GetPreviousMessages(messageId)
                                 orderby m.When descending
                                 select m).Take(100);


         return previousMessages.AsEnumerable()
                                .Reverse()
                                .Select(m => new MessageViewModel(m));
     }
    public RoomViewModel GetRoomInfo(string roomName)
     {
         if (String.IsNullOrEmpty(roomName))
         {
             return null;
         }
         //3/24/2013 we will also update this to get rooms in reverse if they exits
                  
         ChatRoom room = _repository.GetRoomIfCreatedBefore(roomName);
                  

         if (room == null)
         {
             return null;
         }

         var recentMessages = (from m in _repository.GetMessagesByRoom(roomName)
                               orderby m.When descending
                               select m).Take(30);

         return new RoomViewModel
         {
             Name = room.Name,
             Users = from u in  _repository.GetOnlineUsersInRoom(room)// room.Users.Online()
                     select new UserViewModel(u),
             Owners = from u in _repository.GetOnlineOwnersInRoom(room) // room.Owners.Online()
                      select u.Name,
             RecentMessages = recentMessages.AsEnumerable().Reverse().Select(m => new MessageViewModel(m)),
             Topic = room.Topic ,  
             TopicScreenName = room.TopicScreenName,
             TopicStarter = room.TopicStarter,
             TopicStarterScreenName = room.TopicStarterScreenName ,
             Type = room.Type ,
             ChatRequestRejected = room.ChatRequestRejected.GetValueOrDefault(),
             ChatRequestAccepted= room.ChatRequestAccepted.GetValueOrDefault()
         };
     }
    private string ConvertUrlsAndRoomLinks(string message)
     {
         TextTransform textTransform = new TextTransform(_repository);
         message = textTransform.ConvertHashtagsToRoomLinks(message);
         HashSet<string> urls;
         return TextTransform.TransformAndExtractUrls(message, out urls);
     }
    private void OnUpdateActivity(ChatUser user, ChatRoom room)
     {
         var userViewModel = new UserViewModel(user);
         Clients.Group(room.Name).updateActivity(userViewModel, room.Name);
     }
    private void LeaveRoom(ChatUser user, ChatRoom room)
     {
         var userViewModel = new UserViewModel(user);
         Clients.Group(room.Name).leave(userViewModel, room.Name).Wait();

         //var test = Clients.Group(room.Name);
         

         foreach (var client in user.ConnectedClients)
         {
           Groups.Remove  (client.Id, room.Name).Wait();
         }


         OnRoomChanged(room);
     }
     // TODO: Deprecate
    public void Typing()
     {
         string roomName = Clients.Caller.activeRoom;

         Typing(roomName);
     }
    public void Typing(string roomName)
     {
         string id = Clients.Caller.id;
         ChatUser user = _repository.GetUserById(id);

         if (user == null)
         {
             return;
         }

         ChatRoom room = _repository.VerifyUserRoom(user, roomName);

         UpdateActivity(user, room);
         var userViewModel = new UserViewModel(user);
         Clients.Group(room.Name).setTyping(userViewModel, room.Name);
     }
    private void LogOn(ChatUser user, string clientId)
     {
         // Update the client state
         Clients.Caller.id = user.Id;
         Clients.Caller.name = user.Name;
         Clients.Caller.hash = user.Hash;

         //test code : get all the avialble rooms
         

         var userViewModel = new UserViewModel(user);
         var rooms = new List<RoomViewModel>();

         foreach (var room in user.Rooms)
         {
             var isOwner = user.OwnedRooms.Contains(room);

             // Tell the people in this room that you've joined
             Clients.Group(room.Name).addUser(userViewModel, room.Name, isOwner).Wait();

             // Update the room count
             OnRoomChanged(room);
            
             // Add the caller to the group so they receive messages
            Groups.Add (clientId, room.Name).Wait();

             // Add to the list of room names
             rooms.Add(new RoomViewModel
             {
                 Name = room.Name,
                 Private = room.Private,
                 Type = room.Type,
                 ChatRequestAccepted=room.ChatRequestAccepted.GetValueOrDefault(),
                 ChatRequestRejected= room.ChatRequestRejected.GetValueOrDefault()
             });
         }

         // Initialize the chat with the rooms the user is in
         Clients.Caller.logOn(rooms);
     }
    private void UpdateActivity(ChatUser user, ChatRoom room)
     {
         UpdateActivity(user);

         OnUpdateActivity(user, room);
     }
    private void UpdateActivity(ChatUser user)
     {
         _service.UpdateActivity(user, Context.ConnectionId, UserAgent);

         _repository.CommitChanges();
     }
    //private void ProcessUrls(IEnumerable<string> links, ChatRoom room, ChatMessage chatMessage)
     //{
     //    // Use this id when talking to the client to update the message content
     //    string id = chatMessage.Id;

     //    // REVIEW: is this safe to do? We're holding on to this instance 
     //    // when this should really be a fire and forget.
     //    var contentTasks = links.Select(_resourceProcessor.ExtractResource).ToArray();
     //    Task.Factory.ContinueWhenAll(contentTasks, tasks =>
     //    {
     //        foreach (var task in tasks)
     //        {
     //            if (task.IsFaulted)
     //            {
     //                Trace.TraceError(task.Exception.GetBaseException().Message);
     //                continue;
     //            }

     //            if (task.Result == null || String.IsNullOrEmpty(task.Result.Content))
     //            {
     //                continue;
     //            }

     //            // Try to get content from each url we're resolved in the query
     //            string extractedContent = "<p>" + task.Result.Content + "</p>";

     //            // If we did get something, update the message and notify all clients
     //            chatMessage.Content += extractedContent;

     //            // Notify the room
     //            Clients.Group(room.Name).addMessageContent(id, extractedContent, room.Name);

     //            // Commit the changes
     //            _repository.CommitChanges();
     //        }
     //    });
     //}

      //  TO DO add commands down the line   
    private bool TryHandleCommand(string command, string room)
     {
         string clientId = Context.ConnectionId;
         string userId = Clients.Caller.id;

         var commandManager = new CommandManager(clientId, UserAgent, userId, room, _service, _repository, this);
         return commandManager.TryHandleCommand(command);
         //return false;
     }
    private void DisconnectClient(string clientId)
     {
         ChatUser user = _service.DisconnectClient(clientId);

         // There's no associated user for this client id
         if (user == null)
         {
             return;
         }

         // The user will be marked as offline if all clients leave
         if (user.Status == (int)userstatusEnum.Offline)
         {
             foreach (var room in user.Rooms)
             {
                 var userViewModel = new UserViewModel(user);

                 Clients.Group(room.Name).leave(userViewModel, room.Name).Wait();

                 OnRoomChanged(room);
             }
         }
     }



     #region "Notification Service Members"

     void IChatNotificationService.LogOn(ChatUser user, string clientId)
     {
         LogOn(user, clientId);
     }
     void IChatNotificationService.ChangePassword()
     {
         Clients.Caller.changePassword();
     }
     void IChatNotificationService.SetPassword()
     {
         Clients.Caller.setPassword();
     }
     void IChatNotificationService.KickUser(ChatUser targetUser, ChatRoom room)
     {
         foreach (var client in targetUser.ConnectedClients)
         {
             // Kick the user from this room
             Clients.Client(client.Id).kick(room.Name);

             // Remove the user from this the room group so he doesn't get the leave message
            Groups.Remove (client.Id, room.Name).Wait();
             
         }

         // Tell the room the user left
         LeaveRoom(targetUser, room);
     }
     void IChatNotificationService.OnUserCreated(ChatUser user)
     {
         // Set some client state
         Clients.Caller.name = user.Name;
         Clients.Caller.id = user.Id;
         Clients.Caller.hash = user.Hash;

         // Tell the client a user was created
         Clients.Caller.userCreated();
     }

     /// <summary>
     ///  added option to determin if the room being joined is a chatrequest room
     ///  3/25/2012 updated to flag for chat request accepted
     /// </summary>
     /// <param name="user"></param>
     /// <param name="room"></param>
     void IChatNotificationService.JoinRoom(ChatUser user, ChatRoom room)
     {
         var userViewModel = new UserViewModel(user);
         
         //TO do we probbaly dont need chat reqyuest item
         var roomViewModel = new RoomViewModel
         {
             Name = room.Name,
             Private = room.Private,
             ChatRequestRejected = room.ChatRequestRejected.GetValueOrDefault(),
             ChatRequestAccepted = room.ChatRequestAccepted.GetValueOrDefault(),
             Topic = room.Topic , 
             TopicScreenName = room.TopicScreenName ,
             TopicStarter = room.TopicStarter ,
             TopicStarterScreenName = room.TopicStarterScreenName 
         };

         var isOwner = user.OwnedRooms.Contains(room);


         // Tell all clients to join this room
         foreach (var client in user.ConnectedClients)
         {
             Clients.Client(client.Id).joinRoom(roomViewModel);
         }



         // Tell the people in this room that you've joined
         Clients.Group(room.Name).addUser(userViewModel, room.Name, isOwner).Wait();

         // Notify users of the room count change
         OnRoomChanged(room);

         foreach (var client in user.ConnectedClients)
         {
             // Add the caller to the group so they receive messages
             Groups.Add(client.Id, room.Name).Wait();
         }
     }
     void IChatNotificationService.AllowUser(ChatUser targetUser, ChatRoom targetRoom)
     {
         foreach (var client in targetUser.ConnectedClients)
         {
             // Tell this client it's an owner
             Clients.Client(client.Id).allowUser(targetRoom.Name);
         }

         // Tell the calling client the granting permission into the room was successful
         Clients.Caller.userAllowed(targetUser.Name, targetRoom.Name);
     }
     void IChatNotificationService.UnallowUser(ChatUser targetUser, ChatRoom targetRoom)
     {
         // Kick the user from the room when they are unallowed
         ((IChatNotificationService)this).KickUser(targetUser, targetRoom);

         foreach (var client in targetUser.ConnectedClients)
         {
             // Tell this client it's an owner
             Clients.Client(client.Id).unallowUser(targetRoom.Name);
         }

         // Tell the calling client the granting permission into the room was successful
         Clients.Caller.userUnallowed(targetUser.Name, targetRoom.Name);
     }
     void IChatNotificationService.AddOwner(ChatUser targetUser, ChatRoom targetRoom)
     {
         foreach (var client in targetUser.ConnectedClients)
         {
             // Tell this client it's an owner
             Clients.Client(client.Id).makeOwner(targetRoom.Name);
         }

         var userViewModel = new UserViewModel(targetUser);

         // If the target user is in the target room.
         // Tell everyone in the target room that a new owner was added
         if (ChatService.IsUserInRoom(targetRoom, targetUser))
         {
             Clients.Group(targetRoom.Name).addOwner(userViewModel, targetRoom.Name);
         }

         // Tell the calling client the granting of ownership was successful
         Clients.Caller.ownerMade(targetUser.Name, targetRoom.Name);
     }
     void IChatNotificationService.RemoveOwner(ChatUser targetUser, ChatRoom targetRoom)
     {
         foreach (var client in targetUser.ConnectedClients)
         {
             // Tell this client it's no longer an owner
             Clients.Client(client.Id).demoteOwner(targetRoom.Name);
         }

         var userViewModel = new UserViewModel(targetUser);

         // If the target user is in the target room.
         // Tell everyone in the target room that the owner was removed
         if (ChatService.IsUserInRoom(targetRoom, targetUser))
         {
             Clients.Group(targetRoom.Name).removeOwner(userViewModel, targetRoom.Name);
         }

         // Tell the calling client the removal of ownership was successful
         Clients.Caller.ownerRemoved(targetUser.Name, targetRoom.Name);
     }
     void IChatNotificationService.ChangeGravatar(ChatUser user)
     {
         Clients.Caller.hash = user.Hash;

         // Update the calling client
         foreach (var client in user.ConnectedClients)
         {
             Clients.Client(client.Id).gravatarChanged();
         }

         // Create the view model
         var userViewModel = new UserViewModel(user);

         // Tell all users in rooms to change the gravatar
         foreach (var room in user.Rooms)
         {
             Clients.Group(room.Name).changeGravatar(userViewModel, room.Name);
         }
     }
     void IChatNotificationService.OnSelfMessage(ChatRoom room, ChatUser user, string content)
     {
         Clients.Group(room.Name).sendMeMessage(user.Name, content, room.Name);
     }
     void IChatNotificationService.SendPrivateMessage(ChatUser fromUser, ChatUser toUser, string messageText)
     {
         // Send a message to the sender and the sendee
         foreach (var client in fromUser.ConnectedClients)
         {
             Clients.Client(client.Id).sendPrivateMessage(fromUser.Name, toUser.Name, messageText);
         }

         foreach (var client in toUser.ConnectedClients)
         {
             Clients.Client(client.Id).sendPrivateMessage(fromUser.Name, toUser.Name, messageText);
         }
     }

     void IChatNotificationService.SendChatRequest(ChatUser fromUser, ChatUser toUser, string messageText, ChatRoom room)
     {

        // var userViewModel = new UserViewModel(fromUser);
         //since reqest has not been set yet sent status's both to false
         //dont know if we need two status?
         var roomViewModel = new RoomViewModel
         {
             Name = room.Name,
             Private = room.Private, 
             Topic = room.Topic,
             TopicStarter = room.TopicStarter ,
             TopicScreenName =toUser.ScreenName,
             TopicStarterScreenName = fromUser.ScreenName ,
             Type = room.Type ,
             ChatRequestRejected = room.ChatRequestRejected.GetValueOrDefault(),
             ChatRequestAccepted = room.ChatRequestAccepted.GetValueOrDefault()
         };


         //TO DO make sure all the users are connected, if they are not send it as an email
         //  (var client in fromUser.ConnectedClients)

         // Send a nudge message to the sender and the sendee
         foreach (var client in fromUser.ConnectedClients)
         {
             Clients.Client(client.Id).sendPrivateMessage(fromUser.Name, toUser.Name, messageText, roomViewModel);

            // Clients.Client(client.Id).sentChatRequestNotification(toUser.Name, messageText, room);
         }

         foreach (var client in toUser.ConnectedClients)
         {
             Clients.Client(client.Id).sendPrivateMessage(fromUser.Name, toUser.Name, messageText, roomViewModel);
         }

     }

     void IChatNotificationService.RejectChatRequest(ChatUser fromUser, ChatUser toUser, string messageText, ChatRoom room)
     {
        // var userViewModel = new UserViewModel(fromUser);

         //TO Change this code to not use the Topic stuff to sture the user they are chatting to
         var roomViewModel = new RoomViewModel
         {
             Name = room.Name,
             Private = room.Private,
             Topic = room.Topic,
             TopicStarter = room.TopicStarter,
             TopicScreenName = toUser.ScreenName,
             TopicStarterScreenName = fromUser.ScreenName,
             Type = room.Type,
             ChatRequestRejected = room.ChatRequestRejected.GetValueOrDefault()  ,              
             ChatRequestAccepted = room.ChatRequestAccepted.GetValueOrDefault()
         };

        
       

         //TO DO make sure all the users are connected, if they are not send it as an email
         //  (var client in fromUser.ConnectedClients)

         // Send a nudge message to the sender and the sendee
         foreach (var client in fromUser.ConnectedClients)
         {
             Clients.Client(client.Id).sendPrivateMessage(fromUser.Name, toUser.Name, messageText, roomViewModel);

             // Clients.Client(client.Id).sentChatRequestNotification(toUser.Name, messageText, room);
         }

         foreach (var client in toUser.ConnectedClients)
         {
             Clients.Client(client.Id).sendPrivateMessage(fromUser.Name, toUser.Name, messageText, roomViewModel);
         }

     }

     void IChatNotificationService.AcceptChatRequest(ChatUser fromUser, ChatUser toUser, string messageText, ChatRoom room)
     {
         // var userViewModel = new UserViewModel(fromUser);

         //change the room type to a chat room as well as change the accept flag to true and rejected to false.
         //accept also creates the room so make sure the names are set too i.e topic and sender name
         var roomViewModel = new RoomViewModel
         {
             Name = room.Name,
             Private = room.Private,
             Topic = fromUser.Name,
             TopicStarter = toUser.Name,
             TopicScreenName = toUser.ScreenName,
             TopicStarterScreenName = fromUser.ScreenName,
             Type = room.Type ,
             ChatRequestRejected = room.ChatRequestRejected.GetValueOrDefault()   ,
             ChatRequestAccepted = room.ChatRequestAccepted .GetValueOrDefault()
           
         };

               

         //TO DO make sure all the users are connected, if they are not send it as an email
         //  (var client in fromUser.ConnectedClients)

         // Send a nudge message to the sender and the sendee
         foreach (var client in fromUser.ConnectedClients)
         {
             Clients.Client(client.Id).sendPrivateMessage(fromUser.Name, toUser.Name, messageText, roomViewModel);

             // Clients.Client(client.Id).sentChatRequestNotification(toUser.Name, messageText, room);
         }

         foreach (var client in toUser.ConnectedClients)
         {
             Clients.Client(client.Id).sendPrivateMessage(fromUser.Name, toUser.Name, messageText, roomViewModel);
         }

     }

     void IChatNotificationService.PostNotification(ChatRoom room, ChatUser user, string message)
     {
         foreach (var client in user.ConnectedClients)
         {
             Clients.Client(client.Id).postNotification(message, room.Name);
         }
     }

     void IChatNotificationService.ListRooms(ChatUser user)
     {
         string userId = Clients.Caller.id;
         var userModel = new UserViewModel(user);

         Clients.Caller.showUsersRoomList(userModel, user.Rooms.Where(z=>z.AllowedUsers.Contains(user)).Select(r=>r.Name)); //Allowed(userId).Select(r => r.Name));

          // Clients.Caller.showUsersRoomList(userModel,_repository.GetOnlineAllowedUsersInRoom(user.rooms).Where(u=>user.Id == userId).Select(r=>r.Name);
     }

     void IChatNotificationService.ListUsers()
     {
         var users = _repository.GetOnlineUsers().Select(s => s.Name).OrderBy(s => s);
         //_repository.Users.Online().Select(s => s.Name).OrderBy(s => s);
         Clients.Caller.listUsers(users);
     }
     void IChatNotificationService.ListUsers(IEnumerable<ChatUser> users)
     {
         Clients.Caller.listUsers(users.Select(s => s.Name));
     }
     void IChatNotificationService.ListUsers(ChatRoom room, IEnumerable<string> names)
     {
         Clients.Caller.showUsersInRoom(room.Name, names);
     }
     void IChatNotificationService.LockRoom(ChatUser targetUser, ChatRoom room)
     {
         var userViewModel = new UserViewModel(targetUser);

         // Tell the room it's locked
         Clients.All.lockRoom(userViewModel, room.Name);

         // Tell the caller the room was successfully locked
         Clients.Caller.roomLocked(room.Name);

         // Notify people of the change
         OnRoomChanged(room);
     }
     //void IChatNotificationService.ConvertToChatRequestRoom(ChatUser targetUser, ChatRoom room)
     //{
     //    var userViewModel = new UserViewModel(targetUser);

     //    // Tell the room it's locked
     //    Clients.convertToChatRequestRoom(userViewModel, room.Name);

     //    // Tell the caller the room was successfully locked
     //    Clients.Caller.chatRequestRoomCreated(room.Name);

     //    // Notify people of the change
     //    OnRoomChanged(room);
     //}

     void IChatNotificationService.SendDialogNotification(ChatUser targetUser, string message)
     {
         var userViewModel = new UserViewModel(targetUser);

         foreach (var client in targetUser.ConnectedClients)
         {
             Clients.Client(client.Id).postNotification(message, "");
         }


         //  foreach (var client in targetUser.ConnectedClients)
         // {
         //   Clients.Client(client.Id).sendDialognotification(targetUser.Name, message);
         // }

         // Tell the room it's locked
         //Clients.convertToChatRequestRoom(userViewModel, room.Name);

         // Tell the caller the room was successfully locked
         // Clients.Caller.chatRequestRoomCreated(room.Name);

         // Notify people of the change
         //  OnRoomChanged(room);
     }

     void IChatNotificationService.CloseRoom(IEnumerable<ChatUser> users, ChatRoom room)
     {
         // Kick all people from the room.
         foreach (var user in users)
         {
             foreach (var client in user.ConnectedClients)
             {
                 // Kick the user from this room
                 Clients.Client(client.Id).kick(room.Name);

                 // Remove the user from this the room group so he doesn't get the leave message
                 Groups.Remove(client.Id, room.Name).Wait();

              
             }
         }

         // Tell the caller the room was successfully closed.
         Clients.Caller.roomClosed(room.Name);
     }
     void IChatNotificationService.LogOut(ChatUser user, string clientId)
     {
         DisconnectClient(clientId);

         var rooms = user.Rooms.Select(r => r.Name);

         Clients.Caller.logOut(rooms);
     }
     void IChatNotificationService.ShowUserInfo(ChatUser user)
     {
         string userId = Clients.Caller.id;
        // _repository.GetAllowedRooms(user)
         
         Clients.Caller.showUserInfo(new 
         {
             Name = user.Name,
             OwnedRooms = user.OwnedRooms.Where(r=>!r.Closed).Select(r=>r.Name) ,
                 //.Allowed(userId),
                 //.Where(r => !r.Closed),
                 //.Select(r => r.Name),
             Status = ((userstatusEnum)user.Status).ToString(),
             LastActivity = user.LastActivity,
             
             IsAfk = user.IsAfk,
             AfkNote = user.AfkNote,
             Note = user.Note,
             Rooms = _repository.GetAllowedRooms(user) // (userId).Select(r => r.Name)
             
         });
     }
     void IChatNotificationService.ShowHelp()
     {
         Clients.Caller.showCommands();
     }
     void IChatNotificationService.ShowRooms()
     {
         Clients.Caller.showRooms(GetRooms());
     }
     void IChatNotificationService.NugeUser(ChatUser user, ChatUser targetUser)
     {
         // Send a nudge message to the sender and the sendee
         foreach (var client in targetUser.ConnectedClients)
         {
             Clients.Client(client.Id).nudge(user.Name, targetUser.Name);
         }

         foreach (var client in user.ConnectedClients)
         {
             Clients.Client(client.Id).sendPrivateMessage(user.Name, targetUser.Name, "nudged " + targetUser.Name);
         }
     }
     void IChatNotificationService.NudgeRoom(ChatRoom room, ChatUser user)
     {
         Clients.Group(room.Name).nudge(user.Name);
     }
     void IChatNotificationService.LeaveRoom(ChatUser user, ChatRoom room)
     {
         LeaveRoom(user, room);
     }
     void IChatNotificationService.OnUserNameChanged(ChatUser user, string oldUserName, string newUserName)
     {
         // Create the view model
         var userViewModel = new UserViewModel(user);

         // Tell the user's connected clients that the name changed
         foreach (var client in user.ConnectedClients)
         {
             Clients.Client(client.Id).userNameChanged(userViewModel);
         }

         // Notify all users in the rooms
         foreach (var room in user.Rooms)
         {
             Clients.Group(room.Name).changeUserName(oldUserName, userViewModel, room.Name);
         }
     }
     void IChatNotificationService.ChangeNote(ChatUser user)
     {
         bool isNoteCleared = user.Note == null;

         // Update the calling client
         foreach (var client in user.ConnectedClients)
         {
             Clients.Client(client.Id).noteChanged(user.IsAfk, isNoteCleared);
         }

         // Create the view model
         var userViewModel = new UserViewModel(user);

         // Tell all users in rooms to change the note
         foreach (var room in user.Rooms)
         {
             Clients.Group(room.Name).changeNote(userViewModel, room.Name);
         }
     }
     void IChatNotificationService.ChangeFlag(ChatUser user)
     {
         bool isFlagCleared = String.IsNullOrWhiteSpace(user.Flag);

         // Create the view model
         var userViewModel = new UserViewModel(user);

         // Update the calling client
         foreach (var client in user.ConnectedClients)
         {
             Clients.Client(client.Id).flagChanged(isFlagCleared, userViewModel.Country);
         }

         // Tell all users in rooms to change the flag
         foreach (var room in user.Rooms)
         {
             Clients.Group(room.Name).changeFlag(userViewModel, room.Name);
         }
     }
     void IChatNotificationService.ChangeTopic(ChatUser user, ChatRoom room)
     {
         bool isTopicCleared = String.IsNullOrWhiteSpace(room.Topic);
         //To do figure out what this does
         //var parsedTopic = ConvertUrlsAndRoomLinks(room.Topic ?? "");
         var parsedTopic = room.Topic;
         foreach (var client in user.ConnectedClients)
         {
             Clients.Client(client.Id).topicChanged(isTopicCleared, parsedTopic);
         }
         // Create the view model
         var roomViewModel = new RoomViewModel
         { 
             Name = room.Name,
             Topic = parsedTopic
         };
         Clients.Group(room.Name).changeTopic(roomViewModel);
     }
     #endregion

     


}

    
}