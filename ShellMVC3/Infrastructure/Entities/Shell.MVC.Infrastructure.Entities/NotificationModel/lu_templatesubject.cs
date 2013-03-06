using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{



     [DataContract(Namespace = "")]
    public class lu_templatesubject
    {
        /// <summary>
        /// This is an enumeration type for the log severity types we track
        /// this is parsed into database values when the context is created
        /// </summary>


        //we generate this manually from enums for now

         [DataMember()]
         [Key]
         public int id { get; set; }
         [DataMember()]
         public string description { get; set; }
         [DataMember()]
         public bool? active { get; set; }
         [DataMember()]
         public DateTime? creationdate { get; set; }
         [DataMember()]
         public DateTime? removaldate { get; set; }



        public static lu_templatesubject Create(Action<lu_templatesubject> init)
        {
            var messagesubject = new lu_templatesubject();
            //address.MessageAddressID = Guid.NewGuid();
            messagesubject.creationdate = DateTime.Now;         
            init(messagesubject); return messagesubject;
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
