using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;







using Shell.MVC2.Interfaces;
using Shell.MVC2.Domain.Entities.Anewluv.Chat;
using Shell.MVC2.Domain.Entities.Anewluv.Chat.ViewModels;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Shell.MVC2.Infastructure.Chat
{
    //TO DO add a room sweeper to kill chat request denined rooms or chatrequest rooms or even chat rooms with no
    //activity over 30 mins , run it on a different timer maybe 
    #region "Signal IR chat Background task methods"
    public class ChatInfrastructure
    {
        // Background task info
        private static bool _sweeping;           

        private static void ClearConnectedClients(IChatRepository  repository)
        {
            try
            {
                foreach (var u in repository.Users)
                {
                    if (u.IsAfk)
                    {
                        u.Status = (int)userstatusEnum.Offline;
                    }
                }

                repository.RemoveAllClients();
                repository.CommitChanges();
            }
            catch (Exception ex)
            {
              //  Elmah.ErrorLog.GetDefault(null).Log(new Error(ex));
            }
        }
    
        private static void MarkInactiveUsers(IChatRepository repo,  IDependencyResolver  resolver)
        {
            var connectionManager = resolver.Resolve<IConnectionManager>();
            var clients = connectionManager.GetHubContext("chatHub").Clients;
            var inactiveUsers = new List<ChatUser>();

            foreach (var user in repo.Users)
            {
                var status = (userstatusEnum)user.Status;
                if (status == userstatusEnum.Offline)
                {
                    // Skip offline users
                    continue;
                }

                var elapsed = DateTime.UtcNow - user.LastActivity;

                if (!user.IsAfk && elapsed.TotalMinutes > 30)
                {
                    // After 30 minutes of inactivity make the user afk
                    user.IsAfk = true;
                }

                if (elapsed.TotalMinutes > 15)
                {
                    user.Status = (int)userstatusEnum.Inactive;
                    inactiveUsers.Add(user);
                }
            }

            var roomGroups = from u in inactiveUsers
                             from r in u.Rooms
                             select new { User = u, Room = r } into tuple
                             group tuple by tuple.Room into g
                             select new
                             {
                                 Room = g.Key,
                                 Users = g.Select(t => new UserViewModel(t.User))
                             };

            foreach (var roomGroup in roomGroups)
            {
                clients.All(roomGroup.Room.Name).markInactive(roomGroup.Users).Wait();
            }
        }
       public static void Sweep(Func<IChatRepository> repositoryFactory, IDependencyResolver resolver)
        {
            if (_sweeping)
            {
                return;
            }

            _sweeping = true;

            try
            {
                using (IChatRepository repo = repositoryFactory())
                {
                    MarkInactiveUsers(repo, resolver);

                    repo.CommitChanges();
                }
            }
            catch (Exception ex)
            {
              //  Elmah.ErrorLog.GetDefault(null).Log(new Error(ex));
            }
            finally
            {
                _sweeping = false;
            }
        }
    }
    #endregion
}