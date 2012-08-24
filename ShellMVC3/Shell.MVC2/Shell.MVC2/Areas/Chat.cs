using SignalR.Hubs;
using SignalR;

using System.Threading.Tasks;
using Shell.MVC2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dating.Server.Data;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;
using System.Web.Mvc;

using Shell.MVC2.Models.Chat;
using Shell.MVC2.Services.Chat;
using Shell.MVC2.Repositories.Chat;

using Newtonsoft.Json;



using System.ComponentModel.DataAnnotations;

namespace ChatTest
{
    //, IControllerFactory 

    
    public class Chat : Hub,  IDisconnect  
{
        


    private readonly IChatRepository  _repository;
    private readonly IChatService _service;
  //  private readonly IResourceProcessor _resourceProcessor;
    //private readonly IApplicationSettings _settings;


    

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
            string version = Caller.version;
            return String.IsNullOrEmpty(version) ||
                    new Version(version) != typeof(Chat).Assembly.GetName().Version;
        }
    }


          public string Startup()
          {
          
             var clientId = this.Context.User.Identity.Name;
            // var clientstate = GetClientState();
           
             
 
             // clients[clientId].method();             
              Caller.guid = clientId ;
              var temp = Context.Cookies.AllKeys; 
              var count = Context.Cookies.Count;

              return "";
          }





     public bool  Join()
     {
           // clientsCount++;
          //  var msg = new
           // {
          //      Text = string.Format("Client {0} joined, now {1} clients", Context.ClientId, clientsCount),
          //      Timestamp = DateTime.UtcNow
          //  };

        //
         //   Clients.recieve(msg);
       //     return this.Context.ClientId   ;


         // Get the client state
         //ClientState clientState = GetClientState();

        // // Try to get the user from the client state
       //  ChatUser user = _repository.GetUserById(clientState.UserId);

         // Threre's no user being tracked
         //if (user == null)
         //{
         //    return false;
         //}

         // Migrate all users to use new auth
        
         // Update some user values
      //   _service.UpdateActivity(user, Context.ConnectionId, UserAgent);
      //   _repository.CommitChanges();

      //   OnUserInitialize(clientState, user);

         return true;
      }



    public void Send(string message,string UserName,string roomGuid )  
    {
        //find the client ID'
    //    string id = this.Context.ClientId;
        string id2 = this.Context.User.Identity.Name ;
        string callerID = Caller.id;

        var msg = new ChatMessageModel 
        {
             Message  = message,
           CreationDate  =  DateTime.UtcNow,
           
        }; 


        //Call the addMessage method on all clients. 
        Clients.addMessage(msg);

        // Invoke addMessage on all clients in group foo
        Clients[roomGuid].addMessage(message);


    }

        //method callable from client test
    public string Send(string data)
    {
        return data;
    }


    public Task  Disconnect()
    {
        // Query the database to find the user by it's client id.
       // var user = db.Users.Where(u => u.ConnectionId == Context.ConnectionId);
        return null;
        
    }

        //all private conv's take place in rooms which do not have names buy a random GUID
     public void JoinRoom(string RoomGuid)
     {     

         AddToGroup(RoomGuid);
     }

     public void LeaveRoom(string roomGuid)
     {
         RemoveFromGroup(roomGuid);
     }


     private void OnUserInitialize(ClientState clientState, ChatUser user)
     {
         // Update the active room on the client (only if it's still a valid room)
         if (user.Rooms.Any(room => room.Name.Equals(clientState.ActiveRoom, StringComparison.OrdinalIgnoreCase)))
         {
             // Update the active room on the client (only if it's still a valid room)
             Caller.activeRoom = clientState.ActiveRoom;
         }

       //  LogOn(user, Context.ConnectionId);
     }

     private ClientState GetClientState()
     {
         // New client state
        // var jabbrState = GetCookieValue("jabbr.state");

         ClientState clientState = null;

         //if (String.IsNullOrEmpty(jabbrState))
         //{
         //    clientState = new ClientState();
         //}
         //else
         //{
         //    clientState = JsonConvert.DeserializeObject<ClientState>(jabbrState);
         //}

         //// Read the id from the caller if there's no cookie
         //clientState.UserId = clientState.UserId ?? Caller.id;

         return clientState;
     }

     private string GetCookieValue(string key)
     {
         //string value = Context.Cookies[key];
        // return value != null ? HttpUtility.UrlDecode(value) : null;
         return "";
     }


   
}

    
}