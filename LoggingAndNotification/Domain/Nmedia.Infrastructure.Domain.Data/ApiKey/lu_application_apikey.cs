using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Nemdia.Infrastructure.Domain.Data.ApiKey
{


    [DataContract(Namespace = "")]
    public class lu_application_apikey
    {
        //we generate this manually from enums for now
        [Key]
        public int id { get; set; }
        [DataMember()]
        public string description { get; set; }



    }
}
