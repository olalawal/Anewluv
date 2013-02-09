using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    [DataContract]
    public class lu_iconformat
    {     
        [DataMember]
        [Key]
        public int id { get; set; }
        public string description { get; set; }
        public int imagersizerformat_id { get; set; }
        public virtual lu_iconImagersizerformat imageresizerformat { get; set; }
        [NotMapped]
        public bool selected { get; set; }    
    }
}
