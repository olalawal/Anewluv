using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace NotificationModel
{
    public class MessageSystemAddress
    {
        
        [Key]
        public int MessageSystemAddressID { get; set; }
        public int MessageSystemAddressTypeLookupID { get; set; }
        public virtual MessageSystemAddressTypeLookup SystemAddressType { get; set; }              
        public string EmailAddress { get; set; }
        public string HostIp { get; set; }
        public string HostName { get; set; }
        public string CreatedBy { get; set; }   
        public string CredentialUserName { get; set; }
        public string CredentialPassword { get; set; }    //salt password  plain text for now   
        public bool Active { get; set; }       
        public DateTime CreationDate { get; set; }
        public DateTime? RemovalDate { get; set; }

        public static MessageSystemAddress Create(Action<MessageSystemAddress> init)
        {
            var systemaddress = new MessageSystemAddress();
            //address.MessageAddressID = Guid.NewGuid();
            systemaddress.CreationDate = DateTime.Now;
           // systemaddress.RemovalDate = DateTime.Now;
            systemaddress.Active = true;
            init(systemaddress); return systemaddress;
        }

        //  //use create method it like this 
        // context.MessageSystemAddress.Add(MessageSystemAddress.Create(c=>
        //{c.Name = "Name"; 
        // c.Email = "some@one.com";
        // c.Salt = salt; 
        // c.Password = "mypass";
        // c.Roles = new List<Role> { adminRole, userRole }; 
        //  }));

    }
}
