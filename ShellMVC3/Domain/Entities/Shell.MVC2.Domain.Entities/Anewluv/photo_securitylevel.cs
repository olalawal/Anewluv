using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    [DataContract ]
    public class photo_securitylevel
    {

        [Key]
        [DataMember ]
        public int id { get; set; }
        [DataMember]
        public lu_securityleveltype securityleveltype { get; set; }      
        [DataMember]
        public Guid photo_id { get; set; }
        [DataMember]
        public virtual photo photo { get; set; }
        
    }
}
