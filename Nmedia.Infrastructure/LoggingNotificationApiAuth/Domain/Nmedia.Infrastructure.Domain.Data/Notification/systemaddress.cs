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
    public class systemaddress :Entity
    {


        [Key]
        [DataMember()]
        public int id { get; set; }
        [DataMember()]
        public int systemaddresstype_id { get; set; }
        [DataMember()]
        public virtual lu_systemaddresstype systemaddresstype { get; set; }
        [DataMember()]
        public string emailaddress { get; set; }
        [DataMember()]
        public string hostip { get; set; }
        [DataMember()]
        public string hostname { get; set; }
        [DataMember()]
        public string createdby { get; set; }
        [DataMember()]
        public string credentialusername { get; set; }
        [DataMember()]
        public string credentialpassword { get; set; }    //salt password  plain text for now   
        [DataMember()]
        public bool? active { get; set; }
        [DataMember()]
        public DateTime? creationdate { get; set; }
        [DataMember()]
        public DateTime? removaldate { get; set; }

        public static systemaddress Create(Action<systemaddress> init)
        {
            var systemaddress = new systemaddress();
            //address.MessageAddressID = Guid.NewGuid();
            //systemaddress.creationdate = DateTime.Now;
            // systemaddress.RemovalDate = DateTime.Now;
            // systemaddress.active = true;
            // init(systemaddress); return systemaddress;
            return systemaddress;
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
