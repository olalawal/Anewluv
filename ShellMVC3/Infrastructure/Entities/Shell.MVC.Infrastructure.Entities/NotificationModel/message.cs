using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{
    [DataContract]
    public class message
    {
     
        //added conttructor to replace create method imo
        public message()
        {
	      //  recipients = new List<address>();
	      //  messagetype = new lu_messageType();
	      //  template = new lu_template();
	       // systemAddress = new systemAddress();
	       // creationdate = DateTime.Now;
	      //  sent = false;
         }



        [Key]
        public int id { get; set; }     
        public virtual lu_messagetype  messagetype { get; set; }       
        public virtual lu_template  template { get; set; }
        public virtual systemaddress systemaddress { get; set; }
        public virtual ICollection<address > recipients { get; set; }
        public string sendingApplication { get; set; }  //TO do convert applications to enum as well
        public string body { get; set; }
        public string subject { get; set; }
        public object attachMents { get; set; }      
        public DateTime creationdate { get; set; }
        public bool? sent { get; set; }

        public static message Create(Action<message> init)
        {
            var message = new message();
            //address.MessageAddressID = Guid.NewGuid();
            //message.messagetype = new lu_messageType();
          //  message.template = new lu_template();
          //  message.recipients = new List<address>();
            message.creationdate = DateTime.Now;
            message.sent =false;           
            init(message); return message;
        }

            ////use create method it like this 
            //  message=  (Message.Create (c =>
            //   {
            //       c.MessageTemplateLookupId = (int)MessageTemplateEnum.GenericErrorMessage;
            //       c.MessageTypeLookupId = (int)MessageTypeEnum.DeveloperError;
            //       c.Body = c.MessageTemplate == null ? TemplateParser.RazorFileTemplate("", ref customerror) :
            //                                            TemplateParser.RazorDBTemplate(message.MessageTemplate.RazorTemplateBody, ref customerror);
            //       c.Subject = string.Format("An error occured");
            //       c.Recipients = recipientEmailAddresss.ToList();
            //       c.SendingApplication = "ErrorNotificationService";
            //       c.SystemSender = SystemSenderAddress;
            //   }));

    }
}
