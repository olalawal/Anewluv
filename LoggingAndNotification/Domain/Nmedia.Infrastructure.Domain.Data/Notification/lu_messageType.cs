using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Nmedia.Infrastructure.Domain.Data.Notification
{



     [DataContract(Namespace = "")]
    public class lu_messagetype
    {
        /// <summary>
        /// This is an enumeration type for the log severity types we track
        /// this is parsed into database values when the context is created
        /// </summary>


        //we generate this manually from enums for now
     [Key]
        [DataMember()]
        public int id { get; set; }
     [DataMember()]
     public string description { get; set; }
     [DataMember()]
     public bool? active { get; set; }
     [DataMember()]
     public DateTime? creationdate { get; set; }
     [DataMember()]
     public DateTime? removaldate { get; set; }



        public static lu_messagetype Create(Action<lu_messagetype> init)
        {
            var messagetype = new lu_messagetype();
            //address.MessageAddressID = Guid.NewGuid();
            messagetype.creationdate = DateTime.Now;          
            messagetype.active = true;
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
