using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{
    public class news
    {
     
        //added conttructor to replace create method imo
        public news()
        {
	      //  recipients = new List<address>();
	      //  messagetype = new lu_messageType();
	      //  template = new lu_template();
	       // systemAddress = new systemAddress();
	       // creationDate = DateTime.Now;
	      //  sent = false;
         }



        [Key]
        public int id { get; set; }     
        public virtual lu_newstype   newstype { get; set; }  
        public virtual string newsmessage { get; set; }      
        public DateTime creationDate { get; set; }
        public bool? sent { get; set; }

        public static message Create(Action<message> init)
        {
            var message = new message();
            //address.MessageAddressID = Guid.NewGuid();
            //message.messagetype = new lu_messageType();
          //  message.template = new lu_template();
          //  message.recipients = new List<address>();
            message.creationDate = DateTime.Now;
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
