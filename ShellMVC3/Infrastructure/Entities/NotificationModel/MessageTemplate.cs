using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace NotificationModel
{

   

    public class MessageTemplateLookup
    {
        //we generate this manually from enums for now
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MessageTemplateLookupId { get; set; }        
        public string Description { get; set; }        
        public string PhysicalLocation { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? RemovalDate { get; set; }
        public bool Active { get; set; }
        public string RazorTemplateBody { get; set; }
        public string StringTemplateSubject { get; set; }
        public string StringTemplateBody { get; set; }

        public static MessageTemplateLookup Create(Action<MessageTemplateLookup> init)
        {
            var messagetemplate = new MessageTemplateLookup();
            //address.MessageAddressID = Guid.NewGuid();
            messagetemplate.CreationDate = DateTime.Now;
            messagetemplate.Active = true;
            init(messagetemplate); return messagetemplate;
        }


    }
}
