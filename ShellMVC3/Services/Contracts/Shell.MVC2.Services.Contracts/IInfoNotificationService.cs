using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Infrastructure.Entities.NotificationModel;
using System.ServiceModel;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels.Email;
using System.ServiceModel.Web;

namespace Shell.MVC2.Services.Contracts
{
    [ServiceContract]
    public interface IInfoNotificationService 
    {



        //[OperationContract]
        //int WriteLogEntry(CustomErrorLog logEntry);

        //temporary method for use by designer to get the message information formated for them
        [WebGet]
        [OperationContract]
        EmailViewModel getgenericemailviewmodel();

        [WebGet]
        [OperationContract]
        EmailViewModel getcontactusemailviewmodel(string from);

        [WebGet]
        [OperationContract]
        EmailViewModel getemailmatchesviewmodelbyprofileid(int profileid);

        [WebGet]
        [OperationContract]
        EmailModel getemailviewmodelbytemplateid(templateenum template);

    }
}
