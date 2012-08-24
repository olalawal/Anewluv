using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Shell.MVC2.Infrastructure.Entities.UserRepairModel;
using System.ServiceModel.Web;

namespace Shell.MVC2.Services.Logging
{
 
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface  IUserRepairService 
    {
        [OperationContract]
        [WebInvoke]
        int WriteCompleteLogEntry(UserRepairLog log);
    }
}
