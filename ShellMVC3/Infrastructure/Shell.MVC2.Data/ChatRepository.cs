using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using Shell.MVC2.Domain.Entities.Anewluv.Chat;
using Shell.MVC2.Domain.Entities.Anewluv.Chat.ViewModels ;

using Shell.MVC2.Interfaces;
using Shell.MVC2.Infrastructure;
using System.Data.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Objects.DataClasses;

namespace Shell.MVC2.Data
{
   public  class ChatRepository :   ChatRepositoryBase, IChatRepository 
    {


       private IChatRepository  _chatrepository;

       public ChatRepository(ChatContext chatcontext, AnewluvContext datingcontext, IChatRepository chatrepository) 
           :base(chatcontext,datingcontext)
       {
           _chatrepository = chatrepository;
       }


           public IQueryable<ChatRoom> Rooms
           {
               get { return _chatcontext.ChatRooms ; }
           }

           public IQueryable<ChatUser> Users
           {
               get { return _chatcontext.ChatUsers; }
           }


           public IQueryable<ChatUser> GetOnlineUsers(ChatRoom room)
           {
               return _chatcontext.Entry(room)
                         .Collection(r => r.Users)
                         .Query().Online();
                           
           }

           public IQueryable<ChatUser> GetOnlineUsers()
           {
               return _chatcontext.ChatUsers.Include("ConnectedClients").Online();
           }



           public void Add(ChatRoom room)
           {
               _chatcontext.ChatRooms.Add(room);
               _chatcontext.SaveChanges();
           }

           public void Add(ChatUser user)
           {
               _chatcontext.ChatUsers.Add(user);
               _chatcontext.SaveChanges();
           }

           public void Add(ChatMessage message)
           {
               _chatcontext.ChatMessages.Add(message);
           }

           public void Remove(ChatRoom room)
           {
               _chatcontext.ChatRooms.Remove(room);
               _chatcontext.SaveChanges();
           }

           public void Remove(ChatUser user)
           {
               _chatcontext.ChatUsers.Remove(user);
               _chatcontext.SaveChanges();
           }

           public bool Update(ChatRoom room)
           {
               if ( _chatcontext.ChatRooms.Contains(room))
               {
                   Remove(room);
                   Add(room);
                   return true;
               }
               return false;
           }

           public bool Update(ChatUser user)
           {
               if ( _chatcontext.ChatUsers .Contains(user))
               {
                   Remove(user);
                   Add(user);
                   return true;
               }
               return false;

           }

           public void Dispose()
           {
               _chatcontext.Dispose();
           }

           public ChatUser GetUserById(string userId)
           {
               return _chatcontext.ChatUsers.FirstOrDefault(u => u.Id == userId);
           }

           public ChatUser GetUserByName(string userName)
           {
               return _chatcontext.ChatUsers.FirstOrDefault(u => u.Name == userName);
           }

           public ChatRoom GetRoomByName(string roomName)
           {
               return _chatcontext.ChatRooms.Include("Owners")
                               .Include("ChatUsers")
                               .FirstOrDefault(r => r.Name  == roomName);
           }

           public IQueryable<ChatRoom> GetAllowedRooms(ChatUser user)
           {
               // All *open* public and private rooms the user can see.
               return _chatcontext.ChatRooms
                   .Where(r =>
                          (!r.Private  && !r.Closed ) ||
                          (r.Private && !r.Closed && r.AllowedUsers.Any(u => u.Key == user.Key)));
           }

           public IQueryable<ChatMessage> GetMessagesByRoom(string roomName)
           {
               return _chatcontext.ChatMessages.Include("ChatRoom").Where(r => r.Room .Name == roomName);
           }

           public ChatMessage GetMessageById(string id)
           {
               return _chatcontext.ChatMessages.FirstOrDefault(m => m.Id == id);
           }

           public IQueryable<ChatMessage> GetPreviousMessages(string messageId)
           {
               var info = (from m in _chatcontext.ChatMessages.Include("ChatRoom")
                           where m.Id == messageId
                           select new
                           {
                               m.When,
                               RoomName = m.Room.Name
                           }).FirstOrDefault();

               return from m in GetMessagesByRoom(info.RoomName)
                      where m.When < info.When
                      select m;
           }

