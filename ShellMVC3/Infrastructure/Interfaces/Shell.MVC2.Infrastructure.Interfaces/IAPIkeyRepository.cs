using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;

namespace Shell.MVC2.Infrastructure.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.

    public interface IAPIkeyRepository
    {

      
           bool validateAPIkey(string key);
        
         List<Guid> getAPIkeys  ();

         //List<Guid> PopulateAPIKeys();

        //TO DO use enum maybe
       Guid generateAPIkey(string service);

      
    }
}
