using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Repository.Pattern.Ef6;

namespace Nmedia.Infrastructure.Domain.Data.Notification
{
   [DataContract(Namespace = "")]
    public class message : Entity
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
        [DataMember()]
        public int id { get; set; }
        [DataMember()]
        public virtual int messagetype_id { get; set; }
        [DataMember()]
        public virtual lu_messagetype messagetype { get; set; }
        [DataMember()]
        public virtual int? template_id { get; set; }
        [DataMember()]
        public virtual lu_template template { get; set; }
        [DataMember()]
        public virtual int systemaddress_id { get; set; }
        [DataMember()]
        public virtual systemaddress systemaddress { get; set; }
        [DataMember()]
        public virtual ICollection<address> recipients { get; set; }
        [DataMember()]
        public string sendingapplication { get; set; }  //TO do convert applications to enum as well
        [DataMember()]
        public string content { get; set; }
        [DataMember()]
        public string body { get; set; }
        [DataMember()]
        public string subject { get; set; }
        [DataMember()]
        public object attachMents { get; set; }
        [DataMember()]
        public DateTime? creationdate { get; set; }
        [DataMember()]
        public int? sendattempts { get; set; }
        [DataMember()]
        public bool? sent { get; set; }


        public static message Create(Action<message> init)
        {
            var message = new message();
            //address.MessageAddressID = Guid.NewGuid();
            //message.messagetype = new lu_messageType();
            // message.template = new lu_template();
            //  message.recipients = new List<address>();
            message.creationdate = DateTime.Now;
            message.sent = false;
            message.sendattempts = 0;
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