           public IQueryable<ChatUser> SearchUsers(string name)
           {
               return _chatcontext.ChatUsers.Online().Where(u => u.Name.Contains(name));
           }

           public void Add(ChatClient client)
           {
               _chatcontext.ChatClients.Add(client);
               _chatcontext.SaveChanges();
           }

           public void Remove(ChatClient client)
           {
               _chatcontext.ChatClients .Remove(client);
               _chatcontext.SaveChanges();
           }

           public ChatUser GetUserByClientId(string clientId)
           {
               var client = GetClientById(clientId);
               if (client != null)
               {
                   return client.User;
               }
               return null;
           }

           public ChatUser GetUserByIdentity(string userIdentity)
           {
               return _chatcontext.ChatUsers.FirstOrDefault(u => u.Id  == userIdentity);
           }

           public ChatUser GetUserByProfileid(int profileid)
           {
               return _chatcontext.ChatUsers.FirstOrDefault(u => u.profileid == profileid);
           }


           public ChatUser GetUserByScreenName(string userScreenName)
           {
               return _chatcontext.ChatUsers.FirstOrDefault(u => u.ScreenName == userScreenName);
           }


           public ChatClient GetClientById(string clientId)
           {
               return _chatcontext.ChatClients.Include("ChatUser").FirstOrDefault(c => c.Id == clientId);
           }

       /// <summary>
           /// from new version of JABBRA not used i dont think
       /// </summary>
       /// <param name="user"></param>
       /// <param name="room"></param>
       public void AddUserRoom(ChatUser user, ChatRoom room)
           {
               RunNonLazy(() => room.Users.Add(user));
           }

       /// <summary>
       ///  from new version of JABBRA not used i dont think
       /// </summary>
       /// <param name="user"></param>
       /// <param name="room"></param>
           public void RemoveUserRoom(ChatUser user, ChatRoom room)
           {
               RunNonLazy(() =>
               {
                   // The hack from hell to attach the user to room.Users so delete is tracked
                   ObjectContext context = ((IObjectContextAdapter)_chatcontext).ObjectContext;
                   RelationshipManager manger = context.ObjectStateManager.GetRelationshipManager(room);
                   IRelatedEnd end = manger.GetRelatedEnd("Shell.MVC2.Domain.Entities.Anewluv.Chat", "ChatRoom_Users_Target");
                   end.Attach(user);

                   room.Users.Remove(user);
               });
           }

           private void RunNonLazy(Action action)
           {
               bool old = _chatcontext.Configuration.LazyLoadingEnabled;
               try
               {
                   _chatcontext.Configuration.LazyLoadingEnabled = false;
                   action();
               }
               finally
               {
                   _chatcontext.Configuration.LazyLoadingEnabled = old;
               }
           }


           public void RemoveAllClients()
           {
               foreach (var c in _chatcontext.ChatClients)
               {
                   _chatcontext.ChatClients.Remove(c);
               }
           }

           public void CommitChanges()
           {
               _chatcontext.SaveChanges();
           }



           public bool IsUserInRoom(ChatUser user, ChatRoom room)
           {
               
               return _chatcontext .Entry(user)
                         .Collection(r => r.Rooms)
                         .Query()
                         .Where(r => r.Key == room.Key)
                         .Select(r => r.Name)
                         .FirstOrDefault() != null;
           }
              


              //updated this to also remove empty spaces
        public  string NormalizeUserName(string userName)
        {

            return userName.StartsWith("@") ? userName.Substring(1) : userName;
        }

        public  string NormalizeRoomName(string roomName)
        {
            return roomName.StartsWith("#") ? roomName.Substring(1) : roomName;
        }



       

        public  IEnumerable<ChatUser> Online(IEnumerable<ChatUser> source)
        {
            return source.Where(u => u.Status != (int)userstatusEnum.Offline);
        }

        public  ChatUser UserOnline( string name)
        {
            ChatUser user = _chatrepository.GetUserByName(name);

            if (user != null && user.Status != (int)userstatusEnum.Offline)
                return user;

            return null;

        }

        public  IEnumerable<ChatRoom> Allowed(IEnumerable<ChatRoom> rooms, string userId)
        {
            return from r in rooms
                   where !r.Private ||
                         r.Private && r.AllowedUsers.Any(u => u.Id == userId)
                   select r;
        }


