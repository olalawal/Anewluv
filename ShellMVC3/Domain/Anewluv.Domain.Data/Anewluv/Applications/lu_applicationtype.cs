using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Shell.MVC2.Domain.Entities.Anewluv
{
    [DataContract]
    public class lu_applicationtype
    {     
        [DataMember]
        [Key]
        public int id { get; set; }
        [DataMember]
        public string description { get; set; }      
        //[NotMapped]
        public bool selected { get; set; }
    }
}
