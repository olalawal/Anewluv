﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Repository.Pattern.Ef6;

namespace Nmedia.Infrastructure.Domain.Data.Notification
{
    [DataContract(Namespace = "")]
    public class address :Entity
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
        [DataMember()]
        public int addresstype_id { get; set; }
        [DataMember()]
        public virtual lu_addresstype addresstype { get; set; }
        public virtual ICollection<message> messages { get; set; }
        [DataMember()]
        public string emailaddress { get; set; }
        [DataMember()]
        public string username { get; set; }
        [DataMember()]
        public string otheridentifer { get; set; }  //use this for chat notifications maybe
        [DataMember()]
        public bool active { get; set; }
        [DataMember()]
        public DateTime? creationdate { get; set; }
        [DataMember()]
        public DateTime? removaldate { get; set; }

        [DataMember()]
        public int? application_id { get; set; }

        [DataMember()]
        public virtual lu_application application { get; set; }



        public static address Create(Action<address> init)
        {
            var address = new address();

            //address.MessageAddressID = Guid.NewGuid();
            address.addresstype = new lu_addresstype();
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