        public  ChatRoom VerifyUserRoom( ChatUser user, string roomName)
        {
            if (String.IsNullOrEmpty(roomName))
            {
                throw new InvalidOperationException("Use '/join room' to join a room.");
            }

            //3-24-2011 added code to get the room if it already existed 
            ChatRoom room = GetRoomIfCreatedBefore(roomName);
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

        public  ChatUser VerifyUserId( string userId)
        {
            ChatUser user = _chatrepository.GetUserById(userId);

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
        public  ChatRoom VerifyAndOpenRoomIfExists(string roomName, bool OpenIfClosed = true)
        {



            if (String.IsNullOrWhiteSpace(roomName))
            {
                throw new InvalidOperationException("Room name cannot be blank!");
            }

            //best wau uf ut works
            var room = GetRoomIfCreatedBefore(roomName);


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

        public  ChatRoom GetRoomIfCreatedBefore( string roomName)
        {
            // var toUserName = parts[1];          
            string[] RoomMembers = roomName.Split('_');
            if (RoomMembers.Count() > 0)
                //make sure room does not exist
                return (from z in _chatrepository.Rooms where RoomMembers.All(s => z.Name.Contains(s)) select z).FirstOrDefault();

            roomName = roomName.StartsWith("#") ? roomName.Substring(1) : roomName;
            return _chatrepository.GetRoomByName(roomName);

        }


        public static ChatRoom VerifyRoom( string roomName, bool mustBeOpen = true)
        {
            if (String.IsNullOrWhiteSpace(roomName))
            {
                throw new InvalidOperationException("Room name cannot be blank!");
            }


            //3-24-2011 added code to get the room if it already existed 
            ChatRoom room = GetRoomIfCreatedBefore(roomName);

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

        public  ChatUser VerifyUser( string userName)
        {
            userName = userName.StartsWith("@") ? userName.Substring(1) : userName;

            ChatUser user = _chatrepository.GetUserByName(userName);

            if (user == null)
            {
                throw new InvalidOperationException(String.Format("Unable to find user '{0}'.", userName));
            }

            return user;
        }


       

     
        public ChatRoom VerifyUserRoom(ChatUser user, string roomName)
        {
            if (String.IsNullOrEmpty(roomName))
            {
                throw new InvalidOperationException("Use '/join room' to join a room.");
            }

            //3-24-2011 added code to get the room if it already existed 
            ChatRoom room = GetRoomIfCreatedBefore(roomName);
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

        public ChatUser VerifyUserId(string userId)
        {

            ChatUser user = _chatrepository.GetUserById(userId);

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
        public ChatRoom VerifyAndOpenRoomIfExists(string roomName, bool OpenIfClosed = true)
        {



            if (String.IsNullOrWhiteSpace(roomName))
            {
                throw new InvalidOperationException("Room name cannot be blank!");
            }

            //best wau uf ut works
            var room = GetRoomIfCreatedBefore(roomName);


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

        public ChatRoom GetRoomIfCreatedBefore(string roomName)
        {
            // var toUserName = parts[1];          
            string[] RoomMembers = roomName.Split('_');
            if (RoomMembers.Count() > 0)
                //make sure room does not exist
                return (from z in this.Rooms where RoomMembers.All(s => z.Name.Contains(s)) select z).FirstOrDefault();

            roomName = this.NormalizeRoomName(roomName);
            return this.GetRoomByName(roomName);

        }

        public ChatRoom VerifyRoom(string roomName, bool mustBeOpen = true)
        {
            if (String.IsNullOrWhiteSpace(roomName))
            {
                throw new InvalidOperationException("Room name cannot be blank!");
            }


            //3-24-2011 added code to get the room if it already existed 
            ChatRoom room = GetRoomIfCreatedBefore(roomName);

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

        public ChatUser VerifyUser(string userName)
        {
            userName = this.NormalizeUserName(userName);

            ChatUser user = this.GetUserByName(userName);

            if (user == null)
            {
                throw new InvalidOperationException(String.Format("Unable to find user '{0}'.", userName));
            }

            return user;
        }

       }


    
     
    
}
