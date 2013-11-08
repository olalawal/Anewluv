using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Nmedia.Infrastructure.Domain.Errorlog;

namespace Shell.MVC2.Infrastructure.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.

    public interface IAPIkeyRepository
    {

         bool IsValidAPIKey(string key);       

         List<Guid> APIKeys();

         //List<Guid> PopulateAPIKeys();

         Guid generateAPIkey(string service);
        // string APIKEYLIST();

      
    }
}
