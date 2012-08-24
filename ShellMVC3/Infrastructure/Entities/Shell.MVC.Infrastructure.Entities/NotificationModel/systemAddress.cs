using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{
    public class systemAddress
    {
        
        [Key]
        public int id { get; set; }      
        public virtual lu_systemAddressType systemAddressType { get; set; }              
        public string emailAddress { get; set; }
        public string hostIp { get; set; }
        public string hostName { get; set; }
        public string createdBy { get; set; }   
        public string credentialUserName { get; set; }
        public string credentialPassword { get; set; }    //salt password  plain text for now   
        public bool active { get; set; }       
        public DateTime creationDate { get; set; }
        public DateTime? removalDate { get; set; }

        public static systemAddress Create(Action<systemAddress> init)
        {
            var systemaddress = new systemAddress();
            //address.MessageAddressID = Guid.NewGuid();
            systemaddress.creationDate = DateTime.Now;
           // systemaddress.RemovalDate = DateTime.Now;
            systemaddress.active = true;
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
