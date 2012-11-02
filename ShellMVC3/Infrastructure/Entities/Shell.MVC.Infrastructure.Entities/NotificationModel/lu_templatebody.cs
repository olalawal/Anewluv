using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{




    public class lu_templatebody
    {
       
        //we generate this manually from enums for now
        [Key]
        public int id { get; set; }      
        public string description { get; set; }         
        public bool active { get; set; }       
        public DateTime creationDate { get; set; }  
        public DateTime? removalDate { get; set; }



        public static lu_templatebody Create(Action<lu_templatebody> init)
        {
            var messagebody = new lu_templatebody();
            //address.MessageAddressID = Guid.NewGuid();
            messagebody.creationDate = DateTime.Now;
            messagebody.active = true;
            init(messagebody); return messagebody;
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
