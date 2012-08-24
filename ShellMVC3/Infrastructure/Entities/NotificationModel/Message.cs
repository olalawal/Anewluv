using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace NotificationModel
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public int MessageTypeLookupId { get; set; }
        public virtual MessageTypeLookup MessageType { get; set; }
        public int MessageTemplateLookupId { get; set; }
        public virtual MessageTemplateLookup MessageTemplate { get; set; }
        public int MessageSystemAddressID { get; set; }
        public virtual MessageSystemAddress  SystemSender { get; set; }
        public virtual ICollection<MessageAddress> Recipients { get; set; }
        public string SendingApplication { get; set; }  //TO do convert applications to enum as well
        public string Body { get; set; }
        public string Subject { get; set; }
        public object AttachMents { get; set; }
      
        public DateTime CreationDate { get; set; }
        public bool? Sent { get; set; }

        public static Message Create(Action<Message> init)
        {
            var message = new Message();
            //address.MessageAddressID = Guid.NewGuid();
            message.CreationDate = DateTime.Now;
            message.Sent =false;           
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
