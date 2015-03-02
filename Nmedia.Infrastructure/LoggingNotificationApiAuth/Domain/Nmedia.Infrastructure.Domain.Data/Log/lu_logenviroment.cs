using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;

namespace Nmedia.Infrastructure.Domain.Data.log
{


    [DataContract(Namespace = "")]
    public class lu_logenviroment :Entity
    {
        //we generate this manually from enums for now
        [Key]
        [DataMember()]
        public int id { get; set; }
        [DataMember()]
        public string description { get; set; }
        
    }
}
