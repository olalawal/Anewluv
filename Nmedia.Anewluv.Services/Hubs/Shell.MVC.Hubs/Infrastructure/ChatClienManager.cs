﻿

using System.Security.Principal;


using System.Threading.Tasks;
using Shell.MVC2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Shell.MVC2.SignalR.Infastructure
{

    public class UserIdClientIdFactory : IConnectionIdFactory
    {
       

        #region IConnectionIdFactory Members

        string IConnectionIdFactory.CreateConnectionId(SignalR.Hosting.IRequest request)
        {
            // get and return the UserId here, in my app it is stored 
            // in a custom IIdentity object, but you get the idea 


            return HttpContext.Current.User.Identity.Name != null ?
                //TO DO change to get profileID from Appfabric or the database and log user infor as well
                 HttpContext.Current.User.Identity.Name.ToString() :
                 Guid.NewGuid().ToString();
            
        }

        #endregion
    } 


}
