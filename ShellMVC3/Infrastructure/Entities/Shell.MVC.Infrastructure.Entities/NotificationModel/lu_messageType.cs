using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{




    public class lu_messageType
    {
        /// <summary>
        /// This is an enumeration type for the log severity types we track
        /// this is parsed into database values when the context is created
        /// </summary>


        //we generate this manually from enums for now
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataMember]
        public int id { get; set; }
          [DataMember]
        public string description { get; set; }
          [DataMember]
        public bool active { get; set; }
          [DataMember]
        public DateTime creationDate { get; set; }
          [DataMember]
        public DateTime? removalDate { get; set; }



        public static lu_messageType Create(Action<lu_messageType> init)
        {
            var messagetype = new lu_messageType();
            //address.MessageAddressID = Guid.NewGuid();
            messagetype.creationDate = DateTime.Now;          
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
