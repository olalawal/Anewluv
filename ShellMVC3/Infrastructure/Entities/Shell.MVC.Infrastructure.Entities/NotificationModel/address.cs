using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{
    public class address
    {

        public address()
{
	//messages = new List<message>();
//	addressType = new lu_addressType();
	//creationdate = DateTime.Now;
	//active = true;
}


        [Key]
        public int id { get; set; }       
        public virtual lu_addresstype addresstype { get; set; }
        public virtual ICollection<message> messages { get; set; }    
        public string emailaddress { get; set; }
        public string username { get; set; }
        public string otheridentifer { get; set; }  //use this for chat notifications maybe
        public bool active { get; set; }
        public DateTime creationdate { get; set; }
        public DateTime? removaldate { get; set; }
       
        public static address Create(Action<address> init)
        {
            var address = new address();
          
            //address.MessageAddressID = Guid.NewGuid();
            address.creationdate = DateTime.Now;          
            address.active = true;
         //   address.messages = new List<message>();
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
