using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    [DataContract]
    [NotMapped]  //From a different database no need to map to the database its virtual
    public class lu_location
    {
        [Key]
        public int id { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public int? countryid { get; set; }
        [DataMember]
        public string postalcode { get; set; }
        [NotMapped]
        [DataMember]
        public bool selected { get; set; }

    
    }
}
