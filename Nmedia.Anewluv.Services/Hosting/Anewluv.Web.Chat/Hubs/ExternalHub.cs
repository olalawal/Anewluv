
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR;
using Shell.MVC2.Interfaces;
using Anewluv.Services.Contracts;

namespace Anewluv.Web.Chat
{
    
    [HubName("externalHub")]
    public class ExternalHub : Hub
    {
        private readonly IDateTimeDependencyResolver _dateTimeDependencyResolver;
        private readonly IChatRepository _repository;
        private readonly IChatService _service;

        public ExternalHub(IDateTimeDependencyResolver dateTimeDependencyResolver, IChatRepository chatrepo, IChatService service)
        {
            _dateTimeDependencyResolver = dateTimeDependencyResolver;
            _repository = chatrepo;
            _service = service;
        }

        public void SomeExternalMethod()
        {
            Clients.Caller.onSomeExternalMethod("external test for me");
        }

        public object GetDateTime()
        {
            return new
                   {
                       CurrentDateTime = _dateTimeDependencyResolver.GetDateTime()
                   };
        }
    }

         

}
