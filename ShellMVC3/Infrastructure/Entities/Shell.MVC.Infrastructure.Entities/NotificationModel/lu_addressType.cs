using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{


    [DataContract]
    public class lu_addresstype
    {
        //we generate this manually from enums for now
       [Key]
        public int id { get; set; } 
        public string description { get; set; }


    }
}
