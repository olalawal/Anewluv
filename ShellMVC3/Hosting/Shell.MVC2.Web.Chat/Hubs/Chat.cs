using System;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Shell.MVC2.Web.Chat
{

    public class Chat : Hub
    {
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients. 
            Clients.All.broadcastMessage(name, message);


        }


        public void SomeInternalMethod()
        {
            Clients.Caller.onSomeInternalMethod("internal test");
        }
    }
}