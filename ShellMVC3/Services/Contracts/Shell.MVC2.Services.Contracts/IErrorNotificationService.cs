using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Shell.MVC2.Services.Contracts
{
    [ServiceContract]
    public interface IErrorNotificationService 
    {
       
        [OperationContract]
        [WebInvoke]
        int SendErrorMessageToDevelopers(CustomErrorLog customerror);

     
    }
}
