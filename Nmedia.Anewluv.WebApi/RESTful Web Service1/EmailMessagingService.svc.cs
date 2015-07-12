using System.ServiceModel;
using System.ServiceModel.Activation;

namespace RESTful_Web_Service1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class EmailMessagingService : IMessaging
    {
        public string Send(string To, string Subject, string Message)
        {
            return Message;
        }

        public string SendAsync(string To, string Subject, string Message)
        {
            return Message;
        }

        public string Receive(string MessageId)
        {
            return MessageId;
        }
    }
}
