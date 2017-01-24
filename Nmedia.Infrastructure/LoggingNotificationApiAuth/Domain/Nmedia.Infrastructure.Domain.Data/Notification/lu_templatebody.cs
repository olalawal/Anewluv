using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Nmedia.Infrastructure.Domain.Data.Notification
{



     [DataContract(Namespace = "")]
    public class lu_templatebody
    {
       
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

        //[DataMember()]
        //public int? application_id { get; set; }

        //[DataMember()]
        //public virtual lu_application application { get; set; }



        public static lu_templatebody Create(Action<lu_templatebody> init)
        {
            var messagebody = new lu_templatebody();
            //address.MessageAddressID = Guid.NewGuid();
            messagebody.creationdate = DateTime.Now;
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
