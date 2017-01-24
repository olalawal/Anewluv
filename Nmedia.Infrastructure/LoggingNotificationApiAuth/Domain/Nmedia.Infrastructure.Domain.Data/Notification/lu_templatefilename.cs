using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Repository.Pattern.Ef6;

namespace Nmedia.Infrastructure.Domain.Data.Notification
{



     [DataContract(Namespace = "")]
    public class lu_templatefilename :Entity
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



        public static lu_templatefilename Create(Action<lu_templatefilename> init)
        {
            var filename = new lu_templatefilename();
            //address.MessageAddressID = Guid.NewGuid();
            filename.creationdate = DateTime.Now;
            filename.active = true;
            init(filename); return filename;
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
