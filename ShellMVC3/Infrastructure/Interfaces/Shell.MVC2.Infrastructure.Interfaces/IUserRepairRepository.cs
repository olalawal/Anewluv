using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Shell.MVC2.Infrastructure.Entities.UserRepairModel ;

namespace Shell.MVC2.Infrastructure.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public interface  IUserRepairRepository 
    {
        int WriteCompleteLogEntry(UserRepairLog log);
    }
    
}
