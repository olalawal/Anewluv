using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace NotificationModel
{




    public class MessageTypeLookup
    {
        /// <summary>
        /// This is an enumeration type for the log severity types we track
        /// this is parsed into database values when the context is created
        /// </summary>


        //we generate this manually from enums for now
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MessageTypeLookupID { get; set; }    
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? RemovalDate { get; set; }
   


        public static MessageTypeLookup Create(Action<MessageTypeLookup> init)
        {
            var messagetype = new MessageTypeLookup();
            //address.MessageAddressID = Guid.NewGuid();
            messagetype.CreationDate = DateTime.Now;          
            messagetype.Active = true;
            init(messagetype); return messagetype;
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
