using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace NotificationModel
{
    public class MessageAddress
    {

        [Key]
        public int MessageAddressId { get; set; }
        public int MessageAddressTypeLookupID { get; set; }
        public virtual MessageAddressTypeLookup AddressType { get; set; }              
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public string OtherIdentifer { get; set; }  //use this for chat notifications maybe
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? RemovalDate { get; set; }
       // public virtual ICollection<MessageAddress> ReportsTo { get; set; }

        //// EntityId is FK 
        //public int MessageId { get; set; }
        //// Navigation property 
        //[ForeignKey("MessageId")]
        //public Message  LinkedMessage { get; set; } 

        public static MessageAddress Create(Action<MessageAddress> init)
        {
            var address = new MessageAddress();
            //address.MessageAddressID = Guid.NewGuid();
            address.CreationDate = DateTime.Now;          
            address.Active = true;
            init(address); return address; 
        } 
    
       //  //use create method it like this 
        // context.MessageAddress.Add(MessageAddress.Create(c=>
       //{c.Name = "Name"; 
       // c.Email = "some@one.com";
       // c.Salt = salt; 
       // c.Password = "mypass";
       // c.Roles = new List<Role> { adminRole, userRole }; 
       //  }));
    }
}
