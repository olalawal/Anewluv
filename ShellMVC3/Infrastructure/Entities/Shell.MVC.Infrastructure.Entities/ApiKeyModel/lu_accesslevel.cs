using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Infrastructure.Entities.ApiKeyModel
{


    [DataContract(Namespace = "")]
    public class lu_accesslevel
    {
        //we generate this manually from enums for now
        [Key]
        public int id { get; set; }
        [DataMember()]
        public string description { get; set; }



    }
}
